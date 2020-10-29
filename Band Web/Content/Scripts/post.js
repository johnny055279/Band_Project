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