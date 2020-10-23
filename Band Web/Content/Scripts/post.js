class UploadAdapter {
    constructor(loader) {
        this.loader = loader;
    }

    upload() {
        return new Promise((resolve, reject) => {
            const reader = this.reader = new window.FileReader();
            reader.addEventListener('load', () => { resolve({ default: reader.result }); })
            reader.addEventListener('error', error => { reject(error); });
            reader.addEventListener('abort', () => { reject(); });
            this.loader.file.then(file => { reader.readAsDataURL(file); });
        });
    }

    abort() {
        this.reader.abort();
    }
}

function AdapterPlugin(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => { return new UploadAdapter(loader) };
}

ClassicEditor.create(
    document.querySelector('#editor'),
    {
        extraPlugins: [AdapterPlugin],
		toolbar: {
			items: [
				'heading',
				'|',
				'bold',
				'italic',
				'link',
				'bulletedList',
				'numberedList',
				'|',
				'indent',
				'outdent',
				'|',
				'imageUpload',
				'imageInsert',
				'mediaEmbed',
				'|',
				'insertTable',
				'blockQuote',
				'undo',
				'redo',
				'|',
				'fontColor',
				'fontSize',
				'fontFamily',
				'|',
				'highlight',
				'CKFinder'
			]
		},
		language: 'en',
		image: {
			toolbar: [
				'imageTextAlternative',
				'imageStyle:full',
				'imageStyle:side'
			]
		},
		table: {
			contentToolbar: [
				'tableColumn',
				'tableRow',
				'mergeTableCells',
				'tableCellProperties'
			]
		},
		licenseKey: '',

	})
	.then(editor => {
		window.editor = editor;
    }
).catch(error => {
	console.error('Oops, something went wrong!');
	console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
	console.warn('Build id: 3gr4xvjhkhnk-scbtj9keuojb');
	console.error(error);
});