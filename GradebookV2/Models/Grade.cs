using Newtonsoft.Json;
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
        public decimal Value { get; set; }
        [Required]
        [Display(Name = "Grade")]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser Student{ get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }

    }
}