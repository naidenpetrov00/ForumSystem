function AddComment(postId) {
    event.preventDefault();

    const token = $("#AddCommentForm input[name=__RequestVerificationToken]").val();
    const form = $('#AddCommentForm');
    const json = {
        PostId: postId,
        ParentCommentId: parseInt($('#ParentCommentId').val()),
        Content: tinymce.get("textareaTinymce").getBody().textContent
    };

    $.ajax(
        {
            url: "/Comments/Create",
            type: "POST",
            data: JSON.stringify(json),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { "X-CSRF-TOKEN": token },
            success: function (data) {
                console.log(data);
                console.log("succ");
            },
            error: function (xhr) {
                    console.log("err");
                if (xhr.status === 401) {
                    window.location = '/Account/Login';
                }
            }
        }
    )
}

function showAddCommentForm(parentId) {
    $("#AddCommentForm input[name='ParentCommentId']").val(parentId);
    $("#AddCommentForm").show();
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#AddCommentForm").offset().top
    }, 100);
}