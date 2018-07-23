using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DAL
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorList { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishingDate_ { get; set; }
        public string PublishingDate { get { return PublishingDate_.ToString("yyyy-MM-dd"); } }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public int PageCount { get; set; }
        public double Price { get; set; }
    }
}