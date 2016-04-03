using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public abstract class QuesType
    {
        public Question question;
        public string htmlSkeleton = string.Empty;

        protected QuesType(string qType, Question q)
        {
            this.question = q;

            switch (qType)
            {
                case "Boolean":
                    this.htmlSkeleton = HtmlSkeleton.BooleanHtml;
                    break;
                case "Dropdown":
                    this.htmlSkeleton = HtmlSkeleton.DropDownHtml;
                    break;
                case "Free Text":
                    this.htmlSkeleton = HtmlSkeleton.FreeTextHtml;
                    break;
                case "Numeric":
                    this.htmlSkeleton = HtmlSkeleton.NumericHtml;
                    break;
                case "Date":
                    this.htmlSkeleton = HtmlSkeleton.DatePickerHtml;
                    break;
                case "Instruction":
                    this.htmlSkeleton = HtmlSkeleton.InstructionHtml;
                    break;
            }
        }
        

        public abstract string GetHtml(int displayHierarchy);
    }
}