using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Test
    {
        [Required]
        public int TestId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? Start { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Duration must be atleast 1 minute.")]
        public int Duration { get; set; }
        [Required]
        public int SubjectId{ get; set; }
        [Required]
        public int ClassId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Class Class { get; set;}
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}