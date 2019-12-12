using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models.Scenario
{
    public class Scenario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public List<Answer> AnswerList { get; set; }
        public bool IsActive { get; set; }
    }
}
