$(document).ready(function () {

    var quesType = $('#QuesType').text();
    if (quesType == "Dropdown") {
        $("#btnSave").prop("disabled", false);
        $("#AddAtleast1DDItem").hide();

        /* Request the partial view with .get request. */
        $.get("/DropDownValue/_ViewExistingDropDownItems/", function (data) {

            /* data is the pure html returned from action method, load it directly */
            $("#DropDownValuesPlaceHolder").html(data);
            $("#DropDownValuesPlaceHolder").fadeIn("fast");
        });

        //Check if dropdown items are already present (because they are stored in session)
        CheckIfAnyDDItemExists();
    }

});

function CheckIfAnyDDItemExists() {

    var allExistingValues = $(".ddExistingValue").map(function() {
        return this.innerHTML;
    }).get();


    //If there are no items present in the List of dropdown items then Disable the 'Create Question' button.
    if (allExistingValues.length == 0) {
        $("#btnCreateQuestion").prop("disabled", true);
        $("#AddAtleast1DDItem").show();
    } else {
        $("#btnCreateQuestion").prop("disabled", false);
        $("#AddAtleast1DDItem").hide();
    }
}

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
            CheckIfAnyDDItemExists();
        });

        //$('#btnCreateQuestion').prop('disabled', false);
        //CheckIfAnyDDItemExists();
    }

}