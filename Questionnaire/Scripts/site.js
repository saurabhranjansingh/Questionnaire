$(document).ready(function () {

    /* This is change event for dropdownlist */
    $("#SetNewQuesInputType").change(function () {

        /* Get the selected value of dropdownlist */
        var selectedId = $(this).val();

        //If the input type is DropDown
        if (selectedId == 2) {

            /* Request the partial view with .get request. */
            $.get("/DropDownValue/_ViewDropDownItems/", function (data) {

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


function AddNewItemToDropDownList() {
    var newItemToAdd = $("#txtNewDropDownItem").val().trim();

    if (newItemToAdd == "") {
        $("#ErrorMessage").html("Name can not be empty.");
        $("#ErrorPanel").show();
    } else {
        $("#ErrorPanel").hide();

        var allExistingValues = $(".ddExistingValue").map(function () {
            return this.innerHTML.toString().trim().toUpperCase();
        }).get();

        if ($.inArray(newItemToAdd.toUpperCase(), allExistingValues) !== -1) {
            $("#ErrorMessage").html("Item already exists.");
            $("#ErrorPanel").show();
            return;
        } else {
            $("#ErrorPanel").hide();
        }

        /* Request the partial view with .get request. */
        $.get("/DropDownValue/AddNewItem/?item=" + newItemToAdd, function (data) {

            /* data is the pure html returned from action method, load it to your page */
            $("#DropDownValuesPlaceHolder").html(data);
            $("#DropDownValuesPlaceHolder").fadeIn("fast");
        });
    }

}
