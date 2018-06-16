using System;
using System.ComponentModel.DataAnnotations;

namespace RssDb.Entities
{
    public class FeedItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public string Body { get; set; }

        [Required]
        public string Url { get; set; }

        public FeedProvider FeedProvider { get; set; }
    }
}