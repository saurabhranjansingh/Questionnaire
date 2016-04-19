using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;
using Questionnaire.ViewModels;
using System.Globalization;
using System.Text;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Questionnaire.Reporting;

namespace Questionnaire.Controllers
{
    public class ReportMasterController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: ReportMasters
        public ActionResult Index()
        {
            //var reportMaster = db.ReportMaster.Include(r => r.QuestionnaireMaster);
            //ViewBag.ResultVisible = false;

            return View();
        }

        [HttpPost]
        public JsonResult Search(Reportfilters filters)
        {
            if (!String.IsNullOrEmpty(filters.startDate) && !String.IsNullOrEmpty(filters.endDate))
            {
                DateTime startDate = DateTime.ParseExact(filters.startDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);
                DateTime endDate = DateTime.ParseExact(filters.endDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);
            }
            StringBuilder sqlStatement = new StringBuilder();

            bool isField1Search = false, isField2Search = false, isField3Search = false, isField4Search = false, isField5Search = false;
            isField1Search = !(String.IsNullOrEmpty(filters.startDate) && string.IsNullOrEmpty(filters.endDate));
            isField2Search = !String.IsNullOrEmpty(filters.field2);
            isField3Search = !String.IsNullOrEmpty(filters.field3);
            isField4Search = !String.IsNullOrEmpty(filters.field4);
            isField5Search = !String.IsNullOrEmpty(filters.field5);


            sqlStatement.Append("select rm.ReportID,rm.QuestionnaireID,	CONVERT(date,rm.Field1,110) as Field1, rm.Field2,rm.Field3,rm.Field4,rm.Field5,CONVERT(date, CreatedAt, 110) as CreatedAt,rm.Contact,rm.Email from ReportMaster rm");

            if (isField1Search || isField2Search || isField3Search || isField4Search || isField5Search)
            {
                sqlStatement.Append(" Where ");
                if (isField1Search)
                    sqlStatement.Append(String.Format(" (Field1 between '{0}' and '{1}' )", filters.startDate, filters.endDate));

                if ((isField2Search || isField3Search || isField4Search || isField5Search) && isField1Search)
                {
                    sqlStatement.Append(" and ");
                    isField1Search = false;
                }

                if (isField2Search)
                    sqlStatement.Append(String.Format(" (rm.Field2 like '%{0}%') ", filters.field2));

                if ((isField3Search || isField4Search || isField5Search) && (isField1Search || isField2Search))
                {
                    sqlStatement.Append(" and ");
                    isField2Search = false;
                }

                if (isField3Search)
                    sqlStatement.Append(String.Format(" (rm.Field3 like '%{0}%') ", filters.field3));

                if ((isField4Search || isField5Search) && (isField1Search || isField2Search || isField3Search))
                {
                    sqlStatement.Append(" and ");
                    isField3Search = false;
                }

                if (isField4Search)
                    sqlStatement.Append(String.Format(" (rm.Field4 like '%{0}%') ", filters.field4));

                if ((isField5Search) && (isField1Search || isField2Search || isField3Search || isField4Search))
                {
                    sqlStatement.Append(" and ");
                    isField4Search = false;
                }

                if (isField5Search)
                    sqlStatement.Append(String.Format(" (rm.Field5 like '%{0}%')", filters.field5));

            }

            sqlStatement.Append(" order by Field1,Field2,Field3,Field4,Field5,CreatedAt ");

            var result = db.Database.SqlQuery<ReportMaster>(sqlStatement.ToString()).ToList();
            return Json(result);

        }

        [HttpGet]
        public ActionResult ViewReport()
        {
            var reportID = Request["requestId"];
            Reporting.Report r = new BrowserReport();
            var result = r.FetchReportData(reportID);
            return View(result);
        }

        public ActionResult ExportToExcel(string id)
        {
            try
            {
                XlsReport xls = new XlsReport();
                byte[] byteStream = xls.GenerateReport(id);

                string saveAsFileName = string.Format("ExcelReport-{0:d}.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(byteStream);
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        public ActionResult ExportToPDF(string id)
        {
            try
            {
                //PdfReport pdf = new PdfReport();
                //byte[] byteStream = pdf.GenerateReport(id);

                //string saveAsFileName = string.Format("ExcelReport-{0:d}.xls", DateTime.Now);
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                //Response.Clear();
                //Response.BinaryWrite(byteStream);
                //Response.End();
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
