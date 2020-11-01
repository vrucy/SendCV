using System;
using System.Collections.Generic;
using System.Text;

namespace SendCV.Models
{
    public class CompanyAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public CompanyCredentials CompanyCredentials { get; set; }
    }
}
