using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class InstructionType : QuesType
    {
        public InstructionType(Question q) : base(q.QuestionType1.QuesType, q)
        {
        }

        public override string GetHtml()
        {
            return string.Format(htmlSkeleton, question.QuesText);
        }
    }
}