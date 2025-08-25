using System.ComponentModel.DataAnnotations;

namespace WinForms_FileAnalyzer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; } 
    }
}
