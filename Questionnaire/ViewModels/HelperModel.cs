using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.ViewModels
{
    public class UserResponse
    {
        public String qnnrID { get; set; }

        public List<StaticInputFields> staticInputs { get; set; }

        public List<DynamicInputFields> dynamicInputs { get; set; }
    }



    public class StaticInputFields
    {
        public string quesID { get; set; }
        public string response { get; set; }
    }

    public class DynamicInputFields
    {
        public string quesID { get; set; }
        public string response { get; set; }
    }

    public class Reportfilters
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
    }

    public class UpdatedHierarchyOrder
    {
        public int qnnrID { get; set; }
        public List<string> newOrder { get; set; }
    }
}