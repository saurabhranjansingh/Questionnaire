using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class FreeTextQuesType : QuesType
    {
        public FreeTextQuesType(Question q) : base(q.QuestionType1.QuesType, q)
        {
        }

        public override string GetHtml()
        {
            return string.Format(htmlSkeleton, question.Hierarchy, question.QuesText, question.ID);
        }
    }
}