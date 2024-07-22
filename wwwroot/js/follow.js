function followRestaurant(restId) {
    button = $("#follow-button");
    $.ajax({
        url: '/Restaurant/Follow',
        type: 'POST',
        data: { restaurantId: restId },
        success: function (response) {
            if (response.status == "success") {
                if (response.type == "follow") button.find("span").text("Unfollow")
                else if (response.type == "unfollow") button.find("span").text("Follow")
            } else {
                alert("Failed to follow/unfollow restaurant.")
            }
        },
        error: function () {
            alert('An error occurred while fetching data.');
        }
    });
}
