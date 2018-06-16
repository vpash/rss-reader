using System.ComponentModel.DataAnnotations;

namespace RssDb.Entities
{
    public class FeedProvider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }
    }
}