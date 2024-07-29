namespace Theme_Implemenet.Models
{
    public class CompanyViewModel
    {
        public IQueryable<Company> Companies { get; set; }
        public int pagesize { get; set; }
        public int currentpage { get; set; }
        public int totalpage { get; set; }
        public string term { get; set; }

        public string OrderBy { get; set; }

    }
}
