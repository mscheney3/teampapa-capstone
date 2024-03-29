﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models.Scenario
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public string ResponseText { get; set; }
        public string ResponseImage { get; set; }
        public int ScenarioId { get; set; }
        public string Color { get; set; }
        public string Emoji { get; set; }
    }
}