//$(document.body).on('click', '#btnCreateNewRequest' ,function(){

$(document).ready(function () {
    $(document.body).on('click', '#btnSearch', function () {
    //$("#btnSearch").click(function() {
        var ideas = $("#txtSearchIdeas").val();
        $('#recentSearchPageHeader').html('');
        $('#ajaxLoaderImg').show();

        debugger; 

        $.ajax({
            url: "/Innovation/SearchIdeas",
            type: 'POST',
            data: { searchTerm: ideas}, 
            success: function (result) {

                $("#searcResults").html(result);
                $('#recentSearchPageHeader').html('<h4 class="page-header">Your Search Result(s):</h4>');
                $('#ajaxLoaderImg').hide();

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