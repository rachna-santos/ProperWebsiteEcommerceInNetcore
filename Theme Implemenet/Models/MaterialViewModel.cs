namespace Theme_Implemenet.Models
{
    public class MaterialViewModel
    {
        public IQueryable<ProductMaterial> ProductMaterial { get; set; }
        public int pagesize { get; set; }
        public int currentpage { get; set; }
        public int totalpage { get; set; }
        public string term { get; set; }

        public string OrderBy { get; set; }

    }
}
