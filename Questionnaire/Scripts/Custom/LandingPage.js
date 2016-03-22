$(document).ready(function () {
    $("#Go").click(function () {
        var selectedId = $("#QnnrListDropDown").val();
        window.location.href = "Home/Fillup/" + selectedId;
    });
});