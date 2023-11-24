document.getElementById('uploadForm').addEventListener('submit', function (e) {
    e.preventDefault();
    var files = document.getElementById('formFileMultiple').files;

    if (files.length === 0 || Array.from(files).some(file => !file.name.endsWith('.xlsx'))) {
        alert("Please select .xlsx files");
        return;
    }
    if (files.length === 0) {
        alert("No files selected");
        return;
    }
    var formData = new FormData(this);
    fetch(window.location.origin + '/Weather/UploadFiles', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            alert(data.message);
        })
        .catch(error => {
            console.error('Error occured while uploading files:', error);
        });
});