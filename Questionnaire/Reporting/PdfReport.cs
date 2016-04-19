using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI;

namespace Questionnaire.Reporting
{
    public class PdfReport : Report
    {
        public void GenerateReport(string reportID)
        {
            //fetch data
            var result = FetchReportData(reportID);
            //generate logic (npoi)
        }
    }
}