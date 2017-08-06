$(document).ready(function () {
    $(".btnActionRequest").click(function () {

        var action = $(this).attr('name');
        var requestId = $(this).attr('value');
        $(this).innerHTML = "Revoking...";

        var requestObj = new Object();
        requestObj.action = action;
        requestObj.requestId = requestId;



        $.ajax({
            url: "/IdeaRequest/ActionRequest",//config.serverPath + 'request',
            type: 'POST',
            data: requestObj,
            success: function (result) {
               toastr.success(result.toString());
               $(this).html = "Revoked";
               console.log(result);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).message;
                console.log(err);
            }
        });

    });

})