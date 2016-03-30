using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class HtmlBuilder
    {
        public static string Build(List<Question> questions, List<DropDownValues> dropDownChoices)
        {
            string FinalHtmlString = string.Empty;
            List<DropDownValues> ddValues;

            foreach (var ques in questions)
            {
                ddValues = null;

                if (ques.QuestionType1.QuesType.Equals("Dropdown"))
                    ddValues = dropDownChoices;

                var quesType = QuesTypeFactory.GetQuesTypeObj(ques, ddValues);
                string quesHtml = quesType.GetHtml();

                FinalHtmlString = String.Format("{0}{1}", FinalHtmlString, quesHtml);
            }

            return FinalHtmlString;
        }
    }
}