using Microsoft.EntityFrameworkCore.Diagnostics;
using SendCV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        void SaveCompanies(IEnumerable<CompanyCredentials> companies);
        CompanyCredentials GetCompanyByLastDate(string name);
        int GetCompnayCount(string name);

    }
}
