
$("#btnSearchReport").click(function () {
    submitForm();
});

$("#startDate").datetimepicker({ format: "MM/DD/YYYY" });
$("#endDate").datetimepicker({ format: "MM/DD/YYYY" });

//check for: start date should not be greater than end date

function searchReports() {
    

    var startdate = document.getElementById("startDateField").value;  //dateOfCall
    var enddate = document.getElementById("endDateField").value;  //dateOfCall

    var momentStartDate = moment(startdate);
    var momentEndDate = moment(enddate);

    if (startdate != "" && enddate != "") {
        if (momentStartDate.isAfter(momentEndDate) || momentEndDate.isBefore(momentStartDate)) {
            alert("Enter valid date range!");
            return;
        }
    }
    else if ((startdate == "" && enddate != "") || (startdate != "" && enddate == "")) {
        alert("please enter start date as well as end date.");
        return;
    }

    var field2 = document.getElementById("BasicInputField2").value;   //company
    var field3 = document.getElementById("BasicInputField3").value;  //clientLead
    var field4 = document.getElementById("BasicInputField4").value;   //deltekSalesLead
    var field5 = document.getElementById("BasicInputField5").value;    //infotekLead

 

    var data = JSON.stringify({
        'startdate': startdate,
        'enddate': enddate,
        'field2': field2,
        'field3': field3,
        'field4': field4,
        'field5': field5
    });
    
    $.ajax({
        url: '/ReportMaster/Search',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: data,
        success: function (data) {
            loadGrid(data);
        },//$("").trigger("reloadGrid"); },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });

}