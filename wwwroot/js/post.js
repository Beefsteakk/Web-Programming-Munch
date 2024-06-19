allPost = document.querySelectorAll('.posts');
allPost.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        console.log(e.id)
        if (obj.target.localName != "button" && obj.target.localName != "textarea" && obj.target.localName != "span") {
            $.ajax({
                url: '/Posts/GetInfo',
                type: 'POST',
                data: { id: e.id },
                success: function(response) {
                    console.log(response)
                    $('#carouselInner').empty();

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
                    $('#modalUsername').text(response.post.postAuthor.username);
                    $('#modalCreatedAt').text(response.post.postCreatedAt);
                    $('#modalMessage').text(response.post.postContent);
                    $('#modalComments').empty();
                    response.comments.forEach((comment, _) => {
                        $('#modalComments').append(`
                            <div class="row">
                                <div class="d-inline">
                                    <span class="fw-bold">${comment["commentAuthor"]["username"]}</span><span class="text-break"> ${comment["commentContent"]}</span>
                                </div>
                            </div>
                        `)
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

Buttons = document.querySelectorAll('.post-footer > button');
Buttons.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        obj.stopPropagation()
        if (e.textContent == "Like") {
            
        }
        else if (e.textContent == "Comment") {
            
        }
        else if (e.textContent == "Share") {
            navigator.clipboard.writeText("https://localhost:5001/Posts");
        }
    })
});