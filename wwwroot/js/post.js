allPost = document.querySelectorAll('.posts');
allPost.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        if (obj.target.localName != "button") {
            $.ajax({
                url: '/Posts/GetInfo',
                type: 'POST',
                data: { id: e.id },
                success: function(response) {
                    $('#carouselInner').empty();
                    console.log(response)
                    if (response.imageUrl.length > 1) {
                        $('#imageCarousel').append(`
                            <button class="carousel-control-prev" type="button" data-bs-target="#imageCarousel"
                            data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#imageCarousel"
                            data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                        `);
                    }
                    response.imageUrl.forEach((url, index) => {
                        const isActive = index === 0 ? 'active' : '';
                        $('#carouselInner').append(`
                            <div class="carousel-item ${isActive}">
                                <img src="${url}" class="d-block w-100" alt="Image ${index + 1}">
                            </div>
                        `);
                    });
                    $('#modalTitle').text(response.post.postTitle);
                    $('#modalMessage').text(response.post.postContent);
                    $('#modalComments').empty();
                    response.post.comment.forEach((url, index) => {
                        $('#modalComments').append(`${url.commentContent}`)
                    });
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