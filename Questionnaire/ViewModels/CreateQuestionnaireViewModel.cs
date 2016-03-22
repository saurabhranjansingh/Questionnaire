using System.ComponentModel.DataAnnotations;

namespace Questionnaire.ViewModels
{
    public class CreateQuestionnaireViewModel
    {
        [Required(ErrorMessage = "Please provide the Questionnaire name.")]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        public string Name { get; set; }
    }
}