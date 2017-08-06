$(document).ready(function () {
    //$(".btnRequest").click(function () {
    $(document.body).on('click', '.btnRequest', function () {
        var action = $(this).attr('name');
        var ideaId = $(this).attr('value');

        var id = this.id;

        $('#' + id).html('<ion-icon name="send"></ion-icon>&nbsp;Requesting...');

        debugger;
        var requestObj = new Object();
        requestObj.action = action;
        requestObj.ideaId = ideaId;



        $.ajax({
            url: "/Innovation/CreateRequest",//config.serverPath + 'request',
            type: 'POST',
            data: requestObj,
            success: function (result) {
                
                toastr.success(result.toString());

                $('#' + id).html('<ion-icon name="send"></ion-icon>&nbsp;Sent');


                //toastr.success(result.toString());
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