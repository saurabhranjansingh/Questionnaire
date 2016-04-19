using Questionnaire.Models;
using Questionnaire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Questionnaire.Reporting
{
    public abstract class Report
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();
        public List<ViewReport> FetchReportData(string ReportId)
        {           
            StringBuilder query = new StringBuilder();
            query.Append("select Report.ReportID,Report.Heirarchy,Report.Text,Report.Response,rm.Field1, rm.Field2, rm.Field3, rm.Field4, rm.Field5, rm.contact, rm.email, rm.CreatedAt,");
            query.AppendFormat(" (select qt.QuesType from Question q inner join QuestionType qt on q.QuestionType = qt.ID where q.QuesText = Report.Text and q.QuestionnaireID = (select QuestionnaireID from ReportMaster where ReportID = '{0}')) as QuestionType, ", ReportId);
            query.Append(" (select qm.Name from QuestionnaireMaster qm where qm.ID = rm.QuestionnaireID) as QuestionnaireName from Report inner join ReportMaster rm on Report.ReportID = rm.ReportID ");
            query.AppendFormat(" where report.ReportID='{0}' order by Heirarchy", ReportId);

            return db.Database.SqlQuery<ViewReport>(query.ToString()).ToList();                        
        }
    }
}