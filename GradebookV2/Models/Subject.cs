using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Subject
    {
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<SubjectClassTeacher> SubjectClassTeachers { get; set; }
    }
}