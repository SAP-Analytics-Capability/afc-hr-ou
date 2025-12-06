using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class Association
    {
        [JsonProperty(PropertyName = "association_id")]
        public int AssociationId { get; set; }

        [JsonProperty(PropertyName = "typology_association")]
        public string TypologyAssociation { get; set; }

        [JsonProperty(PropertyName = "organizational_unit")]
        public HrmasterdataOu Unit { get; set; }

        [JsonProperty(PropertyName = "cost_centers")]
        public List<BwMasterObject> Centers { get; set; }

        public Association() { }

        public Association(string dummy)
        {
            HrmasterdataOu orgUnit = new HrmasterdataOu("Fake Association");
            Centers = CreateFittizioCC(orgUnit);
        }

        private List<BwMasterObject> CreateFittizioCC(HrmasterdataOu orgUnit)
        {
            List<BwMasterObject> ccList = new List<BwMasterObject>();
            BwMasterObject cc = new BwMasterObject("fittizio", orgUnit);

            return ccList;
        }
    }
}