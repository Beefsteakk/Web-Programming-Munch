allPost = document.querySelectorAll('.post');
allPost.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        console.log(e.id)
        if (obj.target.localName != "button") {
            $.ajax({
                url: '/Posts/GetInfo',
                type: 'POST',
                data: { id: e.id },
                success: function(response) {
                    $('#carouselInner').empty();
                        console.log(response.imageUrl)
                        response.imageUrl.forEach((url, index) => {
                            const isActive = index === 0 ? 'active' : '';
                            $('#carouselInner').append(`
                                <div class="carousel-item ${isActive}">
                                    <img src="${url}" class="d-block w-100" alt="Image ${index + 1}">
                                </div>
                            `);
                        });
                    $('#modalTitle').text(response.title);
                    $('#modalMessage').text(response.message);
                    $('#infoModal').modal('show');
                },
                error: function() {
                    alert('An error occurred while fetching data.');
                }
            });
        }
    })
});

likeButton = document.querySelectorAll('.post-footer > button');
likeButton.forEach(function(e) {
    e.addEventListener('click', function(obj) {3
        obj.stopPropagation()
        console.log("Yses")
    })
});