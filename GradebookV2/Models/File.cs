using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class File
    {
        [Required]
        public int FileId { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}