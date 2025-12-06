using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;

using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models;
using masterdata.Models.rulesmngt;
using masterdata.Models.Configuration;
using System.Reflection;
using System.Text;

namespace masterdata.Helpers
{

    public class OuCCAssociation : IOuCCAssociation
    {
        private ILogger Logger;
        private readonly IHrSyncAdapter hrSyncAdapter;
        private readonly IBwSyncAdapter bwSyncAdapter;
        private readonly ICompanyRulesAdapter companyRulesAdapter;
        private readonly IEntityAdapter entityAdapter;
        private readonly IActivityMappingAdapter activityMappingAdapter;

        //nuovo sviluppo exception
        private readonly IExceptionTableAdapter exceptionTableAdapter;
        private readonly IOptions<HrSyncConfiguration> hrSyncConfiguration;
        private IHrMasterdataOuData hrMasterDataOuData;
        private IResponsabilityAdapter responsabilityAdapter;
        private IResultData resultData;
        private IItaGloAdapter itaGloAdapter;
        private ISequenceData sequenceData;
        private IAssociationData associationData;
        private readonly IDBCleaning DBCleaning;
        private readonly IConstantValueAdapter ConstantValueAdapter;
        private readonly ITypologyTranscodingData TypologyTranscodingData;

        private ConstantValue ConstantValue;
        private readonly IClientAccessData ClientAccessData;
        private IMailSender MailSender;

        public OuCCAssociation(ILoggerFactory loggerFactory,
                                IHrSyncAdapter hrSyncAdapter,
                                IBwSyncAdapter bwSyncAdapter,
                                ICompanyRulesAdapter companyRulesAdapter,
                                IEntityAdapter entityAdapter,
                                IOptions<HrSyncConfiguration> hrSyncConfiguration,
                                IActivityMappingAdapter activityMappingAdapter,
                                IHrMasterdataOuData hrMasterDataOuData,
                                IResponsabilityAdapter responsabilityAdapter,
                                IResultData resultData,
                                IItaGloAdapter itaGloAdapter,
                                ISequenceData sequenceData,
                                IAssociationData associationData,
                                IDBCleaning dbCleaning,
                                IExceptionTableAdapter exceptionTableAdapter,
                                IConstantValueAdapter constantValueAdapter,
                                ITypologyTranscodingData typologyTranscodingData,
                                IClientAccessData clientaccessdata,
                                IMailSender mailSender)
        {
            this.Logger = loggerFactory.CreateLogger<OuCCAssociation>();
            this.hrSyncAdapter = hrSyncAdapter;
            this.bwSyncAdapter = bwSyncAdapter;
            this.companyRulesAdapter = companyRulesAdapter;
            this.entityAdapter = entityAdapter;
            this.activityMappingAdapter = activityMappingAdapter;
            this.hrSyncConfiguration = hrSyncConfiguration;
            this.hrMasterDataOuData = hrMasterDataOuData;
            this.resultData = resultData;
            this.responsabilityAdapter = responsabilityAdapter;
            this.itaGloAdapter = itaGloAdapter;
            this.sequenceData = sequenceData;
            this.associationData = associationData;
            this.DBCleaning = dbCleaning;
            this.exceptionTableAdapter = exceptionTableAdapter;
            this.ConstantValueAdapter = constantValueAdapter;
            this.TypologyTranscodingData = typologyTranscodingData;

            this.ConstantValue = new ConstantValue();
            this.ClientAccessData = clientaccessdata;
            this.MailSender = mailSender;

            try
            {
                Task<ConstantValue> constantValueTask = ConstantValueAdapter.GetConstantValue(CancellationToken.None);
                constantValueTask.Wait();

                if (constantValueTask.IsCompleted)
                {
                    ConstantValue = constantValueTask.Result;
                }
                if (ConstantValue == null)
                    this.ConstantValue = new ConstantValue("const");

            }
            catch (Exception e)
            {
                this.ConstantValue = new ConstantValue("const");
            }
        }

