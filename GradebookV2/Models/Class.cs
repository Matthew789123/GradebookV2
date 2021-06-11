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
        [Range(1, 8)]
        public int Grade { get; set; }
        [Required]
        [StringLength(1)]
        public string Name { get; set; }
        public string TeacherId { get; set; }

        public virtual ApplicationUser HomeroomTeacher { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<SubjectClassTeacher> SubjectClassTeachers { get; set; }

    }
}