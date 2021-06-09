using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class Question
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [Display(Name = "A")]
        public string AnswerA { get; set; }
        [Required]
        [Display(Name = "B")]
        public string AnswerB { get; set; }
        [Required]
        [Display(Name = "C")]
        public string AnswerC { get; set; }
        [Required]
        [Display(Name = "D")]
        public string AnswerD { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        [Required]
        public int Points { get; set; } 
        [Required]
        public int Number { get; set; }
        [Required]
        public int TestId { get; set; }
        [JsonIgnore]
        public virtual Test Test { get; set; }
    }
}