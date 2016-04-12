$(function () {
    $("#quesList").sortable({
        axis: 'y',
        update: function (event, ui) {
            var order = $(this).sortable('serialize');
        }
    }).disableSelection();

    $("#btnSave").on('click', function () {
        var r = $("#quesList").sortable("toArray");
        var newHierarchyOrder = JSON.stringify(r);

        var questionnaireId = $("#qnnrId").text();
        var df = "{qnnrID : " + questionnaireId + ", newOrder : " + newHierarchyOrder + "}";

        $.ajax({
            url: '/Question/Hierarchy',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: df,
            success: function (data) { window.location.href = data },
            failure: function (errMsg) {
                alert(errMsg);
            }
        });
    });
});