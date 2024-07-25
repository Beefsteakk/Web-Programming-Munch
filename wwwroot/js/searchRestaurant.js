document.addEventListener("DOMContentLoaded", function () {
    initialiseAutocomplete();
})

function initialiseAutocomplete() {
    const restaurantSearchInput = document.getElementById("restaurant-search")
    if (!restaurantSearchInput) {
        console.error("Restaurant Search Bar is not loaded.")
    }

    $("#restaurant-search").autocomplete({
        source: function(request, response) {
            const term = restaurantSearchInput.value
            fetch(`/Posts/SearchRestaurant?term=${term}`)
                .then(response => response.json())
                .then(data => {
                    const autoCompleteData = data.map(restaurant => ({
                        label: restaurant.restName,
                        value: restaurant.restName,
                        id: restaurant.restID
                }))
                response(autoCompleteData)
            })
            .catch(error => {
                console.error('Error fetching data:', error);
                response([]);
            })
        },
        select: function(event, ui) {
            console.log('Selected i temId:', ui.item.id); // Log selected itemId
            window.location.href = `/RestProfile/SelectRestaurant?restID=${ui.item.id}`;
            response(`RestProfile/${ui.item.id}`)
            $("#itemId").val(ui.item.id);
        },
    })
}
