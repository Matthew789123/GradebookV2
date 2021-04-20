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
        public string AnswerA { get; set; }
        [Required]
        public string AnswerB { get; set; }
        [Required]
        public string AnswerC { get; set; }
        [Required]
        public string AnswerD { get; set; }
        [Required]
        public string CorrectAnswet { get; set; }
        [Required]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}