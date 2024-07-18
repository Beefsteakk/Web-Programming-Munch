let addModal = document.getElementById("shiftFormModal");
let editModal = document.getElementById("editShiftFormModal");
let addSpan = addModal.getElementsByClassName("close")[0];
let editSpan = editModal.getElementsByClassName("close")[0];
let addForm = document.getElementById("shiftForm");
let editForm = document.getElementById("editShiftForm");

function showShiftForm(element, date) {
    document.getElementById("shiftDate").value = date;
    addForm.reset();
    document.getElementById("shiftDate").value = date; // Set the default date
    addModal.style.display = "block";
}

function showEditShiftForm(shiftId) {
    fetch(`/Timesheet/GetShift?id=${shiftId}`)
        .then(response => response.json())
        .then(data => {
            document.getElementById("editShiftId").value = data.sheetID;
            document.getElementById("editShiftDate").value = data.day;
            document.getElementById("editEmployeeId").value = data.employeeID;
            document.getElementById("editShiftType").value = data.shiftType;
            document.getElementById("editStartTime").value = data.startTime;
            document.getElementById("editEndTime").value = data.endTime;
            editModal.style.display = "block";
        })
        .catch(error => {
            console.error('Error:', error);
            alert("An error occurred while fetching the shift data. Please try again.");
        });
}

addSpan.onclick = function() {
    addModal.style.display = "none";
}

editSpan.onclick = function() {
    editModal.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == addModal) {
        addModal.style.display = "none";
    }
    if (event.target == editModal) {
        editModal.style.display = "none";
    }
}

addForm.addEventListener('submit', function(e) {
    e.preventDefault();
    submitShiftForm(addForm, '/Timesheet/AddShift');
});

editForm.addEventListener('submit', function(e) {
    e.preventDefault();
    submitShiftForm(editForm, '/Timesheet/UpdateShift');
});

function submitShiftForm(form, url) {
    let formData = new FormData(form);
    let shiftData = Object.fromEntries(formData.entries());
    
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

    fetch(url, {
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
        if (data.success) {
            addModal.style.display = "none";
            editModal.style.display = "none";
            location.reload();
        } else {
            alert("Failed to save shift. Error: " + (data.error || "Unknown error"));
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert("An error occurred. Error details: " + JSON.stringify(error));
    });
}

function deleteShift() {
    const shiftId = document.getElementById("editShiftId").value;
    if (confirm("Are you sure you want to delete this shift?")) {
        fetch(`/Timesheet/DeleteShift?id=${shiftId}`, {
            method: 'POST',
        })
        .then(response => {
            if (!response.ok) {
                return response.json().then(err => { throw err; });
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                editModal.style.display = "none";
                location.reload();
            } else {
                alert("Failed to delete shift. Error: " + (data.error || "Unknown error"));
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert("An error occurred while deleting the shift. Error details: " + JSON.stringify(error));
        });
    }
}

function updateShiftTimes() {
    let shiftType = document.getElementById("shiftType").value;
    let startTime = document.getElementById("startTime");
    let endTime = document.getElementById("endTime");

    if (shiftType === "AM") {
        startTime.value = "10:30";
        endTime.value = "18:30";
    } else if (shiftType === "PM") {
        startTime.value = "15:00";
        endTime.value = "23:00";
    }
}

function updateEditShiftTimes() {
    let shiftType = document.getElementById("editShiftType").value;
    let startTime = document.getElementById("editStartTime");
    let endTime = document.getElementById("editEndTime");

    if (shiftType === "AM") {
        startTime.value = "10:30";
        endTime.value = "18:30";
    } else if (shiftType === "PM") {
        startTime.value = "15:00";
        endTime.value = "23:00";
    }
}

console.log("timesheet.js loaded");