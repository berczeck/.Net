function drop(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    $(evt.target).removeClass('over');

    var files = evt.originalEvent.dataTransfer.files;

    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (i = 0; i < files.length; i++) {
                data.append("file" + i, files[i]);
            }

            $.ajax({
                type: "POST",
                url: "/webapi/uploading",
                contentType: false,
                processData: false,
                data: data,
                success: function(res) {
                    //do something with our ressponse
                }
            });
        } else {
            alert("your browser sucks!");
        }
    }
}

function sendFile(evt) {
    var data = new FormData();
    data.append("file", $("[name=somefile]")[0].files[0]);
    $.ajax({
        type: "POST",
        url: "/webapi/uploading",
        contentType: false,
        processData: false,
        data: data,
        success: function (res) {
            alert(res);
        }
    });
}