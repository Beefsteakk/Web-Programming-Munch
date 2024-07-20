function followRestaurant(restId) {
    button = document.getElementById("follow-button");
    $.ajax({
        url: '/Restaurant/Follow',
        type: 'POST',
        data: { restaurantId: restId },
        success: function (response) {
            if (response.status == "success") {
                // if (button.innerText.trim() == "Follow") button.innerText = "Unfollow"
                // else if (button.innerText.trim() == "Unfollow") button.innerText = "Follow"
            } else {
                alert("Failed to follow/unfollow restaurant.")
            }
        },
        error: function () {
            alert('An error occurred while fetching data.');
        }
    });
}
