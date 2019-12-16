using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models.Scenario
{
    public class Review
    {
        public int StudentId { get; set; }
        public string QuestionName { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
        
    }
}
