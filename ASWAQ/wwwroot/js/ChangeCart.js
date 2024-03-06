function ChangeCart(SID, change)
{
    var id = SID;
    var LocationChange = "#" + id;

    $.ajax
        ({
            url: "/Cart/ChangeCartAction",
            success: function (data) {
                $(LocationChange).html(data.total);
                $("#TPrice").html(data.totalPrice + " EGP");
            },
            data: { "ID": id, "change": change },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
}