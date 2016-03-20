using System.ComponentModel.DataAnnotations;

namespace Questionnaire.ViewModels
{
    public class CreateQuestionViewModel
    {
        [Required(ErrorMessage = "Please provide the Question name.")]
        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string QuesText { get; set; }

        public int QuestionnaireID { get; set; }

        public int QuestionType { get; set; }

        public int Hierarchy { get; set; }
    }
}