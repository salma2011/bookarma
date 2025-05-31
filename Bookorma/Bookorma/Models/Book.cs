using System.ComponentModel.DataAnnotations;

namespace Bookorma.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Bookname { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
