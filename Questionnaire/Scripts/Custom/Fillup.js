function submitForm() {
    //console.log("validating..");
    GatherUserResponse();

    if ($("#FillupForm").valid()) {
        alert("GREAT! form is valid");
    } 
}

$(function () {
    $(".date").datetimepicker({ format: "MM/DD/YYYY" });
    $("#DateOfCall").datetimepicker({ format: "MM/DD/YYYY" });

    $(".num").each(function () {
        $(this).rules("add", {
            number: true
        });
    });

    $("#btnSubmitForm").click(function () {
        submitForm();
    });

});

$("#FillupForm").validate({
    errorPlacement: function (error, element) {
        if (element.attr("name") === "bif1")
            error.insertAfter($("#DateOfCall"));
        else
            error.insertAfter(element);
    },
    focusInvalid: false,
    invalidHandler: function (form, validator) {

        if (!validator.numberOfInvalids())
            return;

        $("html, body").animate({
            scrollTop: $(validator.errorList[0].element).offset().top - 70
        }, 1000);

    }
});




function GatherUserResponse() {

    var basicResponseArr = [];
    var dynResponseArr = [];
    var i = 0;
    var len = 0;

    //Get all the Basic Input responses
    var basicInputQuestions = document.getElementsByClassName("BasicInputRow");

    if (basicInputQuestions.length > 0) {
        //Iterate through every basic input question
        for (i = 0, len = basicInputQuestions.length; i < len; i++) {

            //Find the user input
            var userInputBasicQues = basicInputQuestions[i].querySelector(".UserInputBasic");

            //Add the response to response array
            basicResponseArr.push({
                quesID: userInputBasicQues.id,
                response: userInputBasicQues.value.trim()
            });
        }
    }


    //Find all the Boolean ques
    var boolQuestions = document.getElementsByClassName("BooleanQuestion");

    if (boolQuestions.length > 0) {
        //Iterate through every boolean question
        for (i = 0, len = boolQuestions.length; i < len; i++) {

            //Find the active active radio button
            var activeRadioBtn = boolQuestions[i].querySelector(".active");

            //Add the response to response array
            dynResponseArr.push({
                quesID: activeRadioBtn.id.split("-")[1],
                response: activeRadioBtn.innerText
            });
        }
    }

    //Find all the DropDown Questions
    var dropdownQuestions = document.getElementsByClassName("DropDownQuestion");

    if (dropdownQuestions.length > 0) {
        //Iterate through every dropdown ques
        for (i = 0, len = dropdownQuestions.length; i < len; i++) {

            //Find the selected dropdown choice
            var ddCtrl = dropdownQuestions[i].querySelector(".DropDownControl");
            var selectedChoice = ddCtrl.options[ddCtrl.selectedIndex];

            //Add the response to response array
            dynResponseArr.push({
                quesID: selectedChoice.id.split("-")[1],
                response: selectedChoice.text
            });
        }
    }

    //Find all the FreeText Questions
    var freeTextQuestions = document.getElementsByClassName("FreeTextQuestion");

    if (freeTextQuestions.length > 0) {

        //Iterate through every free text ques
        for (i = 0, len = freeTextQuestions.length; i < len; i++) {

            //Find the free text input
            var textInput = freeTextQuestions[i].querySelector(".userTextInput");

            //Add the response to response array
            dynResponseArr.push({
                quesID: textInput.id.split("-")[1],
                response: textInput.value.trim()
            });
        }
    }

    //Get the Questionairre ID
    var qnnrId = { qnnrID: document.getElementById("qnnrID").innerText }


    var response = [qnnrId, basicResponseArr, dynResponseArr];
    //alert(JSON.stringify(response));
}