namespace Theme_Implemenet.Models
{
    public class RegisterViewModel
    {
        public IQueryable<ApplicationUser> ApplicationUser { get; set; }
        public int pagesize { get; set; }
        public int currentpage { get; set; }
        public int totalpage { get; set; }
        public string term { get; set; }
        public string OrderBy { get; set;}
    }
}



