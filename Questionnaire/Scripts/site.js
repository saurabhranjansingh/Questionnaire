$(document).ready(function () {

    /* This is change event for dropdownlist */
    $("#SetNewQuesInputType").change(function () {

        //Hide the error panel
        $()
        /* Get the selected value of dropdownlist */
        var selectedId = $(this).val();

        //If the input type is DropDown
        if (selectedId == 2) {
            console.log("Dropdown selected");

            /* Request the partial view with .get request. */
            $.get("/DropDownValue/_ViewDropDownItems/" + selectedId, function (data) {

                console.log("Got back the data");

                /* data is the pure html returned from action method, load it to your page */
                $("#DropDownValuesPlaceHolder").html(data);
                $("#DropDownValuesPlaceHolder").fadeIn("fast");
            });
        }
        else {
            $("#DropDownValuesPlaceHolder").fadeOut("fast");
        }
    });

});


function AddNewItemLoDropDownList() {
    var NewItemToAdd = $("#txtNewDropDownItem").val().trim();
    if (NewItemToAdd == "") {
        alert("Can not insert blank");
    }
    else
        alert(NewItemToAdd + " item will be added.");
}
