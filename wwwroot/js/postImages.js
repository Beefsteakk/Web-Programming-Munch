document.getElementById('picturesInput').addEventListener('change', function (event) {
    const files = Array.from(event.target.files);
    const previewContainer = document.getElementById('imagePreviewContainer');
    previewContainer.innerHTML = ''; // Clear existing previews

    if (files.length > 10) {
        document.getElementById('picturesInput').value = ""
        const para = document.createElement("p")
        para.innerHTML = "You can only attach a maximum of 10 images in a post."
        para.style.color = "red"
        previewContainer.appendChild(para)
        return
    }

    files.forEach((file, index) => {
        const reader = new FileReader();
        reader.onload = function (e) {
            const imgWrapper = document.createElement('div');
            imgWrapper.style.position = 'relative';
            imgWrapper.style.display = 'inline-block';
            imgWrapper.style.margin = '10px';

            const img = document.createElement('img');
            img.src = e.target.result;
            img.style.maxWidth = '200px';
            img.style.border = '2px solid #ddd';
            img.style.borderRadius = '10px';
            img.style.boxShadow = '0 4px 8px rgba(0,0,0,0.1)';

            const removeButton = document.createElement('button');
            removeButton.textContent = 'Remove';
            removeButton.style.position = 'absolute';
            removeButton.style.top = '5px';
            removeButton.style.right = '5px';
            removeButton.style.backgroundColor = 'red';
            removeButton.style.color = 'white';
            removeButton.style.border = 'none';
            removeButton.style.borderRadius = '5px';
            removeButton.style.cursor = 'pointer';

            removeButton.addEventListener('click', function () {
                console.log(index)
                imgWrapper.remove();
                console.log(files)
                files.splice(index, 1, null); // Remove file from array
                console.log(files)
                updateFileInput(files);
            });

            imgWrapper.appendChild(img);
            imgWrapper.appendChild(removeButton);
            previewContainer.appendChild(imgWrapper);
        };
        reader.readAsDataURL(file);
    })
});

function updateFileInput(files) {
    const picturesInput = document.getElementById('picturesInput');
    const dataTransfer = new DataTransfer();
    for (var file of files) {
        if (file == null) continue
        dataTransfer.items.add(file)
    }
    picturesInput.files = dataTransfer.files;
}
