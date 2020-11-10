using Syncfusion.DocIO.DLS;
using System;

namespace SendCV.Extensions
{
    public static class WordDocExtensions
    {
        public static void ReplaceHrData(this WordDocument doc, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                doc.Replace("{address}, {city}", String.Empty, true, false);
                doc.Replace("{hrManager}","{address}, {city} ", true, false);
            }
            else
            {
                doc.Replace("{hrManager}", value, true, false);
            }
        }
        public static void ReplaceHrDataInEmail(this WordDocument doc, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                doc.Replace("{hrManager}", String.Empty, true, false);
            }
            else
            {
                doc.Replace("{hrManager}", value.Split(' ')[0], true, false);
            }
        }
        public static void ReplaceAddress(this WordDocument doc, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                doc.Replace("{address}, ", String.Empty, true, false);
            }
            else
            {
                doc.Replace("{address}", value, true, false);
            }
        }
        public static void ReplaceDataInDocument(this WordDocument doc, string key, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                doc.Replace(key, String.Empty, true, false);
            }
            else
            {
                doc.Replace(key, value, true, false);
            }
        }
    }
}
