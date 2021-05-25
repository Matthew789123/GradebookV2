using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookV2.Models
{
    public class AssignStudentsViewModel
    {
        public List<ApplicationUser> Students { get; set; }
        public List<Class> Classes { get; set; }
    }
}