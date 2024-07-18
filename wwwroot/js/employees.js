function openPopup(EmployeeId) {
    document.getElementById('popup-container').style.display = 'flex';
    
    fetch(`/Employees/Edit/${EmployeeId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            document.getElementById('photo-preview').src = `/images/${data.employeePic}`;
            document.getElementById('name').value = data.employeeName;
            document.getElementById('title').value = data.role;
            document.getElementById('department').value = data.department;
            document.getElementById('hire-date').value = data.hireDate.split('T')[0];
            document.getElementById('email').value = data.email;
            document.getElementById('phone').value = data.phoneNumber;
            document.getElementById('employee-id').value = data.employeeID;
        })
        .catch(error => {
            console.error('Error fetching employee data:', error);
            alert('An error occurred while fetching the employee data.');
        });
}

function closePopup() {
    document.getElementById('popup-container').style.display = 'none';
}

document.getElementById('edit-form').addEventListener('submit', function(event) {
    event.preventDefault();

    var formData = new FormData(this);
    var photoInput = document.getElementById('photo');

    if (photoInput.files.length > 0) {
        formData.append('photo', photoInput.files[0]);
    }

    var employeeId = document.getElementById('employee-id').value;

    fetch(`/Employees/Edit`, {
        method: 'POST',
        body: formData
    })
    .then(response => {
        if (response.ok) {
            closePopup();
            // alert('Employee updated successfully. Please Refresh the page to see the changes.');
            // Optionally, you can refresh the page or update the UI with the new data
        } else {
            response.text().then(text => alert('Failed to update employee: ' + text));
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while updating the employee.');
    });
});



function previewImage(event) {
    var reader = new FileReader();
    reader.onload = function(){
        var output = document.getElementById('photo-preview');
        output.src = reader.result;
    };
    reader.readAsDataURL(event.target.files[0]);
}


function confirmDelete() {
    if (confirm("Are you sure you want to delete this employee?")) {
        var employeeId = document.getElementById('employee-id').value;
        fetch(`/Employees/Delete/${employeeId}`, {
            method: 'DELETE'
        })
        .then(response => {
            if (response.ok) {
                closePopup();
                // Optionally, refresh the employee list or update UI
                alert('Employee deleted successfully.');
                location.reload(); // Refresh the page
            } else {
                response.text().then(text => alert('Failed to delete employee: ' + text));
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while deleting the employee.');
        });
    }
}

document.getElementById('edit-form').addEventListener('submit', function(event) {
    event.preventDefault();

    var formData = new FormData(this);
    var photoInput = document.getElementById('photo');

    if (photoInput.files.length > 0) {
        formData.append('photo', photoInput.files[0]);
    }

    var employeeId = document.getElementById('employee-id').value;

    fetch(`/Employees/Edit`, {
        method: 'POST',
        body: formData
    })
    .then(response => {
        if (response.ok) {
            closePopup();
            alert('Employee updated successfully.');
            location.reload(); // Refresh the page
        } else {
            response.text().then(text => alert('Failed to update employee: ' + text));
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while updating the employee.');
    });
});


function searchEmployees() {
    var searchTerm = document.getElementById("search-input").value;
    if (searchTerm.trim() !== "") {
        // Construct URL with query string parameter
        var url = `/Employees/Search?query=${encodeURIComponent(searchTerm)}`;
        window.location.href = url; // Redirect to search URL
    }
}

function toggleSorting(column) {
    var currentSort = '@ViewData["CurrentSort"]';
    var newSort = '';

    if (currentSort === '') {
        newSort = column + '_asc'; // Default to ascending order
    } else if (currentSort === column + '_asc') {
        newSort = column + '_desc'; // Toggle to descending order
    } else if (currentSort === column + '_desc') {
        newSort = column + '_asc'; // Toggle back to ascending order
    }

    // Navigate to the same page with new sort order
    window.location.href = '?sortOrder=' + newSort;
}







