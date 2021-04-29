using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Class
    {
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TeacherId { get; set; }

        [Required]
        public virtual ApplicationUser HomeroomTeacher { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<SubjectClassTeacher> SubjectClassTeachers { get; set; }

    }
}