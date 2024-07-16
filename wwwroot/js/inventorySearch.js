document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('search-input');
    const categoryFilter = document.getElementById('category-filter');

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
});