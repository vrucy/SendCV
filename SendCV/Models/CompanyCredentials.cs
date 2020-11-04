using SendCV.Context;
using SendCV.ViewModels;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Unity;

namespace SendCV.Models
{
    public class CompanyCredentials: IDataErrorInfo
    {
        private List<string> errors = new List<string>();
        private SendCVContext _context = new SendCVContext();
        private IUnityContainer _container = new UnityContainer();
        public CompanyCredentials()
        {
            _context = _container.Resolve<SendCVContext>();
        }
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateEmailSend { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string NameHR { get; set; }
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var x = _context.CompanyCredentials.Where(c=>c.Name == this.Name).OrderByDescending(y=>y.DateEmailSend).FirstOrDefault();
                var countCompany = _context.CompanyCredentials.Where(c => c.Name == this.Name).Count();

                if (x != null)
                {
                    if (this.Name.Equals(x.Name))
                        return "You send email this company: " + countCompany + ".\n Last time:" + x.DateEmailSend;
                }

                if (!columnName.Equals("Name"))
                    return string.Empty;

                

                return string.Empty;
            }
        }
    }
}
