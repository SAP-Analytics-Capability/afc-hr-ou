using System;
using System.Collections.Generic;
using masterdata.Models;
using Microsoft.Extensions.Logging;
using masterdata.Interfaces;

public class ThreadClean
{

    private readonly List<CleanHrOU> HrOuList;
    private readonly IOuCCAssociation _CCAssociation;
    private readonly ILogger _Logger;
    private readonly IAssociationData _AssociationData;
    private readonly IFaManager _FaManager;


    public ThreadClean(List<CleanHrOU> hrOuList,
                       IOuCCAssociation cCAssociation,
                       ILogger logger,
                       IAssociationData associationData,
                       IFaManager faManager)
    {
        HrOuList = hrOuList;
        this._CCAssociation = cCAssociation;
        this._Logger = logger;
        this._AssociationData = associationData;
        this._FaManager = faManager;
    }
    public void ThreadProc()
    {
        if (_FaManager.GetCurrFA().inputType == FaConstants.xlsx)
        {
            foreach (CleanHrOU Ou in HrOuList)
            {
                try
                {
                    //compute each ou
                    HrmasterdataOuList hrmasterdataOuList = new HrmasterdataOuList();
                    hrmasterdataOuList.hrmasterdataou = new List<HrmasterdataOu>();
                    hrmasterdataOuList.hrmasterdataou.Add(new HrmasterdataOu().CloneMaster(Ou));

                    List<CleanBwCC> cleanBwList = new List<CleanBwCC>();

                    AssociationV3 association = new AssociationV3();
                    association.Centers = new List<CleanBwCC>();

                    AssociationResult result = new AssociationResult();

                    result = _CCAssociation.getCCForCleaner(hrmasterdataOuList);

                    if (result != null && result.AssociationList.Count > 0)
                    {
                        foreach (BwMasterObject centers in result.AssociationList[0].Centers)
                        {
                            cleanBwList.Add(new CleanBwCC() { uOrg = Ou.UOrg }.Clone(centers));
                        }

                        association.Centers = cleanBwList;

                        if (result.AssociationList[0].Centers != null && result.AssociationList[0].Centers.Count > 0)
                        {
                            foreach (CleanBwCC cc in association.Centers)
                            {

                                cc.faId = HrOuList[0].faId;
                                cc.organizationUnitId = Ou.hrmdou_id;
                                cc.IssueTime = _FaManager.GetCurrFA().issueTime;
                                _AssociationData.AddNewResultAssociationCleanCC(cc);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Logger.LogError($"Error while computing UO in ThreadClean: {ex.Message}");
                }
            }
        }
        else
        {
            //.txt

            List<HrmasterdataOu> hrmasterdataOu = new List<HrmasterdataOu>();
            int fail = 0;

            for (int i = 0; i < HrOuList.Count; i++)
            {

                try
                {
                    //saphr call to get ou's data
                    hrmasterdataOu.Add(_CCAssociation.getHrMasterDataOuCode(HrOuList[i].UOrg)[0]);
                    hrmasterdataOu[i - fail].hrmdou_id = HrOuList[i].hrmdou_id; // i minus fail to compensate when getHrMasterDataOuCode doesn't return NoContent()
                }
                catch (Exception ex)
                {
                    fail++;
                    _Logger.LogError($"Error while retrieve data from SAPHR with ou {HrOuList[i].UOrg[0]} : {ex.Message}");
                }

            }

            if (hrmasterdataOu != null && hrmasterdataOu.Count > 0)
            {
                foreach (HrmasterdataOu ou in hrmasterdataOu)
                {
                    try
                    {

                        //compute each ou
                        HrmasterdataOuList hrmasterdataOuList = new HrmasterdataOuList();
                        hrmasterdataOuList.hrmasterdataou = new List<HrmasterdataOu>();
                        hrmasterdataOuList.hrmasterdataou.Add(ou);

                        List<CleanBwCC> cleanBwList = new List<CleanBwCC>();

                        AssociationV3 association = new AssociationV3();
                        association.Centers = new List<CleanBwCC>();

                        CleanHrOU cleanOU = new CleanHrOU().Clone(ou);
                        cleanOU.faId = _FaManager.GetCurrFA().idFa;

                        AssociationResult result = new AssociationResult();
                        //result = _CCAssociation.getCCForCleaner(hrmasterdataOuList);
                        result = _CCAssociation.getCCForUO(hrmasterdataOuList, "", true);

                        if (result != null && result.AssociationList.Count > 0)
                        {
                            foreach (BwMasterObject centers in result.AssociationList[0].Centers)
                            {
                                cleanBwList.Add(new CleanBwCC() { uOrg = ou.UOrg }.Clone(centers));
                            }

                            association.Centers = cleanBwList;
                            int id = _AssociationData.UpdateAssociationCleanOU(cleanOU);

                            if (result.AssociationList[0].Centers != null && result.AssociationList[0].Centers.Count > 0)
                            {
                                association.Centers.ForEach(x => new Action(delegate ()
                                {
                                    x.organizationUnitId = id;
                                    x.faId = HrOuList[0].faId;
                                    x.IssueTime = _FaManager.GetCurrFA().issueTime;
                                    _AssociationData.AddNewResultAssociationCleanCC(x);
                                }).Invoke());

                                // foreach (CleanBwCC cc in association.Centers)
                                // {

                                //     cc.faId = HrOuList[0].faId;
                                //     cc.organizationUnitId = id;
                                //     cc.IssueTime = _FaManager.GetCurrFA().issueTime;
                                //     _AssociationData.AddNewResultAssociationCleanCC(cc);

                                // }
                            }
                        }
                        else
                        {
                            int id = _AssociationData.UpdateAssociationCleanOU(cleanOU); // to update ou without CCs
                        }
                    }
                    catch (Exception ex)
                    {
                        _Logger.LogError($"Error while computing ou({ou.UOrg}) : {ex.Message}");
                    }
                }
            }
        }
    }
}