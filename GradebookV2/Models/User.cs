using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gradebook.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string Mail { get; set; }
        public int? Phone { get; set; }
        [Required]
        public int type { get; set; }
        public int? ParentId { get; set; }
        public int? ClassId { get; set; }

        public ICollection<News> News { get; set; }
        public ICollection<Message> Messages { get; set; }
        public Class Class { get; set; }
        public User Parent { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}