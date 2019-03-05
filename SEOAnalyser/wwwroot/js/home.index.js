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

    if (!$('#formAnalyse #checkAllWord').is(':checked')
        && !$('#formAnalyse #checkExternalLink').is(':checked')
        && !$('#formAnalyse #checkMetadata').is(':checked')) {

        $('#formAnalyse #analyseInputVal').text("Please tick at least one filter/check option");
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
        title: "Word/Url"
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

    var grid = $("#gridAllWord");
    GenerateGrid(formAction, fdata, grid);
}

function GetExtLink(fdata) {

    $('#divExtLink').show();
  
    var formAction = "/api/ExternalLink";
    var grid = $("#gridExtLink");
    GenerateGrid(formAction, fdata, grid);

    
}

function GetMeta(fdata) {

    $('#divMeta').show();
   
    var formAction = "/api/MetadataWord";
    var grid = $("#gridMeta");
    GenerateGrid(formAction,fdata,grid);
   
}

function GenerateGrid(url, data, grid) {

   grid.kendoGrid({
        dataSource: {
            transport: {
                read: function (e) {
                    $.ajax({
                        url: url,
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(data),
                        success: function (result) {
                            e.success(result);
                            $('#formAnalyse #analyseInputVal').text(result.errorMessage);
                        },
                        error: function (result) {
                            // notify the data source that the request failed
                            e.error(result);
                            $('#formAnalyse #analyseInputVal').text(result.errorMessage);
                        }
                    });
                    //type: 'POST',
                    //url: url,
                    //contentType: "application/json",
                    //dataType: "json",
                    //beforeSend: function (xhr) {
                    //    xhr.setRequestHeader("RequestVerificationToken",
                    //        $('input:hidden[name="__RequestVerificationToken"]').val());
                    //}
                },
                parameterMap: function (options) {
                    //return JSON.stringify(data);
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
       persistSelection: true,
        columns: columns
   }).data("kendoGrid");
}





