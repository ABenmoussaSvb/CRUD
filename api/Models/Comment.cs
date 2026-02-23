namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public String Title { get; set; } = string.Empty;
        public String Contenu { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}