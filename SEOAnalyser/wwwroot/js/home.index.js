$(document).ready(function () {

    $('#divAllWords').hide();
    $('#divMeta').hide();
    $('#divExtLink').hide();


});

var $loading = $('#loadingDiv').hide();
//jQuery.ajaxSetup({
//    beforeSend: function () {
//        $('#loadingDiv').show();
//    },
//    complete: function () {
//        $('#loadingDiv').hide();
//    },
//    success: function () { }
//});

$(document)
    .ajaxStart(function () {
        $loading.show();
    })
    .ajaxStop(function () {
        $loading.hide();
    });

function SubmitForm(frm, caller) {
    caller.preventDefault();

    $('#divAllWords').hide();
    $('#divMeta').hide();
    $('#divExtLink').hide();

    var fdata = {
        analyseInput: $('#formAnalyse #analyseInput').val(),
        checkStopWord: $('#formAnalyse #checkStopWord').is(':checked'),
        checkAllWord: $('#formAnalyse #checkAllWord').is(':checked'),
        checkExternalLink: $('#formAnalyse #checkExternalLink').is(':checked'),
        checkMetadata: $('#formAnalyse #checkMetadata').is(':checked')

    };

    $('#formAnalyse #analyseInputVal').text("");

    if ($('#formAnalyse #analyseInput').val() === "") {
        $('#formAnalyse #analyseInputVal').text("Please enter URL/Text");
        return;
    }

    //get all words
    if ($('#formAnalyse #checkAllWord').is(':checked')) {
    console.log("checkAllWord is checked");
    GetAllWords(fdata);
    }

    //get external links
    if ($('#formAnalyse #checkExternalLink').is(':checked')) {
        console.log("checkExternalLink is checked");
        GetExtLink(fdata);
    }


    //get metadata
    if ($('#formAnalyse #checkMetadata').is(':checked')) {
        console.log("checkMetadata is checked");
        GetMeta(fdata);
    }


}

var columns = [

    {
        field: "word",
        title: "Word"
    },

    {
        field: "total",
        title: "Total Occurence"
    }
];

function GetAllWords(fdata) {
  
    var formAction = "/api/AllWord";
    
    //get all words
    $('#divAllWords').show();

    //$.ajax({
    //    type: 'POST',
    //    url: formAction,
    //    contentType: "application/json",
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader("RequestVerificationToken",
    //            $('input:hidden[name="__RequestVerificationToken"]').val());
    //    },
    //    data: JSON.stringify(fdata)
    //}).done(function (result) {
        
    //    if (result.Status === "Success") {
    //        console.log(JSON.stringify(result.Data));

    //    } else {
    //        console.log(result.ErrorMessage);
    //    }
    //    });

    $("#gridAllWord").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    type: 'POST',
                    url: formAction,
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("RequestVerificationToken",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    }
                },
                parameterMap: function (options) {
                    return JSON.stringify(fdata);
                }
            },
            schema: {
                total: "count",
                data: "data"
            },
            pageSize: 10,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        sortable: {
            mode: "single",
            allowUnsort: true
        },
        pageable: {
            refresh: true,
            pageSizes: [5, 10, 20],
            buttonCount: 5
        },
        scrollable: false,
        columns: columns
    });
    

}

function GetExtLink(fdata) {

    $('#divExtLink').show();
  
    var formAction = "/api/ExternalLink";
   
    //$.ajax({
    //    type: 'POST',
    //    url: formAction,
    //    contentType: "application/json",
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader("RequestVerificationToken",
    //            $('input:hidden[name="__RequestVerificationToken"]').val());
    //    },
    //    data: JSON.stringify(fdata)
    //}).done(function (result) {
      
    //    if (result.Status === "Success") {
    //        console.log(JSON.stringify(result.Data));
    //    } else {
    //        console.log(result.ErrorMessage);
    //    }
    //});

    $("#gridExtLink").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    type: 'POST',
                    url: formAction,
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("RequestVerificationToken",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    }
                },
                parameterMap: function (options) {
                    return JSON.stringify(fdata);
                }
            },
            schema: {
                total: "count",
                data: "data"
            },
            pageSize: 10,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        sortable: {
            mode: "single",
            allowUnsort: true
        },
        pageable: {
            refresh: true,
            pageSizes: [5, 10, 20],
            buttonCount: 5
        },
        scrollable: false,
        columns: columns
    });

}

function GetMeta(fdata) {

    $('#divMeta').show();

    var formAction = "/api/MetadataWord";
   
    //$.ajax({
    //    type: 'POST',
    //    url: formAction,
    //    contentType: "application/json",
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader("RequestVerificationToken",
    //            $('input:hidden[name="__RequestVerificationToken"]').val());
    //    },
    //    data: JSON.stringify(fdata)
    //}).done(function (result) {
       
    //    if (result.Status === "Success") {
    //        console.log(JSON.stringify(result.Data));
    //    } else {
    //        console.log(result.ErrorMessage);
    //    }
    //});

    $("#gridMeta").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    type: 'POST',
                    url: formAction,
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("RequestVerificationToken",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    }
                },
                parameterMap: function (options) {
                    return JSON.stringify(fdata);
                }
            },
            schema: {
                total: "count",
                data: "data"
            },
            pageSize: 10,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        sortable: {
            mode: "single",
            allowUnsort: true
        },
        pageable: {
            refresh: true,
            pageSizes: [5, 10, 20],
            buttonCount: 5
        },
        scrollable: false,
        columns: columns
    });

}





