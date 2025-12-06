namespace masterdata.Helpers
{
    public class QueryBuilder
    {
        public string Company { get; set; }
        public string MacroOrg1 { get; set; }
        public string MacroOrg2 { get; set; }
        public string Vcs { get; set; }
        public string VcsDescription { get; set; }
        public string Perimeter { get; set; }
        public string Process { get; set; }
        public string ProcessDescription { get; set; }
        public string Organization { get; set; }
        public string OrganizationDescription { get; set; }
        public string Result { get; set; }

        public QueryBuilder() { }

        public QueryBuilder(string ResultKO)
        {
            this.Company = null;
            this.MacroOrg1 = null;
            this.MacroOrg2 = null;
            this.Vcs = null;
            this.Result = ResultKO;
        }
    }
}