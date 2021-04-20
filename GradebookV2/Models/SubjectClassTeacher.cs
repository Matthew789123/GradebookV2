using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class SubjectClassTeacher
    {
        [Required]
        public int SubjectClassTeacherId { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual User Teacher { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}