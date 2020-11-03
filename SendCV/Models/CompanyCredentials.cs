using SendCV.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SendCV.Models
{
    public class CompanyCredentials
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateEmailSend { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string NameHR{ get; set; }

}
