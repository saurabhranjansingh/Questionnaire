using System;
using System.Collections.Generic;
using System.Linq;
using Questionnaire.Models;

namespace Questionnaire.HTMLBuilder
{
    public class QuesTypeFactory
    {
        public static QuesType GetQuesTypeObj(Question ques, List<DropDownValues> ddv)
        {
            switch (ques.QuestionType1.QuesType)
            {
                case "Boolean":
                    return new BooleanQuesType(ques);
                case "Dropdown":
                    return new DropdownQuesType(ques, ddv);
                case "Free Text":
                    return new FreeTextQuesType(ques);
                case "Numeric":
                    return new NumericQuesType(ques);
                case "Date":
                    return new DateQuesType(ques);
                default:
                    return new InstructionType(ques);
            }
        }
    }
}