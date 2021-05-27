using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class MyGradesViewModel
    {
        public List<Subject> Subjects { get; set; }
        public List<Grade> Grades { get; set; }
      
    }
}