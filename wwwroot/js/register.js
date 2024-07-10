grecaptcha.ready(function () {
    grecaptcha.execute('6LfLmX8dAAAAAGbTIkC40y7eHl9GptSMiJQDI8L8', { action: 'Login' }).then(function (token) {
        document.getElementById("g-recaptcha-response").value = token;
    });
});

function check(input) {
    if (input.value != document.getElementById('password').value) {
        input.setCustomValidity('Password Must be Matching.');
    } else {
        // input is valid -- reset the error message
        input.setCustomValidity('');
    }
}

document.getElementById('avatarImg').addEventListener('click', function () {
    document.getElementById('fileInput').click();
});

document.getElementById('fileInput').addEventListener('change', function (event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.onload = function (e) {
        document.getElementById('avatarImg').src = e.target.result;
    }
    reader.readAsDataURL(file);
});

document.addEventListener("DOMContentLoaded", function() {
    var message = document.getElementById("register-failure-message");
    if (message) {
        setTimeout(function() {
            message.style.display = 'none';
        }, 3000); // Hide after 3 seconds
    }
});
