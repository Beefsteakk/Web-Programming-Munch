const ignoredElements = new Set(['button', 'textarea', 'h4', 'span']);
const postContainer = document.querySelector('#post-container');
postContainer.addEventListener('click', function (obj) {
    const target = obj.target;
    if (!ignoredElements.has(target.localName) && !target.closest('.clickable') && !target.closest('.post-header')) {
        $.ajax({
            url: '/Posts/GetInfo',
            type: 'POST',
            data: { id: target.closest('.posts').id },
            success: function (response) {
                $('#main-modal')[0].classList.remove("modal-info-dialog");
                $('.modal-image').remove();
                if (response.post.postPictureURLs.length > 0 && $('.modal-image').length == 0) {
                    $('#modal-image-container')[0].insertAdjacentHTML("afterbegin", `
                            <div class="modal-image">
                                <div id="imageCarousel" class="carousel slide" data-bs-ride="carousel">
                                    <div class="carousel-inner" id="carouselInner">
                                    </div>
                                </div>
                            </div>
                        `)

                    $('#main-modal')[0].classList.add("modal-info-dialog");
                }

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
                if (response.post.postAuthorRestaurant.restPic == null) {
                    document.getElementById("modalProfilePic").src = "Images/chef.png"
                } else {
                    document.getElementById("modalProfilePic").src = `Images/RestaurantProfilePics/${response.post.postAuthorRestaurant.restPic}`
                }
                $('#modalUsername').text(response.post.postAuthorUser != null ? response.post.postAuthorUser.username : response.post.postAuthorRestaurant.restName);
                $('#modalCreatedAt').text(response.post.postCreatedAt);
                $('#modalMessage').text(response.post.postContent);
                $('#modalComments').empty();
                response.comments.forEach((comment, _) => {
                    $('#modalComments').append(`
                            <div class="row">
                                <div class="d-inline comments">
                                    <span class="fw-bold">${comment.commentAuthorUser != null ? comment.commentAuthorUser.username : comment.commentAuthorRestaurant.restName}</span><span class="text-break"> ${comment.commentContent}</span>
                                    ${comment.isOwnComment ? `<a class="editIcon" href=/Posts/SpecificComment/${comment.commentID}><i class="fa-regular fa-clipboard"></i></a>` : ''}
                                </div>
                            </div>
                        `)
                });
                if ($('#modalComments').children().length == 0) {
                    $('#modalComments').append(`
                            <div class="row">
                                <div class="d-inline comments">
                                    <span>No comments yet.</span>
                                </div>
                            </div>
                        `)
                }
                $('#infoModal').modal('show');
            },
            error: function () {
                alert('An error occurred while fetching data.');
            }
        });
    }
});

allLikeButton = document.querySelectorAll('#like-post-button')
allLikeButton.forEach(function (e) {
    e.addEventListener('click', function (event) {
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
            success: function (response) {
                if (response.status == "success") {
                    if (event.target.localName == "span" && event.target.innerText == "Like") event.target.innerText = "Unlike"
                    else if (event.target.localName == "span" && event.target.innerText == "Unlike") event.target.innerText = "Like"
                    else if (event.target.localName == "i" && event.target.nextElementSibling.innerText == "Like") event.target.nextElementSibling.innerText = "Unlike"
                    else if (event.target.localName == "i" && event.target.nextElementSibling.innerText == "Unlike") event.target.nextElementSibling.innerText = "Like"
                    else if (event.target.localName == "button" && event.target.children[1].textContent == "Like") event.target.children[1].textContent = "Unlike"
                    else if (event.target.localName == "button" && event.target.children[1].textContent == "Unlike") event.target.children[1].textContent = "Like"
                } else {
                    alert("Failed to like post.")
                }
            },
            error: function () {
                alert('An error occurred while fetching data.');
            }
        });
    });
});