        public AssociationResult GetAssociationResult(string associationType, DateTime lastaccess)
        {
            AssociationResult result = new AssociationResult();
            List<HrmasterdataOu> hrMasterdataOuLists = new List<HrmasterdataOu>();

            try
            {
                Logger.LogInformation(string.Format("{0} - Get all masterdata.", DateTime.Now));
                hrMasterdataOuLists = hrMasterDataOuData.FindDistinct(lastaccess);

                Logger.LogInformation(string.Format("{0} - Inizio algoritmo association.", DateTime.Now));

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataOuLists;

                if (hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    Logger.LogInformation("inizio recupero CC" + DateTime.Now.ToString());
                    result = getCCForUO(hrMasterdataOuList, associationType, false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to compute association.", DateTime.Now));
            }
            return result;
        }

        public void ComputeAssociation(string associationType, DateTime lastaccess)
        {
            List<HrmasterdataOu> hrMasterdataOuLists = new List<HrmasterdataOu>();

            try
            {
                Logger.LogInformation(string.Format("{0} - Computing associations.", DateTime.Now));
                hrMasterdataOuLists = hrMasterDataOuData.FindDistinct(lastaccess);

                Logger.LogInformation(string.Format("{0} - Starting association algorithm...", DateTime.Now));

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataOuLists;

                if (hrMasterdataOuList != null && hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    foreach (HrmasterdataOu ou in hrMasterdataOuList.hrmasterdataou)
                    {
                        HrmasterdataOuList inneroulist = new HrmasterdataOuList();
                        inneroulist.hrmasterdataou = new List<HrmasterdataOu>();
                        inneroulist.hrmasterdataou.Add(ou);

                        //List<AssociationResult> result = getCCForUO();
                        AssociationResult result = getCCForUO(inneroulist, associationType, false);
                        if (result != null && result.AssociationList.Count > 0)
                        {
                            HrmasterdataOu unit = result.AssociationList[0].Unit;
                            int id = resultData.AddNewResultAssociationOU(new AssociationOrganizationUnit().Clone(unit));

                            if (id > 0 && result.AssociationList[0].Centers != null && result.AssociationList[0].Centers.Count > 0)
                            {
                                foreach (BwMasterObject cc in result.AssociationList[0].Centers)
                                {
                                    AssociationCostCenter acc = new AssociationCostCenter().Clone(cc);
                                    acc.OrganizationUnitId = id;
                                    //acc.syncDateTime = unit.SyncDateTime;
                                    resultData.AddNewResultAssociationCC(acc);
                                }
                            }
                        }
                    }
                    Logger.LogInformation(string.Format("{0} - Closing association algorithm...", DateTime.Now));
                }
                else
                {
                    Logger.LogInformation(string.Format("{0} - Closing association algorithm with no date returned.", DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to compute association.", DateTime.Now));
            }
        }

        public void ComputeAssociationV2(string associationType, DateTime lastaccess)
        {
            List<HrmasterdataOu> hrMasterdataOuLists = new List<HrmasterdataOu>();

            try
            {

                // List<string> notificationInit = new List<string>();
                // notificationInit.Add("AFC AWS CC Association STARTED");
                // notificationInit.Add("Processo associazione CC ad OU (ComputeAssociationV2) avviato!");
                // MailSender.SendMail(notificationInit);

                Logger.LogInformation(string.Format("{0} - Computing associations.", DateTime.Now));
                hrMasterdataOuLists = hrMasterDataOuData.FindDistinct(lastaccess);
                bool isDBClean;
                isDBClean = DBCleaning.ElaborationDBCleaning();

                Logger.LogInformation(string.Format("{0} - Starting association algorithm...", DateTime.Now));

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataOuLists;

                if (hrMasterdataOuList != null && hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    DateTime elaborationTimestamp = DateTime.Now;
                    foreach (HrmasterdataOu ou in hrMasterdataOuList.hrmasterdataou)
                    {
                        Logger.LogInformation(string.Format("{0} - Starting association UO...", ou.hrmdou_id));
                        HrmasterdataOuList inneroulist = new HrmasterdataOuList();
                        inneroulist.hrmasterdataou = new List<HrmasterdataOu>();
                        inneroulist.hrmasterdataou.Add(ou);

                        AssociationResultV2 resultV2 = new AssociationResultV2();
                        resultV2.AssociationList = new List<AssociationV2>();

                        AssociationResult result = getCCForUO(inneroulist, associationType, false);
                        if (result != null && result.AssociationList.Count > 0)
                        {
                            HrOu hrOu = new HrOu().Clone(inneroulist.hrmasterdataou[0]);

                            hrOu.typologyAssociation = result.AssociationList[0].Centers.Count == 0 ? ConstantValue.ko : result.AssociationList[0].Unit.Typology;
                            hrOu.totalCostCenters = result.AssociationList[0].Centers.Count;
                            hrOu.syncDateTime = elaborationTimestamp;

                            List<BwCC> bwCCList = new List<BwCC>();
                            AssociationV2 associationv2 = new AssociationV2();
                            associationv2.Unit = hrOu;

                            foreach (var item in result.AssociationList[0].Centers)
                            {
                                BwCC bwcc = new BwCC().Clone(item);
                                bwcc.uOrg = hrOu.UOrg;
                                bwcc.SyncDateTime = elaborationTimestamp;

                                bwCCList.Add(bwcc);
                            }

                            associationv2.Centers = bwCCList;
                            resultV2.AssociationList.Add(associationv2);

                        }

                        if (resultV2 != null && resultV2.AssociationList.Count > 0)
                        {
                            HrOu unit = resultV2.AssociationList[0].Unit;
                            int id = associationData.AddNewResultAssociationOU(unit);

                            if (id > 0 && result.AssociationList[0].Centers != null && result.AssociationList[0].Centers.Count > 0)
                            {
                                foreach (BwCC cc in resultV2.AssociationList[0].Centers)
                                {
                                    cc.organizationUnitId = id;
                                    associationData.AddNewResultAssociationCC(cc);
                                }
                            }
                        }
                    }
                    DBCleaning.ElaborationHRCleaning();
                    Logger.LogInformation(string.Format("{0} - Closing association algorithm...", DateTime.Now));
                    // mail ComputeAssociationV2 FULL OK
                    List<string> notificationEnd = new List<string>();
                    notificationEnd.Add("AFC AWS CC Association END");
                    notificationEnd.Add("Processo associazione CC ad OU (ComputeAssociationV2) terminato!");
                    MailSender.SendMail(notificationEnd);
                }
                else
                {
                    Logger.LogInformation(string.Format("{0} - Closing association algorithm with no OU in hrmasterdataou returned.", DateTime.Now));
                    // mail ComputeAssociationV2 FULL OK con warning
                    List<string> notificationWarning = new List<string>();
                    notificationWarning.Add("AFC AWS CC Association END with WARNING - No OU Found");
                    notificationWarning.Add("Processo associazione CC ad OU (ComputeAssociationV2) terminato, ma nessuna OU trovata in hrmasterdataou!");
                    MailSender.SendMail(notificationWarning);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to compute association: {1}", DateTime.Now,ex.Message));
                // mail ComputeAssociationV2 FULL KO
                List<string> notificationError = new List<string>();
                notificationError.Add("AFC AWS CC Association ERROR");
                notificationError.Add(string.Format("Processo associazione CC ad OU terminato NON correttamente. Errore nella funzione ComputeAssociationV2: {0}", ex.Message));
                MailSender.SendMail(notificationError);
            }
        }

        public void ComputeAssociationResume(string[] ids, string associationType, DateTime lastaccess)
        {
            List<HrmasterdataOu> hrMasterdataLists = new List<HrmasterdataOu>();

            foreach (string oucode in ids)
            {
                hrMasterdataLists.AddRange(getHrMasterDataOuCode(oucode));
            }

            try
            {
                Logger.LogInformation(string.Format("{0} - RESUME: Computing associations.", DateTime.Now));
                //hrMasterdataLists = hrMasterDataOuData.FindDistinct(lastaccess);
                bool isDBClean;
                //capire se va lasciato oppure no
                isDBClean = DBCleaning.ElaborationDBCleaning();

                Logger.LogInformation(string.Format("{0} - RESUME: Starting association algorithm...", DateTime.Now));

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataLists;

                if (hrMasterdataOuList != null && hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    DateTime elaborationTimestamp = DateTime.Now;
                    foreach (HrmasterdataOu ou in hrMasterdataOuList.hrmasterdataou)
                    {
                        HrmasterdataOuList inneroulist = new HrmasterdataOuList();
                        inneroulist.hrmasterdataou = new List<HrmasterdataOu>();
                        inneroulist.hrmasterdataou.Add(ou);

                        AssociationResultV2 resultV2 = new AssociationResultV2();
                        resultV2.AssociationList = new List<AssociationV2>();

                        AssociationResult result = getCCForUO(inneroulist, associationType, false);
                        if (result != null && result.AssociationList.Count > 0)
                        {
                            HrOu hrOu = new HrOu().Clone(inneroulist.hrmasterdataou[0]);

                            hrOu.typologyAssociation = result.AssociationList[0].Centers.Count == 0 ? ConstantValue.ko : result.AssociationList[0].Unit.Typology;
                            hrOu.totalCostCenters = result.AssociationList[0].Centers.Count;
                            hrOu.syncDateTime = elaborationTimestamp;

                            List<BwCC> bwCCList = new List<BwCC>();
                            AssociationV2 associationv2 = new AssociationV2();
                            associationv2.Unit = hrOu;

                            foreach (var item in result.AssociationList[0].Centers)
                            {
                                BwCC bwcc = new BwCC().Clone(item);
                                bwcc.uOrg = hrOu.UOrg;
                                bwcc.SyncDateTime = elaborationTimestamp;

                                bwCCList.Add(bwcc);
                            }

                            associationv2.Centers = bwCCList;
                            resultV2.AssociationList.Add(associationv2);

                        }
                        if (resultV2 != null && resultV2.AssociationList.Count > 0)
                        {
                            HrOu unit = resultV2.AssociationList[0].Unit;
                            int id = associationData.AddNewResultAssociationOU(unit);

                            if (id > 0 && result.AssociationList[0].Centers != null && result.AssociationList[0].Centers.Count > 0)
                            {
                                foreach (BwCC cc in resultV2.AssociationList[0].Centers)
                                {
                                    cc.organizationUnitId = id;
                                    associationData.AddNewResultAssociationCC(cc);
                                }
                            }
                        }
                    }
                    DBCleaning.ElaborationHRCleaning();
                    ClientAccessData.UpdateAccess(null, "RESUME_V2");
                    Logger.LogInformation(string.Format("{0} - RESUME: Closing association algorithm...", DateTime.Now));
                    List<string> notificationEnd = new List<string>();
                    notificationEnd.Add("AFC AWS RESUME CC Association END");
                    notificationEnd.Add("Processo RESUME associazione CC ad OU (ComputeAssociationResume) terminato!");
                    MailSender.SendMail(notificationEnd);
                }
                else
                {
                    Logger.LogInformation(string.Format("{0} - RESUME: Closing association algorithm with no data returned.", DateTime.Now));
                    List<string> notificationWarning = new List<string>();
                    notificationWarning.Add("AFC AWS RESUME CC Association END with WARNING - No OU Found");
                    notificationWarning.Add("Processo RESUME associazione CC ad OU (ComputeAssociationResume) terminato, ma nessuna OU trovata in hrmasterdataou!");
                    MailSender.SendMail(notificationWarning);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - RESUME: Unable to compute association.", DateTime.Now));
                List<string> notificationError = new List<string>();
                notificationError.Add("AFC AWS RESUME CC Association ERROR");
                notificationError.Add(string.Format("Processo RESUME associazione CC ad OU terminato NON correttamente. Errore nella funzione ComputeAssociationResume: {0}", ex.Message));
                MailSender.SendMail(notificationError);
            }
        }

        public AssociationResultV1 GetAssociationResultFromService(DateTime lastAccess)
        {
            AssociationResult associationresult = new AssociationResult();
            AssociationResultV1 associationResult = new AssociationResultV1();
            List<AssociationV1> associationList = new List<AssociationV1>();

            try
            {
                List<AssociationOrganizationUnit> AssociationOUList = resultData.getLastsDistinctUO(lastAccess);
                List<string> uorgcode = new List<string>();
                if (AssociationOUList.Count > 0)
                {
                    foreach (AssociationOrganizationUnit aou in AssociationOUList)
                    {
                        if (!uorgcode.Contains(aou.UOrg))
                        {
                            AssociationV1 association = new AssociationV1();
                            association.Unit = aou;
                            List<AssociationCostCenter> acclist = new List<AssociationCostCenter>();
                            acclist = resultData.getAssociationCCByUOId(aou.OrganizationUnitId);
                            association.Centers = acclist;

                            if (association.Centers == null || association.Centers.Count == 0)
                            {
                                association.TypologyAssociation = ConstantValue.ko;
                            }
                            else
                            {
                                association.TypologyAssociation = "insert";
                            }
                            associationList.Add(association);
                            uorgcode.Add(aou.uOrgCod);
                        }
                    }
                }

                associationResult.AssociationList = associationList;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get association from background service."));
            }

            return associationResult;
        }

        public AssociationResult GetAssociationResultOuCode(string[] listOucode, string associationType)
        {
            AssociationResult result = new AssociationResult();
            List<HrmasterdataOu> hrMasterdataOuLists = new List<HrmasterdataOu>();

            try
            {
                foreach (string oucode in listOucode)
                {
                    hrMasterdataOuLists.AddRange(getHrMasterDataOuCode(oucode));
                }

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataOuLists;

                if (hrMasterdataOuList.hrmasterdataou != null && hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    result = getCCForUO(hrMasterdataOuList, associationType, false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to compute association.", DateTime.Now));
            }
            return result;
        }

        public AssociationResultV2 GetAssociationResultOuCodeV2(string[] listOucode)
        {
            AssociationResult result = new AssociationResult();
            AssociationResultV2 resultV2 = new AssociationResultV2();
            resultV2.AssociationList = new List<AssociationV2>();
            List<HrmasterdataOu> hrMasterdataOuLists = new List<HrmasterdataOu>();


            try
            {
                foreach (string oucode in listOucode)
                {
                    hrMasterdataOuLists.AddRange(getHrMasterDataOuCode(oucode));
                }

                HrmasterdataOuList hrMasterdataOuList = new HrmasterdataOuList();
                hrMasterdataOuList.hrmasterdataou = hrMasterdataOuLists;

                if (hrMasterdataOuList.hrmasterdataou != null && hrMasterdataOuList.hrmasterdataou.Count > 0)
                {
                    result = getCCForUO(hrMasterdataOuList, "modify", false);
                    HrOu hrOu = new HrOu().Clone(hrMasterdataOuList.hrmasterdataou[0]);
                    hrOu.typologyAssociation = result.AssociationList[0].TypologyAssociation;
                    hrOu.totalCostCenters = result.AssociationList[0].Centers.Count;
                    List<BwCC> bwCCList = new List<BwCC>();
                    AssociationV2 associationv2 = new AssociationV2();
                    associationv2.Unit = hrOu;

                    foreach (var item in result.AssociationList[0].Centers)
                    {
                        BwCC bwcc = new BwCC().Clone(item);
                        bwcc.uOrg = hrOu.UOrg;
                        bwCCList.Add(bwcc);
                    }

                    associationv2.Centers = bwCCList;
                    resultV2.AssociationList.Add(associationv2);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to compute association.", DateTime.Now));
            }
            return resultV2;
        }

        public AssociationResult getCCForUO(HrmasterdataOuList orgUnitList, string associationType, bool isCleaner)
        {
            List<HrmasterdataOu> hrmasterdatas = orgUnitList.hrmasterdataou;

            if (hrmasterdatas == null || hrmasterdatas.Count == 0)
                return null;

            AssociationResult associationResult = new AssociationResult();
            List<BwMasterObject> bwDataList = new List<BwMasterObject>();
            List<Association> associationList = new List<Association>();

            try
            {

                foreach (HrmasterdataOu organizationalUnit in hrmasterdatas)
                {
                    CultureInfo myCIintl = new CultureInfo("us-US", false);
                    Association association = new Association();
                    association.Unit = organizationalUnit;
                    association.TypologyAssociation = associationType;

                    organizationalUnit.StartDate = formatterDateOU(organizationalUnit.StartDate);
                    organizationalUnit.EndDate = formatterDateOU(organizationalUnit.EndDate);

                    List<CompanyRules> companyRules = new List<CompanyRules>();
                    NotPrevLine notPrevLine = new NotPrevLine();
                    List<NotPrevLine> npaList = notPrevLine.GetListNpa(organizationalUnit, activityMappingAdapter);
                    List<BwMasterObject> cc = new List<BwMasterObject>();
                    List<BwMasterObject> ccCompatibile = new List<BwMasterObject>();

                    bool real = false;
                    bool cdcDummy = false;
                    bool esisteTranscodifica = true;
                    bool isItaGlo = false;
                    ItaGlo itaGlo = new ItaGlo();


                    Task<ItaGlo> itaGloResult = itaGloAdapter.GetItaGloInfo(CancellationToken.None, organizationalUnit.Company);
                    itaGloResult.Wait();


                    if (itaGloResult != null)
                    {
                        if (itaGloResult.Result != null && itaGloResult.Result.SapHrGlobalCode != null && itaGloResult.Result.IsEnelItalia.Equals("1"))
                        {
                            isItaGlo = true;
                        }
                    }

                    if (isItaGlo)
                    {
                        ActivityAssociationGroup actAss = GetActivityAssociation(organizationalUnit);

                        if (actAss.TypologyObject != null)
                        {
                            if (!actAss.TypologyObject.Equals(ConstantValue.gds) && !organizationalUnit.PerimeterDesc.Equals(ConstantValue.italiaperdesc))
                            {
                                if (organizationalUnit.CompanyDesc.Contains(ConstantValue.enelitalia)
                                    && !CheckProcurement(npaList, ConstantValue.procurement)
                                    && !actAss.TypologyObject.Equals(ConstantValue.procurement))
                                {
                                    companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.italiaperdesc);

                                }
                                else
                                {
                                    companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.centralperdesc);
                                }
                            }
                            else
                            {
                                companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.italiaperdesc);
                            }
                        }
                        else
                        {
                            esisteTranscodifica = false;
                        }
                    }
                    else
                    {
                        companyRules = getCompanyRulesBySapHr(organizationalUnit);
                    }
                    List<CompanyRules> appoCompany = new List<CompanyRules>();
                    if (companyRules.Count == 0)
                    {
                        appoCompany = GetCompanyByDesc(organizationalUnit);
                        companyRules.AddRange(appoCompany);
                        if (companyRules.Count == 0)
                            esisteTranscodifica = false;
                    }


                    if (esisteTranscodifica)
                    {
                        int i = 0;
                        foreach (CompanyRules company in companyRules)
                        {
                            if (company.FlagConsolidation.ToUpper().Contains("TRUE"))
                            {
                                List<Responsability> responsabilityList = getResponsability(company.NewPrimoCode);

                                cdcDummy = false;

                                if (string.IsNullOrEmpty(company.AfcCompanyCode))
                                    cdcDummy = true;

                                organizationalUnit.Country = company.CodeNation.NationAcronym;

                                List<QueryBuilder> queryBuilderList = new List<QueryBuilder>();
                                queryBuilderList = SwitchPrevalentActivity(organizationalUnit, company, queryBuilderList, npaList);

                                if (queryBuilderList != null && queryBuilderList.Count > 0)
                                {
                                    if (isCleaner)
                                        association = GetCCCleaner(association, queryBuilderList, company, responsabilityList, i);
                                    else
                                        cc = GetCCResult(queryBuilderList, real, responsabilityList, company, association, organizationalUnit, cc);
                                }
                                else
                                {
                                    List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                    association.Centers = costCenter;
                                }
                            }
                            else
                            {
                                //List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                association.Centers = new List<BwMasterObject>();
                                associationResult.AssociationList = new List<Association>();
                                associationResult.AssociationList = associationList;
                            }
                        }
                    }
                    else
                    {
                        if (companyRules != null)
                            organizationalUnit.Country = companyRules[0].CodeNation.NationAcronym;

                        List<BwMasterObject> costCenter = new List<BwMasterObject>();
                        association.Centers = costCenter;
                    }

                    if (cc.Count > 0)
                    {

                        association = GetCCFinished(cc, association, cdcDummy);
                        association.TypologyAssociation = ConstantValue.ok;
                    }
                    else
                    {
                        association.TypologyAssociation = ConstantValue.ko;
                    }

                    associationList.Add(association);

                    if (associationList.Count > 0)
                    {
                        associationResult.AssociationList = associationList;
                    }
                    else
                    {
                        association.TypologyAssociation = ConstantValue.ficticious;
                        List<BwMasterObject> costCenter = new List<BwMasterObject>();
                        association.Centers = costCenter;
                        associationResult.AssociationList = associationList;
                    }
                }
                return associationResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        private ActivityAssociationGroup GetActivityAssociation(HrmasterdataOu organizationalUnit)
        {
            ActivityAssociationGroup actAss = new ActivityAssociationGroup();
            Task<List<ActivityAssociationGroup>> activityAssociation = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, organizationalUnit.AttrPrevCod, organizationalUnit.PredAttr);
            activityAssociation.Wait();

            if (activityAssociation.IsCompleted)
            {
                if (activityAssociation.Result.Count > 0)
                {
                    actAss = activityAssociation.Result[0];
                }
            }
            return actAss;
        }

        #region Gestione OU
        public List<HrmasterdataOu> getHrMasterDataOuCode(string oucode)
        {
            Task<HrmasterdataOuList> organizationalUnitsTask = hrSyncAdapter.GetOrganizationalUnit(CancellationToken.None,
                                                                                                     oucode);
            organizationalUnitsTask.Wait();
            if (organizationalUnitsTask.IsCompleted)
            {
                if (organizationalUnitsTask.Result != null)
                {
                    return organizationalUnitsTask.Result.hrmasterdataou;
                }
                else
                    return new List<HrmasterdataOu>();
            }
            else
            {
                return new List<HrmasterdataOu>();
            }
        }

        private List<Responsability> getResponsability(string newPrimoCode)
        {
            Task<List<Responsability>> responsabilityResult = responsabilityAdapter.GetResponsabilityByNewPrimoCode(CancellationToken.None, newPrimoCode.ToUpper());
            responsabilityResult.Wait();
            List<Responsability> resp = responsabilityResult.Result;

            return resp;
        }
        #endregion

        #region Gestione Company
        private List<CompanyRules> getCompanyRulesByPerimeterDesc(HrmasterdataOu organizationalUnit, string perimeter)
        {
            Task<List<CompanyRules>> CompanyRulesTask = null;
            List<CompanyRules> companyList = new List<CompanyRules>();

            Logger.LogInformation("Recupero Company rules" + DateTime.Now.ToString());
            CompanyRulesTask = companyRulesAdapter.GetCompanyByDescPerimeter(organizationalUnit.Company, perimeter, CancellationToken.None);
            CompanyRulesTask.Wait();
            if (CompanyRulesTask != null)
            {
                if (CompanyRulesTask.Result != null)
                {
                    if (CompanyRulesTask.Result.Count > 0)
                    {
                        companyList = CompanyRulesTask.Result;
                    }
                }
            }

            return companyList;
        }
        private List<CompanyRules> GetCompanyByDesc(HrmasterdataOu organizationalUnit)
        {
            Task<List<CompanyRules>> CompanyRulesTask = null;
            List<CompanyRules> companyList = new List<CompanyRules>();

            Logger.LogInformation("Recupero Company rules" + DateTime.Now.ToString());
            CompanyRulesTask = companyRulesAdapter.GetCompanyBynewPrimoDesc(organizationalUnit.CompanyDesc, CancellationToken.None);
            CompanyRulesTask.Wait();
            if (CompanyRulesTask != null)
            {
                if (CompanyRulesTask.Result != null)
                {
                    if (CompanyRulesTask.Result.Count > 0)
                    {
                        companyList = CompanyRulesTask.Result;
                    }
                }
            }

            return companyList;
        }

        private List<CompanyRules> getCompanyRulesBySapHr(HrmasterdataOu organizationalUnit)
        {
            Task<List<CompanyRules>> CompanyRulesTask = null;
            List<CompanyRules> companyList = new List<CompanyRules>();

            Logger.LogInformation("Recupero Company rules" + DateTime.Now.ToString());
            CompanyRulesTask = companyRulesAdapter.GetCompanyBySapHRCode(organizationalUnit.Company, CancellationToken.None);
            CompanyRulesTask.Wait();
            if (CompanyRulesTask != null)
            {
                if (CompanyRulesTask.Result != null)
                {
                    if (CompanyRulesTask.Result.Count > 0)
                    {
                        companyList = CompanyRulesTask.Result;
                    }
                }
            }

            return companyList;
        }
        #endregion

        #region check

        private bool CheckProcurement(List<NotPrevLine> ListNotPrevCode, string prevCodeProc)
        {
            bool compare = false;
            foreach (NotPrevLine codPrev in ListNotPrevCode)
            {
                if (codPrev.TypologyBl == prevCodeProc)
                    return compare = true;
                else
                    return compare = false;
            }
            return compare;
        }

        private bool CheckPrevalent(string orgUnitPrevalent, string businessLinePrevalent)
        {
            bool check = false;
            if (businessLinePrevalent != null && orgUnitPrevalent != null)
            {
                if (orgUnitPrevalent.Contains(businessLinePrevalent))
                    check = true;
                else
                    check = false;
            }
            return check;
        }

        private string GetPrevalentLineCompany(ICollection<BusinessLines> businessLines)
        {
            string prevalent = null;
            foreach (BusinessLines bsl in businessLines)
            {
                if (bsl.Prevalent == 1)
                {
                    prevalent = bsl.BusinessLinesName;
                    prevalent = prevalent.Replace('i', 'b');
                }

            }
            return prevalent;
        }

        private bool CheckBHolBSer(string companyAFCCode)
        {
            Entity entity = new Entity();
            Task<Entity> entityTask = entityAdapter.GetEntityByNewPrimo(CancellationToken.None, companyAFCCode);
            entityTask.Wait();
            bool isPresent = false;
            if (entityTask.IsCompleted)
            {
                entity = entityTask.Result;
                if (entity != null)
                {

                    isPresent = true;
                }
                else
                    isPresent = false;
            }
            else
                isPresent = false;

            return isPresent;
        }

        private bool CoerenceBSer(ICollection<BusinessLines> bsCompany)
        {
            bool isCoerent = false;
            foreach (BusinessLines bs in bsCompany)
            {
                if (bs.Prevalent == 0 && bs.BusinessLinesName.ToUpper().Equals(ConstantValue.iser))
                    isCoerent = true;
            }
            return isCoerent;
        }

        private bool CheckMultiBl(List<NotPrevLine> notPrevList)
        {
            bool isMulti = false;
            foreach (NotPrevLine np in notPrevList)
            {
                if (np.objectTypeId.ToString().Equals(ConstantValue.businesslinecode) && np.codeobject == ConstantValue.codeObjectMultiBusiness)
                {
                    isMulti = true;
                    break;
                }

            }

            return isMulti;
        }

        private bool CheckNpaCoerence(string nPa, CompanyRules companyBl)
        {
            bool check = false;
            nPa = nPa.ToUpper();
            foreach (BusinessLines bl in companyBl.BusinessLines)
            {
                if (bl.Prevalent == 0)
                {
                    if (nPa.Contains(ConstantValue.gblBholBser.ToUpper()) || nPa.Contains(ConstantValue.bholBser.ToUpper()))
                    {
                        nPa = GetPrevalentLineCompany(companyBl.BusinessLines);
                        nPa = nPa.Replace('i', 'b');
                    }

                    string prevalent = bl.BusinessLinesName;
                    prevalent = prevalent.Replace('i', 'b').ToUpper();
                    nPa = nPa.Replace('i', 'b').ToUpper();
                    if (prevalent.Contains(nPa))
                    {
                        check = true;
                        break;
                    }
                    else
                    {
                        check = false;
                    }
                }
            }
            return check;
        }


        private bool IsLatam(CompanyRules company)
        {
            bool isLatam = false;
            foreach (BusinessLines bl in company.BusinessLines)
            {
                if (bl.Prevalent == 0)
                {
                    if (bl.BusinessLinesName.ToUpper().Contains(ConstantValue.irel) || bl.BusinessLinesName.ToUpper().Contains(ConstantValue.iesl))
                    {
                        isLatam = true;
                        break;
                    }

                }
            }
            return isLatam;
        }

        private int CheckNpaGblEquals(List<NotPrevLine> npaList)
        {
            int numCheck = 1;

            if (npaList.Count > 0)
            {
                if (npaList.Count <= 1 && npaList[0].objectTypeId == Convert.ToUInt32(ConstantValue.businesslinecode))
                    return numCheck;

                for (int i = 0; i < npaList.Count; i++)
                {
                    if (npaList[i].objectTypeId == Convert.ToUInt32(ConstantValue.businesslinecode))
                    {
                        if (i == npaList.Count - 1)
                            break;
                        if (npaList[i].codeobject != npaList[i + 1].codeobject)
                        {
                            numCheck = npaList.Count;
                            break;
                        }

                    }
                }
            }
            else
                numCheck = 0;

            return numCheck;

        }

        // Controllo GCO 31-04 service oppure object_type_id = servicecode(31) and po_object_abbr like '0503%'
        private int CheckNpaGCOEquals(List<NotPrevLine> npaList)
        {
            int numCheck = 1;
            string init_search_po_object_abbr = ConstantValue.gco_abbr_constants;
            /* filtro solo le GCO (31-04) oppure (object_type_id = servicecode(31) and po_object_abbr like '0503%') e le metto in una nuova lista 
             */
            List<NotPrevLine> npaListGCO = new List<NotPrevLine>();
            foreach (NotPrevLine npa in npaList)
            {
                if ((npa.TypologyBl == ConstantValue.TypologyGCO) || (npa.objectTypeId.ToString() == ConstantValue.servicecode && npa.PoObjectAbbr.StartsWith(init_search_po_object_abbr)))
                    npaListGCO.Add(npa);
            }

            if (npaListGCO.Count > 0)
            {
                if ( npaListGCO.Count <= 1 && 
                     (    (npaListGCO[0].TypologyBl == ConstantValue.TypologyGCO) 
                       || (npaListGCO[0].objectTypeId.ToString() == ConstantValue.servicecode && npaListGCO[0].PoObjectAbbr.StartsWith(init_search_po_object_abbr))
                     )
                   )
                    return numCheck;
                /* se più di una npa controlla che siano tutte GCO con lo stesso codeobject,
                 * se così allora ritorna numero npa GCO = 1
                 * altrimenti ritorna numero npa GCO > 1
                 */
                for (int i = 0; i < npaList.Count; i++)
                {
                    if (    (npaListGCO[i].TypologyBl == ConstantValue.TypologyGCO)
                         || (npaListGCO[i].objectTypeId.ToString() == ConstantValue.servicecode && npaListGCO[i].PoObjectAbbr.StartsWith(init_search_po_object_abbr))
                       )
                    {
                        if (i == npaListGCO.Count - 1)
                            break;
                        if (npaListGCO[i].codeobject != npaListGCO[i + 1].codeobject)
                        {
                            numCheck = npaListGCO.Count;
                            break;
                        }
                    }
                }
            }
            else
                numCheck = 0;
            
            return numCheck;
        
        }

        #endregion

        #region logic

        private List<QueryBuilder> SwitchPrevalentActivity(HrmasterdataOu orgUnit, CompanyRules company, List<QueryBuilder> queryBuilderList, List<NotPrevLine> npaList)
        {
            List<ActivityMapping> activityMapping = new List<ActivityMapping>();


            Task<List<ActivityMapping>> activityMappingTask = activityMappingAdapter.GetActivityMappingByCodeEPrev(CancellationToken.None, orgUnit.AttrPrevCod, orgUnit.PredAttr);
            activityMappingTask.Wait();

            if (activityMappingTask.IsCompleted)
            {
                activityMapping = activityMappingTask.Result;

                if (activityMapping != null && activityMapping.Count > 0)
                {
                    switch (orgUnit.PredAttr)
                    {
                        case ConstantValue.businesslinecode:
                            queryBuilderList = CaseBusinessLine(orgUnit, queryBuilderList, company, activityMapping, npaList);
                            break;
                        case ConstantValue.servicecode:
                            queryBuilderList = CaseService(orgUnit, queryBuilderList, company, activityMapping, npaList);
                            break;
                        case ConstantValue.staffcode:
                            queryBuilderList = CaseStaff(orgUnit, queryBuilderList, company, activityMapping, npaList);
                            break;
                    }
                }
                else
                {
                    return null;
                }
            }
            return queryBuilderList;
        }

        private List<QueryBuilder> CaseBusinessLine(HrmasterdataOu orgUnit, List<QueryBuilder> queryBuilderList, CompanyRules company, List<ActivityMapping> activityMappingList, List<NotPrevLine> npaList)
        {
            bool isLatam = IsLatam(company);
            List<ActivityAssociationGroup> actAssList = new List<ActivityAssociationGroup>();
            Task<List<ActivityAssociationGroup>> activityAssociation = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, orgUnit.AttrPrevCod, orgUnit.PredAttr);
            activityAssociation.Wait();

            if (activityAssociation.IsCompleted)
            {
                actAssList = activityAssociation.Result;
            }

            if (!isLatam)
            {
                foreach (ActivityMapping activityMapping in activityMappingList)
                {
                    if (!activityMapping.MacroOrg1.MacroOrg1Code.ToUpper().Contains(ConstantValue.rel)
                      && !activityMapping.MacroOrg1.MacroOrg1Code.ToUpper().Contains(ConstantValue.esl))
                    {
                        QueryBuilder queryBuilder = new QueryBuilder();

                        // nuovo sviluppo exception 

                        List<ExceptionTable> exceptions = new List<ExceptionTable>();

                        Task<List<ExceptionTable>> exceptionsTask = exceptionTableAdapter.GetExceptions();
                        exceptionsTask.Wait();

                        if (exceptionsTask.IsCompleted)
                        {
                            exceptions = exceptionsTask.Result;
                        }

                        bool isException = IsException(exceptions, orgUnit, company, npaList);
                        if (isException)
                        {
                            NotPrevLine notPrevLine = new NotPrevLine();
                            List<NotPrevLine> npaListException = notPrevLine.GetListNpaEception(orgUnit, activityMappingAdapter);
                            queryBuilder = GetQueryBuilder(activityMapping, company);
                            List<QueryBuilder> queryBuilderListAppo = new List<QueryBuilder>();

                            if (string.IsNullOrEmpty(queryBuilder.MacroOrg1))
                            {
                                foreach (NotPrevLine np in npaListException)
                                {
                                    QueryBuilder queryBuilderAppo = new QueryBuilder();
                                    queryBuilderAppo.MacroOrg2 = queryBuilder.MacroOrg2;
                                    queryBuilderAppo.Organization = queryBuilder.Organization;
                                    queryBuilderAppo.Process = queryBuilder.Process;
                                    queryBuilderAppo.Vcs = queryBuilder.Vcs;
                                    queryBuilderAppo.OrganizationDescription = queryBuilder.OrganizationDescription;
                                    queryBuilderAppo.ProcessDescription = queryBuilder.ProcessDescription;
                                    queryBuilderAppo.VcsDescription = queryBuilder.VcsDescription;
                                    queryBuilderAppo.MacroOrg1 = np.PrevalentBl;
                                    queryBuilderAppo.Result = ConstantValue.ok;
                                    queryBuilderListAppo.Add(queryBuilderAppo);
                                }
                            }
                            if (queryBuilderListAppo.Count > 0)
                                queryBuilderList = queryBuilderListAppo;
                        }
                        else
                        {
                            //fine sviluppo eccezione
                            if (CheckNpaCoerence(activityMapping.MacroOrg1.MacroOrg1Code, company))
                            {
                                if (actAssList != null && actAssList.Count > 0)
                                {
                                    if (CheckPrevalent(activityMapping.TypologyObject, ConstantValue.attrPrevBsMl))
                                    {
                                        if (CoerenceBHolBSer(company.BusinessLines))
                                        {
                                            //activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                            queryBuilder.Result = ConstantValue.ok;
                                            queryBuilderList.Add(queryBuilder);
                                        }
                                        else
                                        {
                                            activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                            queryBuilder.Result = ConstantValue.ok;
                                            queryBuilderList.Add(queryBuilder);
                                        }

                                    }
                                    else
                                    {
                                        //activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                        queryBuilder = GetQueryBuilder(activityMapping, company);
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }
                                else
                                {
                                    queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                                }
                            }
                            else
                            {
                                queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                            }
                        }
                    }

                }
            }
            else
            {
                foreach (ActivityMapping activityMapping in activityMappingList)
                {
                    QueryBuilder queryBuilder = new QueryBuilder();
                    if (CheckNpaCoerence(activityMapping.MacroOrg1.MacroOrg1Code, company))
                    {

                        if (actAssList != null && actAssList.Count > 0)
                        {
                            //MODIFICA INDRA PER DEBUG
                            if (CheckPrevalent(actAssList[0].TypologyObject, ConstantValue.attrPrevBsMl) ||
                                CheckPrevalent(actAssList[0].BpcCode, ConstantValue.attrPrevBsMl))
                            {
                                if (CoerenceBHolBSer(company.BusinessLines))
                                {
                                    //activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                    queryBuilder = GetQueryBuilder(activityMapping, company);
                                    queryBuilder.Result = ConstantValue.ok;
                                    queryBuilderList.Add(queryBuilder);
                                }
                                else
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                    queryBuilder = GetQueryBuilder(activityMapping, company);
                                    queryBuilder.Result = ConstantValue.ok;
                                    queryBuilderList.Add(queryBuilder);
                                }
                            }
                            else
                            {
                                //activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                        else
                        {
                            queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                        }
                    }
                    else
                    {
                        queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                    }
                }
            }
            return queryBuilderList;
        }

        private List<QueryBuilder> CaseStaff(HrmasterdataOu orgUnit, List<QueryBuilder> queryBuilderList, CompanyRules company, List<ActivityMapping> activityMappingList, List<NotPrevLine> npaList)
        {
            bool isLatam = IsLatam(company);
            bool notholser = true;
            string prevalentCode = GetPrevalentLineCompany(company.BusinessLines);
            NotPrevLine notPrevLine = new NotPrevLine();


            if (isLatam)
            {
                if (!CheckBHolBSer(company.NewPrimoCode))
                {
                    List<NotPrevLine> npaLatamList = new List<NotPrevLine>();
                    foreach (NotPrevLine npa in npaList)
                    {
                        if (!npa.PrevalentBl.Contains(ConstantValue.gbl))
                        {
                            if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.rel)
                                || npa.PrevalentBl.ToUpper().Contains(ConstantValue.esl))
                            {
                                npaLatamList.Add(npa);
                            }
                            else if (!npa.PrevalentBl.ToUpper().Contains(ConstantValue.ret)
                               && !npa.PrevalentBl.ToUpper().Contains(ConstantValue.eso))
                            {
                                npaLatamList.Add(npa);
                            }

                        }
                    }
                    if (npaLatamList.Count > 0)
                        npaList = npaLatamList;
                }
            }
            else
            {
                foreach (NotPrevLine npa in npaList)
                {
                    if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.rel))
                        npa.PrevalentBl = "bRET";
                    else if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.esl))
                        npa.PrevalentBl = "bESO";
                }

            }

            if (CheckBHolBSer(company.NewPrimoCode))
            {
                foreach (ActivityMapping activityMapping in activityMappingList)
                {
                    activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                    QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                    queryBuilder.Result = ConstantValue.ok;
                    queryBuilderList.Add(queryBuilder);
                }
                notholser = false;
            }

            if (notholser)
            {
                //fine sviluppo exception
                bool npagbl = true;
                int npagblCheck = 0;
                int npaGCOCheck = 0;
                //Verifico che ci sia una notprevalent e che sia una BusinessLine, controllo anche che siano tutte uguali se più di una
                //DXC Maggio 2022: Verifico inoltre che ci sia una notprevalent service GCO, controllo anche che siano tutte uguali se più di una
                List<NotPrevLine> npaBlList = notPrevLine.GetListNpaBl(orgUnit);
                List<NotPrevLine> npaSeList = notPrevLine.GetListNpaSe(orgUnit, activityMappingAdapter, isLatam, ConstantValue.gco_abbr_constants);
                if (npaList.Count > 0)
                {
                    npagblCheck = CheckNpaGblEquals(npaBlList);
                    npaGCOCheck = CheckNpaGCOEquals(npaSeList);
                    if ((npagblCheck + npaGCOCheck) == 1)
                    {
                        npagbl = true;
                        if (npaGCOCheck == 1)
                            npaList = npaSeList;
                    }
                    else
                        npagbl = false;
                }
                else
                    npagbl = false;

                if (npagbl)
                {
                    // //nuovo sviluppo exception
                    List<ExceptionTable> exceptions = new List<ExceptionTable>();

                    Task<List<ExceptionTable>> exceptionsTask = exceptionTableAdapter.GetExceptions();
                    exceptionsTask.Wait();

                    if (exceptionsTask.IsCompleted)
                    {
                        exceptions = exceptionsTask.Result;
                    }
                    bool isException = IsException(exceptions, orgUnit, company, npaList);
                    if (isException)
                    {
                        NotPrevLine notPrevLineO = new NotPrevLine();
                        List<NotPrevLine> npaListException = notPrevLineO.GetListNpaEception(orgUnit, activityMappingAdapter);

                        QueryBuilder queryBuilder = new QueryBuilder();
                        foreach (ActivityMapping activityMapping in activityMappingList)
                        {
                            queryBuilder = GetQueryBuilder(activityMapping, company);

                        }
                        List<QueryBuilder> queryBuilderListAppo = new List<QueryBuilder>();
                        
                        List<NotPrevLine> npaCompatibili_1 = npaListException.Where(npa => CheckNpaCoerence(npa.PrevalentBl, company)).ToList();


                        if (npaCompatibili_1.Count > 0)
                        {
                            foreach (NotPrevLine np in npaCompatibili_1)
                            {
                            
                            //MODIFICA LS(EY) 27.09.2024  
                            if (!np.PrevalentBl.ToUpper().Contains("GBL"))
                            {
                          // END  //MODIFICA LS(EY) 27.09.2024 
                                QueryBuilder queryBuilderAppo = new QueryBuilder();
                                queryBuilderAppo.MacroOrg2 = queryBuilder.MacroOrg2;
                                queryBuilderAppo.Organization = queryBuilder.Organization;
                                queryBuilderAppo.Process = queryBuilder.Process;
                                queryBuilderAppo.Vcs = queryBuilder.Vcs;
                                queryBuilderAppo.OrganizationDescription = queryBuilder.OrganizationDescription;
                                queryBuilderAppo.ProcessDescription = queryBuilder.ProcessDescription;
                                queryBuilderAppo.VcsDescription = queryBuilder.VcsDescription;
                                queryBuilderAppo.MacroOrg1 = np.PrevalentBl;
                                queryBuilderAppo.Result = ConstantValue.ok;
                                queryBuilderListAppo.Add(queryBuilderAppo);
                          //MODIFICA LS(EY) 27.09.2024 
                            }
                                // END  //MODIFICA LS(EY) 27.09.2024
                         }
                        }
                        else
                            {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                activityMapping.MacroOrg1.MacroOrg1Code = prevalentCode;
                                QueryBuilder queryBuilder1 = GetQueryBuilder(activityMapping, company);
                                queryBuilder1.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder1);
                            }
                        }

                        if (queryBuilderListAppo.Count > 0)
                            queryBuilderList = queryBuilderListAppo;
                    }
                    else
                    {
                        bool checkNpa = true;
                        List<NotPrevLine> npaCompatibili = npaList.Where(npa => CheckNpaCoerence(npa.PrevalentBl, company)).ToList();

                        if (npaCompatibili.Count > 0)
                        {
                            foreach (NotPrevLine npa in npaCompatibili)
                            {
                                if (npa.objectTypeId == Convert.ToUInt32(ConstantValue.businesslinecode))
                                {
                                    checkNpa = CheckNpaCoerence(npa.PrevalentBl, company);
                                    if (!checkNpa)
                                        break;
                                }
                                if (npa.objectTypeId == Convert.ToUInt32(ConstantValue.servicecode))
                                {
                                    checkNpa = CheckNpaCoerence(npa.PrevalentBl, company);
                                    if (!checkNpa)
                                        break;
                                }
                            }
                        }
                        else
                            checkNpa = false;
                        if (checkNpa)
                        {
                            // verificare con Anglioletti se è valido questo check 20.02.2025
                            if (npaList[0].objectTypeId.ToString() == ConstantValue.businesslinecode && CheckMultiBl(npaList))
                            {
                                if (CoerenceBSer(company.BusinessLines))
                                {
                                    foreach (ActivityMapping activityMapping in activityMappingList)
                                    {
                                        activityMapping.MacroOrg1.MacroOrg1Code = ConstantValue.bser;
                                        QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }
                                else
                                {
                                    foreach (ActivityMapping activityMapping in activityMappingList)
                                    {
                                        activityMapping.MacroOrg1.MacroOrg1Code = prevalentCode;
                                        QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }
                            }
                            else
                            {
                                // NO MultiBusiness
                                foreach (ActivityMapping activityMapping in activityMappingList)
                                {

                                    QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                    queryBuilder.MacroOrg1 = npaList[0].PrevalentBl;
                                    // queryBuilder.MacroOrg1 = GetPrevalentLineCompany(company.BusinessLines);
                                    queryBuilder.Result = ConstantValue.ok;
                                    queryBuilderList.Add(queryBuilder);

                                    // if (activityMapping.MacroOrg1.MacroOrg1Code.Contains("GBL")
                                    // && GetPrevalentLineCompany(company.BusinessLines).ToUpper() != "BHOL"
                                    // && GetPrevalentLineCompany(company.BusinessLines).ToUpper() != "BSER"
                                    // )
                                    // {
                                    //     QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                    //     queryBuilder.MacroOrg1 = npaList[0].PrevalentBl;
                                    //     queryBuilder.Result = ConstantValue.ok;
                                    //     queryBuilderList.Add(queryBuilder);
                                    // }
                                }
                            }
                        }
                        else
                        {
                            //queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                            // indra: qui dovrei mettere altra query non il ko
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
                else
                {
                    // List<NotPrevLine> npaGbl = notPrevLine.GetListNpaBl(orgUnit);
                    // List<NotPrevLine> npaServList = notPrevLine.GetListNpaSe(orgUnit, activityMappingAdapter);
                    // DXC Maggio 2022: devo considerare anche le npa service oltre alle Bl
                    if (npaBlList.Count + npaSeList.Count > 0)
                    {
                        // DXC Maggio 2022: Check npa GCO disabilitato
                        /*
                        // CR Indra: GCO
                        if (CheckNpaGco(npaList))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                if (isLatam)
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = "BREL";
                                }
                                else
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = "BRET";
                                }
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                        else
                        */
                        if (!CoerenceBSer(company.BusinessLines))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                if (GBLPrevIsEqualsToNotPrevBL(GetPrevalentLineCompany(company.BusinessLines), npaList))
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                    QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                    queryBuilder.Result = ConstantValue.ok;
                                    queryBuilderList.Add(queryBuilder);
                                }
                                else
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = prevalentCode;
                                    QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                    queryBuilder.Result = ConstantValue.ok;
                                    queryBuilderList.Add(queryBuilder);
                                }
                            }
                        }
                        else
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                activityMapping.MacroOrg1.MacroOrg1Code = ConstantValue.bser;
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                    else
                    {
                        // DXC Maggio 2022: Check npa GCO disabilitato
                        /*
                        // CR Indra: GCO
                        if (CheckNpaGco(npaList))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                if (isLatam)
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = "BREL";
                                }
                                else
                                {
                                    activityMapping.MacroOrg1.MacroOrg1Code = "BRET";
                                }
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                        else
                        */
                        if(CoerenceBSer(company.BusinessLines))
                        {

                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                activityMapping.MacroOrg1.MacroOrg1Code = ConstantValue.bser;
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                        else
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                //forzo la macroorg1 con la gblprevalen
                                activityMapping.MacroOrg1.MacroOrg1Code = prevalentCode;
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
            }
            return queryBuilderList;
        }
        private List<QueryBuilder> CaseService(HrmasterdataOu orgUnit, List<QueryBuilder> queryBuilderList, CompanyRules company, List<ActivityMapping> activityMappingList, List<NotPrevLine> npaList)
        {
            //Verifico che la company non sia una delle LATAM
            bool isLatam = IsLatam(company);
            string prevalentCode = GetPrevalentLineCompany(company.BusinessLines);
            bool notholser = true;
            NotPrevLine notPrevLine = new NotPrevLine();

            //Caso LATAM
            if (isLatam)
            {
                //Controllo entity bhol/bser
                if (!CheckBHolBSer(company.NewPrimoCode))
                {
                    List<NotPrevLine> npaLatamList = new List<NotPrevLine>();
                    foreach (NotPrevLine npa in npaList)
                    {
                        if (!npa.PrevalentBl.Contains(ConstantValue.gbl))
                        {//Se ho recuperato sull'activityassosiation un act bret/brel la escludo dalla lista
                            if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.rel)
                                || npa.PrevalentBl.ToUpper().Contains(ConstantValue.esl))
                            {
                                npaLatamList.Add(npa);
                            }
                            else if (!npa.PrevalentBl.ToUpper().Contains(ConstantValue.ret)
                               && !npa.PrevalentBl.ToUpper().Contains(ConstantValue.eso))
                            {
                                npaLatamList.Add(npa);
                            }
                        }
                    }
                    if (npaLatamList.Count > 0)
                        npaList = npaLatamList;
                }
            }
            else
            {
                foreach (NotPrevLine npa in npaList)
                {
                    if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.rel))
                        npa.PrevalentBl = "bRET";
                    else if (npa.PrevalentBl.ToUpper().Contains(ConstantValue.esl))
                        npa.PrevalentBl = "bESO";
                }
            }
            bool npagbl = true;
            int npagblCheck = 0;
            int npaGCOCheck = 0;
            //Verifico che ci sia una notprevalent e che sia una BusinessLine, controllo anche se sono tutte uguali
            //DXC Maggio 2022: Verifico inoltre che ci sia una notprevalent GCO, controllo anche se sono tutte uguali
            List<NotPrevLine> npaBlList = notPrevLine.GetListNpaBl(orgUnit);
            List<NotPrevLine> npaSeList = notPrevLine.GetListNpaSe(orgUnit, activityMappingAdapter, isLatam, ConstantValue.gco_abbr_constants);
            if (npaList.Count > 0)
            {
                npagblCheck = CheckNpaGblEquals(npaBlList);
                npaGCOCheck = CheckNpaGCOEquals(npaSeList);
                if ((npagblCheck + npaGCOCheck) == 1)
                {
                    npagbl = true;
                    if (npaGCOCheck == 1)
                        npaList = npaSeList;
                }
                else
                    npagbl = false;
            }
            else
                npagbl = false;

            //controllo entity bhol/bser
            if (CheckBHolBSer(company.NewPrimoCode))
            {
                queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, true, false);
                notholser = false;
                //ALLINEAMENTO CON LE STAFF
                /*
                foreach (ActivityMapping activityMapping in activityMappingList)
                {
                    activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                    QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                    queryBuilder.Result = ConstantValue.ok;
                    queryBuilderList.Add(queryBuilder);
                }
                notholser = false;
                */
            }
            //Caso in cui la company non ha una entity bhol/bser
            if (notholser)
            {

                if (npagbl)
                {
                    //nuovo sviluppo exception
                    List<ExceptionTable> exceptions = new List<ExceptionTable>();
                    Task<List<ExceptionTable>> exceptionsTask = exceptionTableAdapter.GetExceptions();
                    exceptionsTask.Wait();

                    if (exceptionsTask.IsCompleted)
                    {
                        exceptions = exceptionsTask.Result;
                    }
                    //verifico che la uorg rientri un una eccezione nota
                    bool isException = IsException(exceptions, orgUnit, company, npaList);
                    if (isException)
                    {
                        List<NotPrevLine> npaListException = notPrevLine.GetListNpaEception(orgUnit, activityMappingAdapter);

                        List<NotPrevLine> npaCompatibili = npaListException.Where(npa => CheckNpaCoerence(npa.PrevalentBl, company)).ToList();

                        queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaCompatibili, prevalentCode, false, true);
                        List<QueryBuilder> queryBuilderListAppo = new List<QueryBuilder>();

          

                        foreach (QueryBuilder queryBuilder in queryBuilderList)
                        {
                            if (string.IsNullOrEmpty(queryBuilder.MacroOrg1))
                            {
                                //   foreach (NotPrevLine np in npaListException)
                             if (npaCompatibili.Count > 0){
                              foreach (NotPrevLine np in npaCompatibili) {
                               if (!np.PrevalentBl.ToUpper().Contains("GBL")){
                                        QueryBuilder queryBuilderAppo = new QueryBuilder();
                                            queryBuilderAppo.MacroOrg2 = queryBuilder.MacroOrg2;
                                            queryBuilderAppo.Organization = queryBuilder.Organization;
                                            queryBuilderAppo.Process = queryBuilder.Process;
                                            queryBuilderAppo.Vcs = queryBuilder.Vcs;
                                            queryBuilderAppo.OrganizationDescription = queryBuilder.OrganizationDescription;
                                            queryBuilderAppo.ProcessDescription = queryBuilder.ProcessDescription;
                                            queryBuilderAppo.VcsDescription = queryBuilder.VcsDescription;
                                            queryBuilderAppo.MacroOrg1 = np.PrevalentBl;
                                            queryBuilderAppo.Result = ConstantValue.ok;
                                            queryBuilderListAppo.Add(queryBuilderAppo);
                                }
                              }
                              }
                             else {
                                    QueryBuilder queryBuilderAppo = new QueryBuilder();
                                    queryBuilderAppo.MacroOrg2 = queryBuilder.MacroOrg2;
                                    queryBuilderAppo.Organization = queryBuilder.Organization;
                                    queryBuilderAppo.Process = queryBuilder.Process;
                                    queryBuilderAppo.Vcs = queryBuilder.Vcs;
                                    queryBuilderAppo.OrganizationDescription = queryBuilder.OrganizationDescription;
                                    queryBuilderAppo.ProcessDescription = queryBuilder.ProcessDescription;
                                    queryBuilderAppo.VcsDescription = queryBuilder.VcsDescription;
                                    queryBuilderAppo.MacroOrg1 = prevalentCode;
                                    queryBuilderAppo.Result = ConstantValue.ok;
                                    queryBuilderListAppo.Add(queryBuilderAppo);                              
                             }
                            }
                         }
                        if (queryBuilderListAppo.Count > 0)
                            queryBuilderList = queryBuilderListAppo;
                    }
                    else
                    {
                        bool checkNpa = true;
                        //Verifico per ogni npa se è coerente con la company di appartenenza
                        foreach (NotPrevLine npa in npaList)
                        {
                            if (npa.objectTypeId == Convert.ToUInt32(ConstantValue.businesslinecode))
                            {
                                checkNpa = CheckNpaCoerence(npa.PrevalentBl, company);
                                if (!checkNpa)
                                    break;
                            }
                            if (npa.objectTypeId == Convert.ToUInt32(ConstantValue.servicecode))
                            {
                                checkNpa = CheckNpaCoerence(npa.PrevalentBl, company);
                                if (!checkNpa)
                                    break;
                            }
                        }

                        if (checkNpa)
                        {
                            //Controllo se la notprevalent sia di tipo MBL solo per le sole businessline (30)
                            if (npaList[0].objectTypeId.ToString() == ConstantValue.businesslinecode && CheckMultiBl(npaList))
                            {//Verifico che la company abbia valorizzato a 1 iSER
                                if (CoerenceBSer(company.BusinessLines))
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, false, false);
                                }
                                else
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, false, false);
                                    //Forzo la macroorg1 come la gbl Prevalent
                                    foreach (QueryBuilder queryBuilder in queryBuilderList)
                                    {
                                        queryBuilder.MacroOrg1 = prevalentCode;
                                    }
                                }
                            }
                            else
                            {
                                // NO MultiBusiness
                                queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, false, false);

                                foreach (QueryBuilder queryBuilder in queryBuilderList)
                                {
                                    queryBuilder.MacroOrg1 = npaList[0].PrevalentBl;
                                }

                            }
                        }
                        else
                        {
                            //queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
                            // indra: qui dovrei mettere altra query non il ko
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                activityMapping.MacroOrg1.MacroOrg1Code = GetPrevalentLineCompany(company.BusinessLines);
                                QueryBuilder queryBuilder = GetQueryBuilder(activityMapping, company);
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
                else
                {
                    // DXC Maggio 2022
                    // List<NotPrevLine> npaGbl = notPrevLine.GetListNpaBl(orgUnit);
                    // List<NotPrevLine> npaServList = notPrevLine.GetListNpaSe(orgUnit, activityMappingAdapter);
                    // DXC Maggio 2022: devo considerare anche le npa service oltre alle Bl
                    if (npaBlList.Count + npaSeList.Count > 0)
                    {
                        // DXC Maggio 2022: check npa GCO disabilitato
                        /*
                        // CR Indra: GCO
                        if (CheckNpaGco(npaList))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                if (isLatam)
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, "BREL", true, false);
                                }
                                else
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, "BRET", true, false);
                                }
                            }
                        }
                        else
                        */
                        if (CoerenceBSer(company.BusinessLines))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, ConstantValue.bser, true, false);
                            }
                        }
                        else
                        {
                            queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, false, false);
                            //Forzo la macroorg1 come la gbl Prevalent
                            queryBuilderList.ForEach(q => q.MacroOrg1 = prevalentCode);

                        }
                    }
                    else
                    {
                        // DXC Maggio 2022: check npa GCO disabilitato
                        /*
                        // CR Indra: GCO
                        if (CheckNpaGco(npaList))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                if (isLatam)
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, "BREL", true, false);
                                }
                                else
                                {
                                    queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, "BRET", true, false);
                                }
                            }
                        }
                        else 
                        */
                        if (CoerenceBSer(company.BusinessLines))
                        {
                            foreach (ActivityMapping activityMapping in activityMappingList)
                            {
                                queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, ConstantValue.bser, true, false);
                            }
                        }
                        else
                        {
                            queryBuilderList = SwitchPaServiceCase(orgUnit, activityMappingList, company, npaList, prevalentCode, false, false);
                            //Forzo la macroorg1 come la gbl Prevalent
                            queryBuilderList.ForEach(q => q.MacroOrg1 = prevalentCode);
                        }
                    }

                }
            }

            List<QueryBuilder> uniqueL = queryBuilderList
            .GroupBy(q => new { q.Company, q.MacroOrg1, q.MacroOrg2, q.Vcs, q.VcsDescription, q.Perimeter, q.Process, q.ProcessDescription, q.Organization, q.OrganizationDescription, q.Result })
            .Select(g => g.First())
            .ToList();
            return uniqueL;
        }
        private List<QueryBuilder> SwitchPaServiceCase(HrmasterdataOu orgUnit, List<ActivityMapping> activityMappingList, CompanyRules company, List<NotPrevLine> nPA, string macroOrg1, bool isBserBhol, bool isExce)
        {
            List<ActivityAssociationGroup> actAssList = new List<ActivityAssociationGroup>();
            Task<List<ActivityAssociationGroup>> activityAssociation = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, orgUnit.AttrPrevCod, orgUnit.PredAttr);
            activityAssociation.Wait();
            List<QueryBuilder> queryBuilderList = new List<QueryBuilder>();

            string prevalentCode = GetPrevalentLineCompany(company.BusinessLines);

            if (activityAssociation.IsCompleted)
            {
                if (activityAssociation.Result != null)
                    actAssList = activityAssociation.Result;
            }

            if (actAssList != null && actAssList.Count > 0)
            {
                if (CheckPrevalent(actAssList[0].TypologyObject, ConstantValue.sesec))
                {
                    foreach (ActivityMapping activityMapping in activityMappingList)
                    {
                        QueryBuilder queryBuilder = new QueryBuilder();
                        if (!isExce)
                            queryBuilder = GetQueryBuilder(activityMapping, company);
                        else
                            queryBuilder = GetQueryBuilderException(activityMapping, company, string.Empty);
                        if (isBserBhol)
                            queryBuilder.MacroOrg1 = macroOrg1;
                        queryBuilder.Result = ConstantValue.ok;
                        queryBuilderList.Add(queryBuilder);
                    }
                }
                //se PA risale a PRO
                /* codice del controllo sul parimetro tolto su richiesta */
                // INIZIO gestione caso BpcCode PA in (GDS,DGF,DGH,CHP)
                else if (CheckPrevalent(actAssList[0].BpcCode, ConstantValue.gds.ToString()) ||
                        actAssList[0].BpcCode.Equals(ConstantValue.digfac) ||
                        actAssList[0].BpcCode.Equals(ConstantValue.dighub) ||
                        actAssList[0].BpcCode.Equals(ConstantValue.chapter) ||
                        ConstantValue.gco_constants.Split(";")[0].Contains(actAssList[0].BpcCode)
                        )

                {
                    // Se la Prevalent Activity rientra nella tipologio digital factory, digital hub o chapter 
                    // filtro anche per la notPrevalent dal catalogo delle attvità

                    if (nPA.Count > 0)
                    {
                        foreach (NotPrevLine notP in nPA)
                        {
                            // check debug NPA 
                            // Logger.LogInformation(string.Format("DEBUG - check NPA 2 {0}", ConstantValue.gco_constants.Split(";")[2]));
                            // Logger.LogInformation(string.Format("DEBUG - check NPA 2 {0} <- {1}", ConstantValue.gco_constants.Split(";")[2].Contains(notP.BpcCode),notP.BpcCode));
                            
                            //ulteriore filtro per le NotPrevalent che devono rientrare in una specifica tipologia
                            if (CheckPrevalent(notP.BpcCode, ConstantValue.afc) ||
                            CheckPrevalent(notP.BpcCode,ConstantValue.peo) ||
                            CheckPrevalent(notP.BpcCode,ConstantValue.communications) ||
                            CheckPrevalent(notP.BpcCode,ConstantValue.procurement) ||
                            ConstantValue.gco_constants.Split(";")[2].Contains(notP.BpcCode))
                            {
                                foreach (ActivityMapping activityMapping in activityMappingList)
                                {
                                    if (/*activityMapping.NotPrevalent.ToUpper().Contains(notP.objectDesc.ToUpper())*/
                                        (activityMapping.NotPrevalent != null && notP.BpcCode != null) &&
                                        (activityMapping.NotPrevalent.ToUpper().Contains(notP.BpcCode.ToUpper()) ||
                                        activityMapping.NotPrevalent.ToUpper().Contains(notP.PrevalentBl.ToUpper())) )
                                    {
                                        QueryBuilder queryBuilder = new QueryBuilder();
                                        if (!isExce)
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                        else
                                            queryBuilder = GetQueryBuilderException(activityMapping, company, notP.PrevalentBl);
                                        if (isBserBhol)
                                            queryBuilder.MacroOrg1 = macroOrg1;
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }
                            }
                            else
                            {
                                foreach (ActivityMapping activityMapping in activityMappingList)
                                {
                                    if (/*activityMapping.NotPrevalent.ToUpper().Contains(notP.objectDesc.ToUpper())*/
                                        (activityMapping.NotPrevalent != null && notP.BpcCode != null) &&
                                        (activityMapping.NotPrevalent.ToUpper().Contains(notP.BpcCode.ToUpper()) ||
                                        activityMapping.NotPrevalent.ToUpper().Contains(notP.PrevalentBl.ToUpper())) )
                                    {
                                        QueryBuilder queryBuilder = new QueryBuilder();
                                        if (!isExce)
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                        else
                                            queryBuilder = GetQueryBuilderException(activityMapping, company, notP.PrevalentBl);
                                        if (isBserBhol)
                                            queryBuilder.MacroOrg1 = macroOrg1;
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (ActivityMapping activityMapping in activityMappingList)
                        {
                            if (activityMapping.NotPrevalent.Equals(""))
                            {
                                QueryBuilder queryBuilder = new QueryBuilder();
                                if (!isExce)
                                    queryBuilder = GetQueryBuilder(activityMapping, company);
                                else
                                    queryBuilder = GetQueryBuilderException(activityMapping, company, string.Empty);
                                if (isBserBhol)
                                    queryBuilder.MacroOrg1 = macroOrg1;
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
                // FINE gestione caso BpcCode PA in (GDS,DGF,DGH,CHP)
                // INIZIO gestione caso BpcCode PA in (PRO, SMD)
                else if (actAssList[0].BpcCode.Equals(ConstantValue.procurement) ||
                        actAssList[0].BpcCode.Equals(ConstantValue.smd) ||
                        ConstantValue.gco_constants.Split(";")[1].Contains(actAssList[0].BpcCode)
                        )
                {
                    // Se la Prevalent Activity rientra nella tipologio procurement o smd, 
                    // filtro anche per la notPrevalent dal catalogo delle attvità

                    if (nPA.Count > 0)
                    {
                        foreach (NotPrevLine notP in nPA)
                        {
                            // check debug NPA 
                            // Logger.LogInformation(string.Format("DEBUG - check NPA 3 {0}", ConstantValue.gco_constants.Split(";")[3]));
                            // Logger.LogInformation(string.Format("DEBUG - check NPA 3 {0} <- {1}", ConstantValue.gco_constants.Split(";")[3].Contains(notP.BpcCode),notP.BpcCode));

                            //ulteriore filtro per le NotPrevalent che devono rientrare in una specifica tipologia
                            // GCO, GDS, GAT, GBI, GCC, GCH, GCR, GCS, GFR, GOI ...
                            if (CheckPrevalent(notP.BpcCode, ConstantValue.gco) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gds) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gat) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gbi) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gcc) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gch) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gcr) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gcs) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gfr) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.goi) ||
                                CheckPrevalent(notP.BpcCode, ConstantValue.gpq) ||
                                ConstantValue.gco_constants.Split(";")[3].Contains(notP.BpcCode)
                                )
                            {
                                foreach (ActivityMapping activityMapping in activityMappingList)
                                {
                                    if (/*activityMapping.NotPrevalent.ToUpper().Contains(notP.objectDesc.ToUpper())*/
                                        (activityMapping.NotPrevalent != null && notP.BpcCode != null) &&
                                        (activityMapping.NotPrevalent.ToUpper().Contains(notP.BpcCode.ToUpper()) ||
                                        activityMapping.NotPrevalent.ToUpper().Contains(notP.PrevalentBl.ToUpper())))
                                    {
                                        QueryBuilder queryBuilder = new QueryBuilder();
                                        if (!isExce)
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                        else
                                            queryBuilder = GetQueryBuilderException(activityMapping, company, notP.PrevalentBl);
                                        if (isBserBhol)
                                            queryBuilder.MacroOrg1 = macroOrg1;
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }
                            }
                            else
                            {
                                foreach (ActivityMapping activityMapping in activityMappingList)
                                {
                                    if (/*activityMapping.NotPrevalent.ToUpper().Contains(notP.objectDesc.ToUpper())*/
                                        (activityMapping.NotPrevalent != null && notP.BpcCode != null) &&
                                        (activityMapping.NotPrevalent.ToUpper().Contains(notP.BpcCode.ToUpper()) ||
                                        activityMapping.NotPrevalent.ToUpper().Contains(notP.PrevalentBl.ToUpper())))
                                    {
                                        QueryBuilder queryBuilder = new QueryBuilder();
                                        if (!isExce)
                                            queryBuilder = GetQueryBuilder(activityMapping, company);
                                        else
                                            queryBuilder = GetQueryBuilderException(activityMapping, company, notP.PrevalentBl);
                                        if (isBserBhol)
                                            queryBuilder.MacroOrg1 = macroOrg1;
                                        queryBuilder.Result = ConstantValue.ok;
                                        queryBuilderList.Add(queryBuilder);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (ActivityMapping activityMapping in activityMappingList)
                        {
                            if (activityMapping.NotPrevalent.Equals("") || activityMapping.NotPrevalent.Equals(prevalentCode))
                            {
                                QueryBuilder queryBuilder = new QueryBuilder();
                                if (!isExce)
                                    queryBuilder = GetQueryBuilder(activityMapping, company);
                                else
                                    queryBuilder = GetQueryBuilderException(activityMapping, company, string.Empty);
                                if (isBserBhol)
                                    queryBuilder.MacroOrg1 = macroOrg1;
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
                // FINE gestione caso BpcCode PA in (PRO, SMD)
                else
                {
                    foreach (NotPrevLine notP in nPA)
                    {
                        foreach (ActivityMapping activityMapping in activityMappingList)
                        {
                            if (/*activityMapping.NotPrevalent.ToUpper().Contains(notP.objectDesc.ToUpper())*/
                                (activityMapping.NotPrevalent != null && notP.BpcCode != null) &&
                                (activityMapping.NotPrevalent.ToUpper().Contains(notP.BpcCode.ToUpper()) ||
                                activityMapping.NotPrevalent.ToUpper().Contains(notP.PrevalentBl.ToUpper())) )
                            {
                                QueryBuilder queryBuilder = new QueryBuilder();
                                if (!isExce)
                                    queryBuilder = GetQueryBuilder(activityMapping, company);
                                else
                                    queryBuilder = GetQueryBuilderException(activityMapping, company, string.Empty);
                                if (isBserBhol)
                                    queryBuilder.MacroOrg1 = macroOrg1;
                                queryBuilder.Result = ConstantValue.ok;
                                queryBuilderList.Add(queryBuilder);
                            }
                        }
                    }
                }
                if (queryBuilderList.Count == 0)
                {
                    foreach (ActivityMapping activityMapping in activityMappingList)
                    {

                        if (activityMapping.NotPrevalent.Equals(""))
                        {
                            QueryBuilder queryBuilder = new QueryBuilder();
                            if (!isExce)
                                queryBuilder = GetQueryBuilder(activityMapping, company);
                            else
                                queryBuilder = GetQueryBuilderException(activityMapping, company, string.Empty);
                            if (isBserBhol)
                                queryBuilder.MacroOrg1 = macroOrg1;
                            queryBuilder.Result = ConstantValue.ok;
                            queryBuilderList.Add(queryBuilder);
                        }
                    }
                }
            }
            else
            {
                queryBuilderList.Add(new QueryBuilder(ConstantValue.ko));
            }
            List<QueryBuilder> uniqueList = queryBuilderList.Distinct().ToList();
            //return queryBuilderList;
            return uniqueList;
        }
        #endregion

        #region Controllo coerenza Company Unità organizzativa
        //Metodo che verifica la coerenza tra company e attività non prevalenti bhol e bser
        private bool CoerenceBHolBSer(ICollection<BusinessLines> bsCompany)
        {
            bool isCoerent = false;
            foreach (BusinessLines bs in bsCompany)
            {
                if (bs.Prevalent == 0 && bs.BusinessLinesName.ToUpper().Equals(ConstantValue.iser) || bs.Prevalent == 0 && bs.BusinessLinesName.ToUpper().Equals(ConstantValue.ihol))
                    isCoerent = true;
            }
            return isCoerent;
        }

        //Metodo per settare la MacroOrg1 a bhol o bser in base alla company
        private string GetBHolOrBSer(ICollection<BusinessLines> bsCompany)
        {
            string macroOrg1 = string.Empty;
            foreach (BusinessLines bs in bsCompany)
            {
                if (bs.Prevalent == 0 && bs.BusinessLinesName.ToUpper().Equals(ConstantValue.iser))
                    macroOrg1 = ConstantValue.bser;
                else if (bs.Prevalent == 0 && bs.BusinessLinesName.ToUpper().Equals(ConstantValue.ihol))
                    macroOrg1 = ConstantValue.bhol;
            }
            return macroOrg1;
        }

        //Verifica ugualianza tra attività prevalente company e notPrevalent unità organizzativa
        public bool GBLPrevIsEqualsToNotPrevBL(string GBLPrevalent, List<NotPrevLine> notPrevLines)
        {
            bool areEqual = false;
            if (!string.IsNullOrEmpty(GBLPrevalent))
            {
                foreach (NotPrevLine npa in notPrevLines)
                {
                    if (GBLPrevalent.ToUpper().Equals(npa.PrevalentBl.ToUpper()))
                    {
                        areEqual = true;
                        break;
                    }
                }
            }
            else
                areEqual = false;

            return areEqual;

        }
        #endregion

        #region Gestione Centri di costo


        public Association GetCCFinished(List<BwMasterObject> cc, Association association, bool cdcDummy)
        {
            int i = 1;
            foreach (BwMasterObject ccReal in cc)
            {
                if (ccReal.typeCostCenter.Equals("R"))
                {

                }
                else if (!cdcDummy)
                {
                    string codice = getCompatibleCode();
                    ccReal.costCenterCode = "COMP" + ccReal.companyCodeAFC + codice + i;
                    i++;
                }
                else
                {
                    string code = getFictCode(ccReal);
                    ccReal.costCenterCode = code;
                    ccReal.typeCostCenter = "F";
                    ccReal.costCenterDescription = ConstantValue.ficticious;
                }
                association.Centers = cc;
            }
            return association;
        }
        public List<BwMasterObject> GetCCResult(List<QueryBuilder> queryBuilders, bool real, List<Responsability> responsabilities, CompanyRules company,
                                                Association association, HrmasterdataOu organizationalUnit, List<BwMasterObject> cc)
        {

            List<BwMasterObject> ccCompatibile = new List<BwMasterObject>();
            foreach (QueryBuilder queryBuilder in queryBuilders)
            {
                real = false;
                if (queryBuilder.Result == ConstantValue.ok)
                {
                    if (queryBuilder.MacroOrg1 != null && queryBuilder.MacroOrg2 != null)
                    {
                        queryBuilder.MacroOrg2 = queryBuilder.MacroOrg2.ToUpper();
                        Logger.LogInformation(string.Format("Recupero CC {0}", DateTime.Now.ToString()));

                        Task<List<BwMasterObject>> CostCenterListTask = bwSyncAdapter.GetBwMasterDatas(CancellationToken.None, queryBuilder.MacroOrg1.ToUpper(), queryBuilder.MacroOrg2, queryBuilder.Vcs, company.AfcCompanyCode, queryBuilder.Process, queryBuilder.Organization);
                        CostCenterListTask.Wait();
                        List<BwMasterObject> costCentersList = CostCenterListTask.Result;

                        if (CostCenterListTask.IsCompleted && costCentersList != null && costCentersList.Count() > 0)
                        {
                            foreach (BwMasterObject bwCC in costCentersList)
                            {
                                bwCC.endDateCostCenter = formatterDateCC(bwCC.endDateCostCenter);
                                bwCC.startDateCostCenter = formatterDateCC(bwCC.startDateCostCenter);

                                bwCC.companyAFC = company.SapHrDesc;
                                bwCC.NewPrimoCode = company.NewPrimoCode;

                                if (bwCC.typeCostCenter.Equals("R"))
                                {
                                    if (responsabilities.Count > 0)
                                    {
                                        foreach (Responsability resp in responsabilities)
                                        {
                                            if (bwCC.resp == resp.ResponsabilityCode)
                                            {
                                                bwCC.resp = resp.ResponsabilityCode;
                                                bwCC.vcsDescription = queryBuilder.VcsDescription;
                                                bwCC.organizationDescription = queryBuilder.OrganizationDescription;
                                                //bwCC.processGlobal = queryBuilder.Process;
                                                bwCC.processDescription = queryBuilder.ProcessDescription;
                                                bwCC.countryAFC = company.Perimeter.PerimeterDesc;
                                                real = true;
                                                cc.Add(bwCC);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bwCC.countryAFC = company.Perimeter.PerimeterDesc;
                                        bwCC.vcsDescription = queryBuilder.VcsDescription;
                                        bwCC.organizationDescription = queryBuilder.OrganizationDescription;
                                        //bwCC.processGlobal = queryBuilder.Process;
                                        bwCC.processDescription = queryBuilder.ProcessDescription;
                                        real = true;
                                        cc.Add(bwCC);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (responsabilities.Count > 0)
                            {
                                List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                foreach (Responsability resp in responsabilities)
                                {

                                    ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", resp.ResponsabilityCode));
                                    real = true;
                                }
                            }
                            else
                            {
                                List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", ""));
                                real = true;
                            }
                        }
                    }
                    else
                    {
                        if (responsabilities.Count > 0)
                        {
                            List<BwMasterObject> costCenter = new List<BwMasterObject>();
                            foreach (Responsability resp in responsabilities)
                            {

                                ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", resp.ResponsabilityCode));
                                real = true;
                            }
                        }
                        else
                        {
                            List<BwMasterObject> costCenter = new List<BwMasterObject>();
                            ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", ""));
                            real = true;
                        }
                    }
                }
                else
                {
                    real = true;
                    association.TypologyAssociation = ConstantValue.ko;
                    List<BwMasterObject> costCenter = new List<BwMasterObject>();
                    association.Centers = costCenter;
                }

                if (!real)
                {
                    if (responsabilities.Count > 0)
                    {
                        List<BwMasterObject> costCenter = new List<BwMasterObject>();
                        foreach (Responsability resp in responsabilities)
                        {

                            ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", resp.ResponsabilityCode));
                        }
                    }
                    else
                    {
                        List<BwMasterObject> costCenter = new List<BwMasterObject>();
                        ccCompatibile.Add(CreateCC(organizationalUnit, company, queryBuilder, "C", "example", ""));
                    }
                }
            }

            cc.AddRange(ccCompatibile);
            return cc;

        }

        private Association GetCCCleaner(Association association, List<QueryBuilder> queryBuilderList, CompanyRules company, List<Responsability> responsabilityList, int i)
        {
            association.Centers = new List<BwMasterObject>();

            foreach (QueryBuilder queryBuilder in queryBuilderList)
            {

                if (queryBuilder.Result == ConstantValue.ok)
                {
                    if (responsabilityList != null && responsabilityList.Count > 0)
                    {
                        foreach (Responsability r in responsabilityList)
                        {
                            association.Centers.Add(new BwMasterObject
                            {
                                macroOrg1 = queryBuilder.MacroOrg1,
                                macroOrg2 = queryBuilder.MacroOrg2,
                                vcs = queryBuilder.Vcs,
                                vcsDescription = queryBuilder.VcsDescription,
                                companyCodeAFC = company.AfcCompanyCode,
                                companyAFC = company.SapHrDesc,
                                processGlobal = queryBuilder.Process,
                                processDescription = queryBuilder.ProcessDescription,
                                organization = queryBuilder.Organization,

                                organizationDescription = queryBuilder.OrganizationDescription,
                                countryAFC = company.Perimeter.PerimeterDesc,
                                resp = r.ResponsabilityCode

                            });
                        }
                    }
                    else
                    {
                        association.Centers.Add(new BwMasterObject
                        {
                            macroOrg1 = queryBuilder.MacroOrg1,
                            macroOrg2 = queryBuilder.MacroOrg2,
                            vcs = queryBuilder.Vcs,
                            vcsDescription = queryBuilder.VcsDescription,
                            companyCodeAFC = company.AfcCompanyCode,
                            companyAFC = company.SapHrDesc,
                            processGlobal = queryBuilder.Process,
                            processDescription = queryBuilder.ProcessDescription,
                            organization = queryBuilder.Organization,

                            organizationDescription = queryBuilder.OrganizationDescription,
                            countryAFC = company.Perimeter.PerimeterDesc,
                            resp = ""

                        });

                    }
                }
            }

            return association;
        }

        public AssociationResult getCCForCleaner(HrmasterdataOuList orgUnitList)
        {

            List<HrmasterdataOu> hrmasterdatas = orgUnitList.hrmasterdataou;
            if (hrmasterdatas == null || hrmasterdatas.Count == 0)
                return null;

            AssociationResult associationResult = new AssociationResult();
            associationResult.AssociationList = new List<Association>();

            try
            {
                foreach (HrmasterdataOu organizationalUnit in hrmasterdatas)
                {
                    CultureInfo myCIintl = new CultureInfo("us-US", false);
                    Association association = new Association();
                    association.Unit = organizationalUnit;
                    association.Centers = new List<BwMasterObject>();

                    organizationalUnit.StartDate = formatterDateOU(organizationalUnit.StartDate);
                    organizationalUnit.EndDate = formatterDateOU(organizationalUnit.EndDate);

                    List<CompanyRules> companyRules = new List<CompanyRules>();
                    NotPrevLine notPrevLine = new NotPrevLine();
                    List<NotPrevLine> npaList = notPrevLine.GetListNpa(organizationalUnit, activityMappingAdapter);

                    bool esisteTranscodifica = true;
                    bool isItaGlo = false;
                    ItaGlo itaGlo = new ItaGlo();

                    Task<ItaGlo> itaGloResult = itaGloAdapter.GetItaGloInfo(CancellationToken.None, organizationalUnit.Company);
                    itaGloResult.Wait();


                    if (itaGloResult != null)
                    {
                        if (itaGloResult.Result != null && itaGloResult.Result.SapHrGlobalCode != null)
                        {
                            isItaGlo = true;
                        }
                    }

                    if (isItaGlo)
                    {
                        ActivityAssociationGroup actAss = GetActivityAssociation(organizationalUnit);

                        if (actAss.TypologyObject != null)
                        {
                            if (!actAss.TypologyObject.Equals(ConstantValue.gds) && !organizationalUnit.PerimeterDesc.Equals(ConstantValue.italiaperdesc))
                            {
                                if (organizationalUnit.CompanyDesc.Contains(ConstantValue.enelitalia)
                                    && !CheckProcurement(npaList, ConstantValue.procurement)
                                    && !actAss.TypologyObject.Equals(ConstantValue.procurement))
                                {
                                    companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.italiaperdesc);

                                }
                                else
                                {
                                    companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.centralperdesc);
                                }
                            }
                            else
                            {
                                companyRules = getCompanyRulesByPerimeterDesc(organizationalUnit, ConstantValue.italiaperdesc);
                            }
                        }
                        else
                        {
                            esisteTranscodifica = false;
                        }
                    }
                    else
                    {
                        companyRules = getCompanyRulesBySapHr(organizationalUnit);
                    }
                    List<CompanyRules> appoCompany = new List<CompanyRules>();
                    if (companyRules.Count == 0)
                    {
                        appoCompany = GetCompanyByDesc(organizationalUnit);
                        companyRules.AddRange(appoCompany);
                        if (companyRules.Count == 0)
                            esisteTranscodifica = false;
                    }


                    if (esisteTranscodifica)
                    {
                        foreach (CompanyRules company in companyRules)
                        {
                            if (company.FlagConsolidation.ToUpper().Contains("TRUE"))
                            {
                                List<Responsability> responsabilityList = getResponsability(company.NewPrimoCode);


                                if (string.IsNullOrEmpty(company.AfcCompanyCode))

                                    organizationalUnit.Country = company.CodeNation.NationAcronym;

                                List<QueryBuilder> queryBuilderList = new List<QueryBuilder>();
                                queryBuilderList = SwitchPrevalentActivity(organizationalUnit, company, queryBuilderList, npaList);

                                if (queryBuilderList != null && queryBuilderList.Count > 0)
                                {
                                    foreach (QueryBuilder queryBuilder in queryBuilderList)
                                    {
                                        if (queryBuilder.Result == ConstantValue.ok)
                                        {
                                            if (responsabilityList != null && responsabilityList.Count > 0)
                                            {
                                                foreach (Responsability r in responsabilityList)
                                                {
                                                    association.Centers.Add(new BwMasterObject()
                                                    {
                                                        macroOrg1 = queryBuilder.MacroOrg1,
                                                        macroOrg2 = queryBuilder.MacroOrg2,
                                                        vcs = queryBuilder.Vcs,
                                                        vcsDescription = queryBuilder.VcsDescription,
                                                        companyCodeAFC = company.AfcCompanyCode,
                                                        companyAFC = company.SapHrDesc,
                                                        processGlobal = queryBuilder.Process,
                                                        processDescription = queryBuilder.ProcessDescription,
                                                        organization = queryBuilder.Organization,
                                                        organizationDescription = queryBuilder.OrganizationDescription,
                                                        countryAFC = company.Perimeter.PerimeterDesc,
                                                        resp = r.ResponsabilityCode
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                association.Centers.Add(new BwMasterObject()
                                                {
                                                    macroOrg1 = queryBuilder.MacroOrg1,
                                                    macroOrg2 = queryBuilder.MacroOrg2,
                                                    vcs = queryBuilder.Vcs,
                                                    vcsDescription = queryBuilder.VcsDescription,
                                                    companyCodeAFC = company.AfcCompanyCode,
                                                    companyAFC = company.SapHrDesc,
                                                    processGlobal = queryBuilder.Process,
                                                    processDescription = queryBuilder.ProcessDescription,
                                                    organization = queryBuilder.Organization,
                                                    organizationDescription = queryBuilder.OrganizationDescription,
                                                    countryAFC = company.Perimeter.PerimeterDesc,
                                                    resp = ""
                                                });
                                            }
                                        }
                                        else
                                        {

                                            List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                            association.Centers = costCenter;
                                        }
                                    }
                                    associationResult.AssociationList.Add(association);
                                }
                            }
                            else
                            {
                                List<BwMasterObject> costCenter = new List<BwMasterObject>();
                                association.Centers = costCenter;
                                associationResult.AssociationList.Add(association);
                            }
                        }
                    }
                    else
                    {
                        List<BwMasterObject> costCenter = new List<BwMasterObject>();
                        association.Centers = costCenter;
                        associationResult.AssociationList.Add(association);
                    }
                }
                return associationResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        private string getCompatibleCode()
        {
            string result = "";

            string giorno = DateTime.Now.Day.ToString();
            string mese = DateTime.Now.Month.ToString();
            string anno = DateTime.Now.Year.ToString();
            string ora = DateTime.Now.Hour.ToString();
            string min = DateTime.Now.Minute.ToString();
            string sec = DateTime.Now.Second.ToString();
            return result = giorno + mese + anno + ora + min + sec;
            // return result = ora + min + sec;
        }

        private string getFictCode(BwMasterObject cc)
        {
            string result = string.Empty;
            int seq = 0;

            try
            {
                string prefix = cc.NewPrimoCode.Substring(2, 4);
                seq = sequenceData.GetSequence();
                string suffix = Convert.ToString(seq).PadLeft(6, '0');
                result = prefix + suffix;
            }
            catch (Exception ex)
            {
                Logger.LogError("No able to retrieve sequence number for fictitious: " + ex);
            }



            return result;
        }

        private string formatterDateOU(string date)
        {
            string result = null;

            DateTime dateFormatted;
            DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFormatted);
            result = dateFormatted.ToString("yyyy-MM-dd");

            return result;
        }

        private string formatterDateCC(string date)
        {
            //string result = null;

            //DateTime dateFormatted;
            date = date.Insert(4, "-");
            date = date.Insert(7, "-");
            //DateTime.TryParseExact(date, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFormatted);
            //result = dateFormatted.ToString("yyyy-MM-dd");

            return date;
        }

        private BwMasterObject CreateCC(HrmasterdataOu orgUnit, CompanyRules companyRules, QueryBuilder querybuilder, string type, string description, string primoResp)
        {
            BwMasterObject cc = new BwMasterObject(type, orgUnit, companyRules, querybuilder, description, primoResp);
            return cc;
        }

        private QueryBuilder GetQueryBuilder(ActivityMapping activityMapping, CompanyRules company)
        {
            QueryBuilder queryBuilder = new QueryBuilder();

            if (activityMapping.MacroOrg1 != null)
            {
                if (activityMapping.MacroOrg1.MacroOrg1Code.ToUpper().Contains(ConstantValue.gbl.ToUpper()))
                {
                    queryBuilder.MacroOrg1 = GetPrevalentLineCompany(company.BusinessLines);
                }
                else if (activityMapping.MacroOrg1.MacroOrg1Code.ToUpper().Contains(ConstantValue.bholBser.ToUpper()))
                {
                    queryBuilder.MacroOrg1 = GetBHolOrBSer(company.BusinessLines);
                }
                else
                {
                    queryBuilder.MacroOrg1 = activityMapping.MacroOrg1.MacroOrg1Code;
                }
            }

            if (activityMapping.MacroOrg2 != null)
                queryBuilder.MacroOrg2 = activityMapping.MacroOrg2.ActivityName;
            if (activityMapping.Vcs != null)
            {
                queryBuilder.Vcs = activityMapping.Vcs.VcsCode;
                queryBuilder.VcsDescription = activityMapping.Vcs.Desc;
            }

            if (activityMapping.ProcessGlobal != null)
            {
                queryBuilder.Process = activityMapping.ProcessGlobal.processCode;
                queryBuilder.ProcessDescription = activityMapping.ProcessGlobal.Desc;
            }

            if (activityMapping.Organization != null)
            {
                queryBuilder.Organization = activityMapping.Organization.OrganizationCode;
                queryBuilder.OrganizationDescription = activityMapping.Organization.Desc;
            }

            return queryBuilder;
        }

        //nuovo sviluppo exception
        private QueryBuilder GetQueryBuilderException(ActivityMapping activityMapping, CompanyRules company, string Prevalent)
        {
            QueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.MacroOrg1 = Prevalent;

            if (activityMapping.MacroOrg2 != null)
                queryBuilder.MacroOrg2 = activityMapping.MacroOrg2.ActivityName;
            if (activityMapping.Vcs != null)
            {
                queryBuilder.Vcs = activityMapping.Vcs.VcsCode;
                queryBuilder.VcsDescription = activityMapping.Vcs.Desc;
            }

            if (activityMapping.ProcessGlobal != null)
            {
                queryBuilder.Process = activityMapping.ProcessGlobal.processCode;
                queryBuilder.ProcessDescription = activityMapping.ProcessGlobal.Desc;
            }

            if (activityMapping.Organization != null)
            {
                queryBuilder.Organization = activityMapping.Organization.OrganizationCode;
                queryBuilder.OrganizationDescription = activityMapping.Organization.Desc;
            }
            return queryBuilder;
        }
        #endregion

        #region exception
        //nuovo sviluppo exception        
        public bool IsException(List<ExceptionTable> exceptions, HrmasterdataOu uorg, CompanyRules company, List<NotPrevLine> notPrevLine)
        {
            bool isException = false;
            string prevalentCode = GetPrevalentLineCompany(company.BusinessLines);
            // filtri le exception per HrmasterdataOu.AttrPrevCod
            // per ogni exception filtri nuovamente che exception.ValueAfcMacroorg1_1 = 
            exceptions = exceptions.Where(e => e.ValueTipoUo.Equals(uorg.PredAttr) && e.Active.Equals("1")).ToList();

            foreach (ExceptionTable except in exceptions)
            {
                if (isException)
                    break;
                if (!except.ValueGblPrevalent.Equals("1"))
                {
                    if (except.ValueGblPrevalent.Equals(prevalentCode))
                    {
                        //if (except.ValuePoObject.Equals('1'))
                        if (!except.ValuePoObject.Equals("1"))
                        {
                            foreach (BusinessLines bl in company.BusinessLines)
                            {

                                if (bl.Prevalent == 0)
                                {
                                    if (bl.BusinessLinesName.ToUpper().Contains(except.ValueNpa.ToUpper()))
                                    {
                                        isException = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (NotPrevLine npa in notPrevLine)
                            {
                                if (except.ValuePoObject == npa.codeobject.ToString())
                                {
                                    foreach (BusinessLines bl in company.BusinessLines)
                                    {

                                        if (bl.Prevalent == 0)
                                        {
                                            if (bl.BusinessLinesName.ToUpper().Contains(except.ValueNpa.ToUpper()))
                                            {
                                                isException = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    isException = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        isException = false;
                    }
                }
                else
                {
                    if (except.ValuePoObject.Equals('1'))
                    {
                        foreach (BusinessLines bl in company.BusinessLines)
                        {
                            if (bl.Prevalent == 0)
                            {
                                if (bl.BusinessLinesName.ToUpper().Contains(except.ValueNpa.ToUpper()))
                                {
                                    isException = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (NotPrevLine npa in notPrevLine)
                        {
                            if (except.ValuePoObject == npa.codeobject.ToString())
                            {
                                foreach (BusinessLines bl in company.BusinessLines)
                                {
                                    if (bl.Prevalent == 0)
                                    {
                                        if (bl.BusinessLinesName.ToUpper().Contains(except.ValueNpa.ToUpper()))
                                        {
                                            isException = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isException = false;
                            }
                        }
                    }
                }
            }
            return isException;
        }
        #endregion
    }
}