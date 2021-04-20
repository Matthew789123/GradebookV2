using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Grade
    {
        [Required]
        public int GradeId { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int StudentId { get; set; }
        public virtual ApplicationUser Student{ get; set; }
        public virtual Subject Subject { get; set; }

    }
}