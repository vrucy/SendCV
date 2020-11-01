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
        //private bool isSelected;

        //public bool IsSelected
        //{
        //    get { return isSelected; }
        //    set { isSelected = value; OnPropertyChanged("IsSelected"); }
        //}

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateEmailSend { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string NameHR{ get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed == null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //TODO: Make 1:1 relationship
        //public int CompanyAddressId { get; set; }
        //public CompanyAddress CompanyAddress { get; set; }
    }
}
