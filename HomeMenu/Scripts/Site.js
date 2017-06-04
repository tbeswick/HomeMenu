

$(document).ready(function () {


    $('a.addMenuItemIngredient').click(function () {


        var postObject = {
            UserId: $('#userId').val(),
            ItemId: $('#itemId').val(),
            IngredientId: $('#newIngredient').val(),

        };
        var postData = JSON.stringify(postObject);
        var uri = "/Menu/AddIngredient";

        $.ajax({
            beforeSend: function (xhrObj) {
                xhrObj.setRequestHeader("Content-Type", "application/json");
                xhrObj.setRequestHeader("Accept", "application/json");
            },
            type: "POST",
            url: uri,
            data: postData,
            dataType: "json",
            success: function (data) {

                location.reload();
                
            }, error: function (data) {
                location.reload();
            }


        });

        return false;

    });



    $('a.deleteMenuItemIngredient').click(function () {


        var postObject = {
            UserId: $('#userId').val(),
            ItemId: $('#itemId').val(),
            IngredientId: $('#newIngredient').val(),

        };
        var postData = JSON.stringify(postObject);
        var uri = "/Menu/DeleteIngredient";

        $.ajax({
            beforeSend: function (xhrObj) {
                xhrObj.setRequestHeader("Content-Type", "application/json");
                xhrObj.setRequestHeader("Accept", "application/json");
            },
            type: "POST",
            url: uri,
            data: postData,
            dataType: "json",
            success: function (data) {

                location.reload();

            }, error: function (data) {
                location.reload();
            }


        });

        return false;

    });


});



