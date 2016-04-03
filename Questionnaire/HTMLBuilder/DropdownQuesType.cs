using System.Collections.Generic;
using System.Linq;
using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class DropdownQuesType : QuesType
    {
        List<DropDownValues> _ddv;
        string _dropDownItemHtml;

        public DropdownQuesType(Question q, List<DropDownValues> ddv) : base(q.QuestionType1.QuesType, q)
        {
            _ddv = ddv;
            _dropDownItemHtml = HtmlSkeleton.DropDownItemHtml;
        }

        public override string GetHtml(int dispHierarchy)
        {
            var ddChoices = (from x in _ddv
                             where x.QuestionID == question.ID
                             select x.Value).ToList();

            var dropdownOptionsHtml = string.Empty;

            for (var i = 0; i < ddChoices.Count(); i++)
            {
                var tmpStr = string.Format(_dropDownItemHtml, i.ToString(), question.ID, i.ToString(), ddChoices[i]);
                dropdownOptionsHtml = string.Format("{0}{1}", dropdownOptionsHtml, tmpStr);
            }
            
            return string.Format(htmlSkeleton, dispHierarchy, question.QuesText, dropdownOptionsHtml);
        }
    }
}