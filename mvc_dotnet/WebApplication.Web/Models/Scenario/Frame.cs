using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models.Scenario
{
    public class Frame
    {
        public int Id { get; set; }
        public string FrameName { get; set; }
        public string ImageName { get; set; }
        public string Question { get; set; }
        public Dictionary<string, string> AnswerList { get; set; }
    }
}
