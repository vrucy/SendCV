using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SendCV.Extensions
{
    public static class WordDocExtensions
    {
        public static void HrManager(this WordDocument doc, string nameHr)
        {
            if (String.IsNullOrEmpty(nameHr))
            {
                doc.Replace("{hrManager}", String.Empty, true, false);
            }
            else
            {
                doc.Replace("{hrManager}", nameHr, true, false);
            }
        }
    }
}
