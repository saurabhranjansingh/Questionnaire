using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Questionnaire.Models;

namespace Questionnaire.ViewModels
{
    public class EditQuestionViewModel
    {
        [Required(ErrorMessage = "This field can not be left blank.")]
        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string QuesText { get; set; }

        public int QuestionnaireID { get; set; }

        public string Questionnaire { get; set; }

        public string QuestionType { get; set; }

        public int Hierarchy { get; set; }

        public List<DrpDwnItem> DropDownItems { get; set; }
    }
}