namespace Theme_Implemenet.Models
{
    public class CommentModelView
    {
        public IQueryable<Rewies> rewies { get; set; }
        public int pagesize { get; set; }
        public int currentpage { get; set; }
        public int totalpage { get; set; }
        public string term { get; set; }

        public string OrderBy { get; set; }
    }
}

