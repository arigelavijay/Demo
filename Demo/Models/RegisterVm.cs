using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class RegisterVm
    {
        public register register { get; set; }
        public List<register> data { get; set; }
        public int selectedIndex { get; set; }
    }
}