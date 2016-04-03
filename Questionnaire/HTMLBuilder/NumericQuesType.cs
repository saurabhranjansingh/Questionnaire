﻿using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class NumericQuesType : QuesType
    {
        public NumericQuesType(Question q) : base(q.QuestionType1.QuesType, q)
        {
        }

        public override string GetHtml(int dispHierarchy)
        {
            return string.Format(htmlSkeleton, dispHierarchy, question.QuesText, question.ID, question.ID);
        }
    }
}