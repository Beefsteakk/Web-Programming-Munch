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

                    if (response.post.postPictureURLs.length > 1) {
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
                    response.post.postPictureURLs.forEach((url, index) => {
                        const isActive = index === 0 ? 'active' : '';
                        $('#carouselInner').append(`
                            <div class="carousel-item ${isActive}">
                                <img src="Images/PostPics/${url}" class="d-block w-100" alt="Image ${index + 1}">
                            </div>
                        `);
                    });
                    $('#modalUsername').text(response.post.postAuthorUser != null ? response.post.postAuthorUser.username : response.post.postAuthorRestaurant.restName);
                    $('#modalCreatedAt').text(response.post.postCreatedAt);
                    $('#modalMessage').text(response.post.postContent);
                    $('#modalComments').empty();
                    response.comments.forEach((comment, _) => {
                        $('#modalComments').append(`
                            <div class="row">
                                <div class="d-inline">
                                    <span class="fw-bold">${comment.commentAuthorUser != null ? comment.commentAuthorUser.username : comment.commentAuthorRestaurant.restName}</span><span class="text-break"> ${comment.commentContent}</span>
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

document.getElementById('like-post-button').addEventListener('click', function (event) {
    let id;
    if (event.target.localName == "button") {
        id = event.target.parentElement.parentElement.id
    } else {
        id = event.target.parentElement.parentElement.parentElement.id
    }

    $.ajax({
        url: '/Posts/LikePost',
        type: 'POST',
        data: { postId: id },
        success: function(response) {
            if (response.status == "success") {
                if (event.target.localName == "span" && event.target.innerText == "Like") event.target.innerText = "Unlike"
                else if (event.target.localName == "span" && event.target.innerText == "Unlike") event.target.innerText = "Like"
                else if (event.target.localName == "button" && event.target.children[1].textContent == "Like") event.target.children[1].textContent = "Unlike"
                else if (event.target.localName == "button" && event.target.children[1].textContent == "Unlike") event.target.children[1].textContent = "Like"
            } else {
                alert("Failed to like post.")
            }
        },
        error: function() {
            alert('An error occurred while fetching data.');
        }
    });
});
