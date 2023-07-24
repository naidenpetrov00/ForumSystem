function sendVote(postId, isUpVote) {
	let token = $("#votesForm input[name=__RequestVerificationToken]").val();
	let json = {
		postId: postId,
		isUpVote: isUpVote
	};

	$.ajax(
		{
			url: "/api/votes",
			type: "POST",
			data: JSON.stringify(json),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			headers: { "X-CSRF-TOKEN": token },
			success: function (data) {
				$("#votesCount").html(data.votesCount)
			}
		}
	)
}