using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gradebook.Models
{
    public class Lesson
    {
        [Required]
        public int LessonId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public virtual IList<File> Files { get; set; }
        public virtual Subject Subject { get; set; }
    }
}