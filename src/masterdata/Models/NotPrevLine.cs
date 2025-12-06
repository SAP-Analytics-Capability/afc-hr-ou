using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Helpers.Adapters;
using masterdata.Interfaces;
using masterdata.Helpers;

namespace masterdata.Models
{
    public class NotPrevLine
    {
        public int objectTypeId { get; set; }
        public int codeobject { get; set; }
        public string objectDesc { get; set; }
        public string PrevalentBl { get; set; }
        public string TypologyBl { get; set; }
        public string BpcCode { get; set; }
        public string MacroOrg2 { get; set; }
        public string Vcs { get; set; }
        public string PoObjectAbbr { get; set; }

        private readonly IActivityMappingAdapter activityMappingAdapter;
        private ConstantValue ConstantValue;
        public NotPrevLine(IActivityMappingAdapter activityMappingAdapter)
        {
            this.activityMappingAdapter = activityMappingAdapter;
        }
        public NotPrevLine()
        {
        }

        public List<NotPrevLine> GetListNpaBl(HrmasterdataOu orgUnit)
        {
            List<NotPrevLine> npaList = new List<NotPrevLine>();

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod1);
                notPrev.objectDesc = orgUnit.BusLineDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod2);
                notPrev.objectDesc = orgUnit.BusLineDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod3);
                notPrev.objectDesc = orgUnit.BusLineDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod4);
                notPrev.objectDesc = orgUnit.BusLineDesc4;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod5);
                notPrev.objectDesc = orgUnit.BusLineDesc5;
                npaList.Add(notPrev);
            }
            return npaList;
        }
        public List<NotPrevLine> GetListNpa(HrmasterdataOu orgUnit, IActivityMappingAdapter activityMappingAdapter)
        {
            List<NotPrevLine> npaList = new List<NotPrevLine>();

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod1);
                notPrev.objectDesc = orgUnit.BusLineDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod2);
                notPrev.objectDesc = orgUnit.BusLineDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod3);
                notPrev.objectDesc = orgUnit.BusLineDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod4);
                notPrev.objectDesc = orgUnit.BusLineDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.BusLineCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod5);
                notPrev.objectDesc = orgUnit.BusLineDesc5;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod1);
                notPrev.objectDesc = orgUnit.ServDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod2);
                notPrev.objectDesc = orgUnit.ServDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod3);
                notPrev.objectDesc = orgUnit.ServDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod4);
                notPrev.objectDesc = orgUnit.ServDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.ServCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod5);
                notPrev.objectDesc = orgUnit.ServDesc5;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod1);
                notPrev.objectDesc = orgUnit.StaffDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod2);
                notPrev.objectDesc = orgUnit.StaffDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod3);
                notPrev.objectDesc = orgUnit.StaffDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod4);
                notPrev.objectDesc = orgUnit.StaffDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.StaffCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod5);
                notPrev.objectDesc = orgUnit.StaffDesc5;
                npaList.Add(notPrev);
            }
            
            List<NotPrevLine> npaListAll = new List<NotPrevLine>();

            foreach (NotPrevLine npa in npaList)
            {
                Task<List<ActivityAssociationGroup>> activityAssociationTask = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, npa.codeobject.ToString(), npa.objectTypeId.ToString());
                activityAssociationTask.Wait();

                if (activityAssociationTask.IsCompleted)
                {
                    List<ActivityAssociationGroup> assGroup = activityAssociationTask.Result;

                    if (assGroup != null && assGroup.Count > 0)
                    {
                        foreach (ActivityAssociationGroup aa in assGroup)
                        {
                            // Controllo se la lista è vuota oppure non contiene la macroOrg1 
                            //allora aggiungo la NPA prendendo le prime informazioni dalla NPA e le seguenti dall' assGroup
                           
                            NotPrevLine nn = new NotPrevLine();
                            //nn.BpcCode = npa.BpcCode;
                            nn.codeobject = npa.codeobject;
                            nn.MacroOrg2 = npa.MacroOrg2;
                            nn.objectDesc = npa.objectDesc;
                            nn.objectTypeId = npa.objectTypeId;
                            nn.Vcs = npa.Vcs;

                            nn.TypologyBl = aa.TypologyObject;
                            nn.PrevalentBl = aa.MacroOrg1;
                            nn.BpcCode = aa.BpcCode;

                            npaListAll.Add(nn);
                        }
                    }
                }
            }

            return npaListAll;
        }

        public List<NotPrevLine> GetListNpaEception(HrmasterdataOu orgUnit, IActivityMappingAdapter activityMappingAdapter)
        {
            List<NotPrevLine> npaList = new List<NotPrevLine>();

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod1);
                notPrev.objectDesc = orgUnit.BusLineDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod2);
                notPrev.objectDesc = orgUnit.BusLineDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod3);
                notPrev.objectDesc = orgUnit.BusLineDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.BusLineCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod4);
                notPrev.objectDesc = orgUnit.BusLineDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.BusLineCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 30;
                notPrev.codeobject = Convert.ToInt32(orgUnit.BusLineCod5);
                notPrev.objectDesc = orgUnit.BusLineDesc5;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod1);
                notPrev.objectDesc = orgUnit.ServDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod2);
                notPrev.objectDesc = orgUnit.ServDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod3);
                notPrev.objectDesc = orgUnit.ServDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod4);
                notPrev.objectDesc = orgUnit.ServDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.ServCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod5);
                notPrev.objectDesc = orgUnit.ServDesc5;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod1);
                notPrev.objectDesc = orgUnit.StaffDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod2);
                notPrev.objectDesc = orgUnit.StaffDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod3);
                notPrev.objectDesc = orgUnit.StaffDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod4);
                notPrev.objectDesc = orgUnit.StaffDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.StaffCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod5);
                notPrev.objectDesc = orgUnit.StaffDesc5;
                npaList.Add(notPrev);
            }
            List<NotPrevLine> OtherNotPrev = new List<NotPrevLine>();

            foreach (NotPrevLine npa in npaList)
            {
                Task<List<ActivityAssociationGroup>> activityAssociationTask = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, npa.codeobject.ToString(), npa.objectTypeId.ToString());
                activityAssociationTask.Wait();

                if (activityAssociationTask.IsCompleted)
                {
                    List<ActivityAssociationGroup> assGroup = activityAssociationTask.Result;

                    if (assGroup != null && assGroup.Count > 0)
                    {
                        foreach (ActivityAssociationGroup asg in assGroup)
                        {
                            NotPrevLine notPrevOther = new NotPrevLine();
                            notPrevOther.objectTypeId = npa.objectTypeId;
                            notPrevOther.objectDesc = npa.objectDesc;
                            notPrevOther.codeobject = npa.codeobject;
                            notPrevOther.MacroOrg2 = npa.MacroOrg2;
                            notPrevOther.Vcs = npa.Vcs;

                            notPrevOther.TypologyBl = asg.TypologyObject;
                            notPrevOther.PrevalentBl = asg.MacroOrg1;
                            notPrevOther.BpcCode = asg.BpcCode;
                            //EY 02.10.2024 - EXCLUDE MacroOrg1 that contain GBL
                            if(!notPrevOther.PrevalentBl.ToUpper().Contains("GBL"))
                            {
                                OtherNotPrev.Add(notPrevOther);
                            }
                            // END EY 02.10.2024
                        }

                    }
                }
            }
            return OtherNotPrev;
        }
        private List<NotPrevLine> GetListNpaServStaff(HrmasterdataOu orgUnit)
        {
            List<NotPrevLine> npaList = new List<NotPrevLine>();

            if (!string.IsNullOrEmpty(orgUnit.ServCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod1);
                notPrev.objectDesc = orgUnit.ServDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod2);
                notPrev.objectDesc = orgUnit.ServDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod3);
                notPrev.objectDesc = orgUnit.ServDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod4);
                notPrev.objectDesc = orgUnit.ServDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.ServCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod5);
                notPrev.objectDesc = orgUnit.ServDesc5;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod1);
                notPrev.objectDesc = orgUnit.StaffDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod2);
                notPrev.objectDesc = orgUnit.StaffDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod3);
                notPrev.objectDesc = orgUnit.StaffDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.StaffCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 32;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod4);
                notPrev.objectDesc = orgUnit.StaffDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.StaffCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.StaffCod5);
                notPrev.objectDesc = orgUnit.StaffDesc5;
                npaList.Add(notPrev);
            }
            return npaList;
        }


        public List<NotPrevLine> GetListNpaSe(HrmasterdataOu orgUnit, IActivityMappingAdapter activityMappingAdapter, bool isLatam, string init_search_po_object_abbr)
        {
            List<NotPrevLine> npaList = new List<NotPrevLine>();

            if (!string.IsNullOrEmpty(orgUnit.ServCod1))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod1);
                notPrev.objectDesc = orgUnit.ServDesc1;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod2))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod2);
                notPrev.objectDesc = orgUnit.ServDesc2;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod3))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod3);
                notPrev.objectDesc = orgUnit.ServDesc3;
                npaList.Add(notPrev);
            }

            if (!string.IsNullOrEmpty(orgUnit.ServCod4))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod4);
                notPrev.objectDesc = orgUnit.ServDesc4;
                npaList.Add(notPrev);
            }
            if (!string.IsNullOrEmpty(orgUnit.ServCod5))
            {
                NotPrevLine notPrev = new NotPrevLine();
                notPrev.objectTypeId = 31;
                notPrev.codeobject = Convert.ToInt32(orgUnit.ServCod5);
                notPrev.objectDesc = orgUnit.ServDesc5;
                npaList.Add(notPrev);
            }

            List<NotPrevLine> npaListAll = new List<NotPrevLine>();

            foreach (NotPrevLine npa in npaList)
            {
                Task<List<ActivityAssociationGroup>> activityAssociationTask = activityMappingAdapter.GetActivityAssociationGroup(CancellationToken.None, npa.codeobject.ToString(), npa.objectTypeId.ToString());
                activityAssociationTask.Wait();

                if (activityAssociationTask.IsCompleted)
                {
                    List<ActivityAssociationGroup> assGroup = activityAssociationTask.Result;

                    if (assGroup != null && assGroup.Count > 0)
                    {
                        this.ConstantValue = new ConstantValue();
                        foreach (ActivityAssociationGroup aa in assGroup)
                        {
                            // Controllo se la lista è vuota oppure non contiene la macroOrg1 
                            //allora aggiungo la NPA prendendo le prime informazioni dalla NPA e le seguenti dall' assGroup

                            NotPrevLine nn = new NotPrevLine();
                            //nn.BpcCode = npa.BpcCode;
                            nn.codeobject = npa.codeobject;
                            nn.MacroOrg2 = npa.MacroOrg2;
                            nn.objectDesc = npa.objectDesc;
                            nn.objectTypeId = npa.objectTypeId;
                            nn.Vcs = npa.Vcs;

                            nn.TypologyBl = aa.TypologyObject;

                            if ((nn.TypologyBl == ConstantValue.TypologyGCO) || (nn.objectTypeId.ToString() == ConstantValue.servicecode && aa.PoObjectAbbr.StartsWith(init_search_po_object_abbr)))
                            {
                                nn.PrevalentBl = isLatam ? ConstantValue.bREL : ConstantValue.bRET;
                            }
                            else
                            {
                                nn.PrevalentBl = aa.MacroOrg1;
                            }
                            nn.BpcCode = aa.BpcCode;

                            nn.PoObjectAbbr = aa.PoObjectAbbr;

                            npaListAll.Add(nn);
                        }
                    }
                }
            }

            return npaListAll;
        }
        
    }
}