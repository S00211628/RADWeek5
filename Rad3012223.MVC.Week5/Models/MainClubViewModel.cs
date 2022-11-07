﻿using Rad3012223.ClubData.ClassLibrary.Week5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rad3012223.MVC.Week5.Models
{
    public class MainClubViewModel
    {
        public int ClubID { get; set; }
        public string cluName { get; set; }
        public List<Member> Unapproved { get; set; }
        public List<Member> Approved { get; set; }
    }
}