using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(0, 5)]
        public int? Rating { get; set; }

        public string? Text { get; set; }

        public string? IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }

        [ForeignKey("Book")]
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}
