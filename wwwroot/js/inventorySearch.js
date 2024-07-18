document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('search-input');
    const categoryFilter = document.getElementById('category-filter');
    const items = document.querySelectorAll('.item-row');

    function filterItems() {
        const searchTerm = searchInput.value.toLowerCase();
        const selectedCategory = categoryFilter.value;

        items.forEach(item => {
            const itemName = item.querySelector('.item-name').textContent.toLowerCase();
            const itemCategory = item.dataset.category;

            const matchesSearch = itemName.includes(searchTerm);
            const matchesCategory = selectedCategory === '' || itemCategory === selectedCategory;

            if (matchesSearch && matchesCategory) {
                item.style.display = '';
            } else {
                item.style.display = 'none';
            }
        });
    }

    searchInput.addEventListener('input', filterItems);
    categoryFilter.addEventListener('change', filterItems);

    // Add to Cart functionality
    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', async (e) => {
            const itemId = e.target.getAttribute('data-item-id');
            console.log('Adding item to cart, ItemID:', itemId); // Log the itemId
            try {
                const response = await fetch('/Cart/AddToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(itemId)
                });

                if (response.ok) {
                    alert('Item added to cart successfully!');
                } else {
                    const errorText = await response.text();
                    console.error('Error response:', errorText); // Log the error response
                    alert(`Failed to add item to cart: ${errorText}`);
                }
            } catch (error) {
                console.error('Error:', error);
                alert('An error occurred while adding the item to the cart.');
            }
        });
    });
});