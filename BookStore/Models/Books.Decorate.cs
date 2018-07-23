using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    [MetadataType(typeof(BooksDecorate))]
    public partial class Books
    {

    }

    public class BooksDecorate
    {
        [DisplayName("Category"),Required]
        public int CategoryId { get; set; }
        [DisplayName("Publishing Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PublishingDate { get; set; }
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }
    }
}