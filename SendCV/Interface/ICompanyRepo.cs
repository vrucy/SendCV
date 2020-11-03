using Microsoft.EntityFrameworkCore.Diagnostics;
using SendCV.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendCV.Interface
{
    public interface ICompanyRepo
    {
        void SaveCompanies(IEnumerable<CompanyCredentials> companies);
    }
}
