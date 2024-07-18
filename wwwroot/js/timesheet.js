let modal = document.getElementById("shiftFormModal");
let span = document.getElementsByClassName("close")[0];
let form = document.getElementById("shiftForm");

function showShiftForm(element, date) {
    document.getElementById("shiftId").value = "00000000-0000-0000-0000-000000000000"; // Empty Guid
    document.getElementById("shiftDate").value = date;
    form.reset();
    modal.style.display = "block";
}

span.onclick = function() {
    modal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

form.addEventListener('submit', function(e) {
    e.preventDefault();
    console.log("Form submitted");

    let formData = new FormData(form);
    let shiftData = Object.fromEntries(formData.entries());
    
    shiftData.SheetID = shiftData.SheetID || "00000000-0000-0000-0000-000000000000";
    shiftData.EmployeeID = shiftData.EmployeeID;
    
    // Parse the date and time values correctly
    let shiftDate = new Date(shiftData.Day);
    let startTime = shiftData.StartTime.split(':');
    let endTime = shiftData.EndTime.split(':');
    
    let startDateTime = new Date(shiftDate);
    startDateTime.setHours(parseInt(startTime[0]), parseInt(startTime[1]));
    
    let endDateTime = new Date(shiftDate);
    endDateTime.setHours(parseInt(endTime[0]), parseInt(endTime[1]));
    
    shiftData.Day = shiftDate.toISOString();
    shiftData.StartTime = startDateTime.toISOString();
    shiftData.EndTime = endDateTime.toISOString();

    console.log("Shift data:", shiftData);

    fetch('/Timesheet/AddOrUpdateShift', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(shiftData),
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(err => { throw err; });
        }
        return response.json();
    })
    .then(data => {
        console.log("Response data:", data);
        if (data.success) {
            modal.style.display = "none";
            location.reload();
        } else {
            alert("Failed to save shift. Error: " + (data.error || "Unknown error"));
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert("An error occurred. Error details: " + JSON.stringify(error));
    });
});

function updateShiftTimes() {
    let shiftType = document.getElementById("shiftType").value;
    let startTime = document.getElementById("startTime");
    let endTime = document.getElementById("endTime");

    if (shiftType === "Morning") {
        startTime.value = "10:30";
        endTime.value = "18:30";
    } else if (shiftType === "Afternoon") {
        startTime.value = "15:00";
        endTime.value = "23:00";
    }
}

function deleteShift(shiftId) {
    if (confirm("Are you sure you want to delete this shift?")) {
        fetch('/Timesheet/DeleteShift', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ id: shiftId }),
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert("Failed to delete shift. Please try again.");
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert("An error occurred while deleting the shift. Please check the console and try again.");
        });
    }
}

function editShift(shiftId) {
    fetch(`/Timesheet/GetShift?id=${shiftId}`)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(shift => {
        document.getElementById("shiftId").value = shift.sheetID;
        document.getElementById("shiftDate").value = new Date(shift.day).toISOString().split('T')[0];
        document.getElementById("employeeId").value = shift.employeeID;
        document.getElementById("shiftType").value = shift.shiftType;
        document.getElementById("startTime").value = shift.startTime.substring(0, 5);
        document.getElementById("endTime").value = shift.endTime.substring(0, 5);
        modal.style.display = "block";
    })
    .catch(error => {
        console.error('Error:', error);
        alert("An error occurred while fetching shift data. Please check the console and try again.");
    });
}

// Log when the script has loaded
console.log("timesheet.js loaded");