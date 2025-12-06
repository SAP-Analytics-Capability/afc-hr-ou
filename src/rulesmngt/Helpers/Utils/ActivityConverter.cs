using System;
using System.Text;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Helpers
{
    public class ActivityConverter
    {
        public static List<ActivityMapping> ConvertFromAssociations(List<ActivityAssociation> alist, out string errormessage)
        {
            errormessage = string.Empty;
            List<ActivityMapping> amlist = new List<ActivityMapping>();

            if (alist == null || alist.Count == 0)
            {
                return amlist;
            }

            try
            {
                foreach (ActivityAssociation associationitem in alist)
                {
                    Vcs vcs = new Vcs();
                    ProcessLocal processlocal = new ProcessLocal();
                    ProcessGlobal processglobal = new ProcessGlobal();
                    Organization organization = new Organization();
                    ActivityList macroorg2 = new ActivityList();
                    MacroOrg1 macroorg1 = new MacroOrg1();
                    Perimeter perimeter = new Perimeter();
                    Bpc bpc = new Bpc();
                    ActivityMapping activitymapping = new ActivityMapping();

                    vcs.VcsId = 0;
                    vcs.VcsCode = associationitem.OrVcs;
                    vcs.Desc = associationitem.OrVcsDesc;

                    processlocal.ProcessCode = associationitem.OrProcess;
                    processlocal.Desc = associationitem.OrProcessDesc;

                    processglobal.processCode = associationitem.OrProcess;
                    processglobal.Desc = associationitem.OrProcessDesc;

                    organization.OrganizationCode = associationitem.OrOrganization;
                    organization.Desc = associationitem.OrOrganizationDesc;

                    macroorg2.ActivityId = 0;
                    macroorg2.ActivityName = associationitem.AfcMacroorg2;
                    macroorg2.Desc = associationitem.AfcMacroorg2Desc;

                    macroorg1.MacroOrg1Id = 0;
                    macroorg1.MacroOrg1Code = associationitem.AfcMacroorg1;
                    macroorg1.Desc = associationitem.AfcMacroorg1Desc;

                    bpc.BpcId = Convert.ToInt32(associationitem.PoObjectId);
                    bpc.bpcCode = associationitem.PoBpcCode;
                    bpc.Desc = associationitem.PoDescription;
                    

                    

                    activitymapping.ActivityMappingId = associationitem.RecordId;
                    activitymapping.ObjectType = associationitem.ObjectTypeId;
                    activitymapping.Attribute = associationitem.PoAttribute;
                    activitymapping.NotPrevalent = associationitem.PoNpa;

                    activitymapping.BpcCode = bpc;
                    activitymapping.PeOPerimeter = associationitem.PoPerimeter;
                    activitymapping.MacroOrg1 = macroorg1;
                    activitymapping.MacroOrg2 = macroorg2;
                    activitymapping.ProcessGlobal = processglobal;
                    activitymapping.Organization = organization;
                    
                    //activitymapping.ProcessLocal = processlocal.ProcessCode;
                    activitymapping.Vcs = vcs;
                    activitymapping.TypologyObject = associationitem.TypologyObject;
                    

                    amlist.Add(activitymapping);
                }
            }
            catch (Exception ex)
            {
                amlist = null;
                errormessage = ex.ToString();
            }
            return amlist;
        }
    }
}