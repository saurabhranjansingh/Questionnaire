$(document).ready(function () {

    /* This is change event for dropdownlist */
    $("#SetNewQuesInputType").change(function () {

        $("#btnCreateQuestion").prop("disabled", false);

        /* Get the selected value of dropdownlist */
        var selectedId = $(this).val();

        $("#AddAtleast1DDItem").hide();

        //If the input type is DropDown
        if (selectedId == 2) {

            /* Request the partial view with .get request. */
            $.get("/DropDownValue/_ViewDropDownItems/1", function (data) {

                /* data is the pure html returned from action method, load it directly */
                $("#DropDownValuesPlaceHolder").html(data);
                $("#DropDownValuesPlaceHolder").fadeIn("fast");
            });

            //Check if dropdown items are already present (because they are stored in session)
            CheckIfAnyDDItemExists();
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
        $.get("/DropDownValue/AddNewItem/?mode=create&item=" + newItemToAdd, function (data) {

            /* data is the pure html returned from action method, load it to your page */
            $("#DropDownValuesPlaceHolder").html(data);
            $("#DropDownValuesPlaceHolder").fadeIn("fast");
            CheckIfAnyDDItemExists();
        });

        //$('#btnCreateQuestion').prop('disabled', false);
        //CheckIfAnyDDItemExists();
    }

}

function CheckIfAnyDDItemExists() {

    var allExistingValues = $(".ddExistingValue").map(function () {
        return this.innerHTML;
    }).get();


    //If there are no items present in the List of dropdown items then Disable the 'Create Question' button.
    if (allExistingValues.length == 0) {
        $("#btnCreateQuestion").prop("disabled", true);
        $("#AddAtleast1DDItem").show();
    }
    else {
        $("#btnCreateQuestion").prop("disabled", false);
        $("#AddAtleast1DDItem").hide();
    }
}