﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class Setting
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}