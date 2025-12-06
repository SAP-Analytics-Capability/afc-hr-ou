using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationV2
    {
        [JsonProperty(PropertyName = "association_id")]
        public int AssociationId { get; set; }

        [JsonProperty(PropertyName = "typology_association")]
        public string TypologyAssociation { get; set; }

        [JsonProperty(PropertyName = "organizational_unit")]
        public HrOu Unit { get; set; }

        [JsonProperty(PropertyName = "cost_centers")]
        public List<BwCC> Centers { get; set; }

        

        public AssociationV2() { }

        // public Association(string dummy)
        // {
        //     HrmasterdataOu orgUnit = new HrmasterdataOu("Fake Association");
        //     Centers = CreateFittizioCC(orgUnit);
        // }

        // private List<BwMasterObject> CreateFittizioCCV2(HrmasterdataOu orgUnit)
        // {
        //     List<BwMasterObject> ccList = new List<BwMasterObject>();
        //     BwMasterObject cc = new BwMasterObject("fittizio", orgUnit);

        //     return ccList;
        // }
    }
}