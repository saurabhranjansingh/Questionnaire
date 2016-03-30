using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class BooleanQuesType : QuesType
    {
        public BooleanQuesType(Question q) : base(q.QuestionType1.QuesType, q)
        {
        }

        public override string GetHtml()
        {
            return string.Format(htmlSkeleton, question.Hierarchy, question.QuesText, question.ID, question.ID, question.ID);
        }
    }
}