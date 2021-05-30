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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Start { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int SubjectClassTeacherId { get; set; }
        public virtual SubjectClassTeacher SubjectClassTeacher { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}