//-------------------------------CKeditor5--------------------------------------

ClassicEditor
    .create(document.querySelector('#editor'), {
        placeholder: 'Please Type Here...',
        simpleUpload: {
            // The URL that the images are uploaded to.
            uploadUrl: '/posts/UploadImage',

            // Enable the XMLHttpRequest.withCredentials property.
            withCredentials: true,

            // Headers sent along with the XMLHttpRequest to the upload server.
            headers: {
                'X-CSRF-TOKEN': 'CSRF-Token',
                Authorization: 'Bearer <JSON Web Token>'
            }
        },
        toolbar: {
            items: [
                'heading',
                'bold',
                'italic',
                'link',
                'bulletedList',
                'numberedList',
                '|',
                'indent',
                'outdent',
                '|',
                'fontColor',
                'fontFamily',
                'fontSize',
                'fontBackgroundColor',
                '|',
                'imageUpload',
                'mediaEmbed',
                'insertTable',
                'blockQuote',
                'undo',
                'redo'
            ]
        },
        language: 'en',
        image: {
            toolbar: [
                'imageStyle:full',
                'imageStyle:side',
                '|',
                'imageTextAlternative'
            ],
        },
        table: {
            contentToolbar: [
                'tableColumn',
                'tableRow',
                'mergeTableCells',
                'tableCellProperties',
                'tableProperties'
            ]
        },
        licenseKey: '',
    })
    .then(editor => {
        window.editor = editor;
    })
    .catch(error => {
        console.error('Oops, something went wrong!');
        console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
        console.error(error);
    });

//-------------------------------Common js--------------------------------------

//get post like count
$(".like").hover(function () {
    let like = $(this).attr('id');
    $.ajax({
        url: '/posts/getAccountList',
        type: 'post',
        data: {
            id: $(this).attr('id').slice(2),
            type: 'like'
        },
        success: function (data) {
            if (data.length == 0) {
                $(`#${like}`).find('#like_tooltiptext').text('Nothing...');
            }
            else {
                $.each(data, function (i, item) {
                    $(`#${like}`).find('#like_tooltiptext').text('');
                    $(`#${like}`).find('#like_tooltiptext').append(item.UserAccount);
                })
            }
        },
        error: function () {
            $(`#${like}`).find('#like_tooltiptext').text('ERROR');
        }
    });
}, function () { });

//get posy reply count
$(".reply").hover(function () {
    let reply = $(this).attr('id');
    $.ajax({
        url: '/posts/getAccountList',
        type: 'post',
        data: {
            id: $(this).attr('id').slice(2),
            type: 'reply'
        },
        success: function (data) {
            if (data.length == 0) {
                $('#' + reply).find('#reply_tooltiptext').text('Nothing...');
            }
            else {
                $.each(data, function (i, item) {
                    $(`#${reply}`).find('#reply_tooltiptext').text('');
                    $(`#${reply}`).find('#reply_tooltiptext').append(item.UserAccount);
                })
            }
        },
        error: function () {
            $(`#${reply}`).find('#reply_tooltiptext').text('ERROR');
        }
    });
}, function () { });

//post like click
$(".like").click(function () {
    var cancel = 0;
    if ($(this).attr('data-cancel').valueOf == '0') {
        cancel = 1;
    }
    //$(this).removeClass();
    //$(this).addClass();
    $.ajax({
        url: '/posts/userAction',
        type: 'post',
        data: {
            id: $(this).attr('id').slice(2),
            cancel: cancel
        },
        success: function (data) {
            if (data.Error != null) {
                alert(data.Error)
            }
        },
        error: function () {
            alert("Server Error...")
        }
    });
});