/* This is change event for dropdownlist */
$("#SetNewQuesInputType").change(function () {

    /* Get the selected value of dropdownlist */
    var selectedId = $(this).val();

    //If the input type is DropDown
    if (selectedId === 2) {
        /* Request the partial view with .get request. */
        $.get("/DropDownValue/AddItemToList/" + selectedId, function (data) {

            /* data is the pure html returned from action method, load it to your page */
            $("#DropDownValuesPlaceHolder").html(data);
            $("#DropDownValuesPlaceHolder").fadeIn("fast");
        });
    }
});