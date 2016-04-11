using System.ComponentModel.DataAnnotations;

namespace Questionnaire.ViewModels
{
    public class EditHierarchyViewModel
    {
        public int QuestionID { get; set; }
       
        public string QuesText { get; set; }

        public string QuestionType { get; set; }

        public int Hierarchy { get; set; }
    }
}