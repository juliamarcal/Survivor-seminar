function validateForm() {
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirmPassword").value;
    const errorMessage = document.getElementById("errorMessage");

    errorMessage.textContent = "";

    if (password !== confirmPassword) {
        errorMessage.textContent = "Passwords do not match!";
        return false;
    }

    submitForm();
    return false;
}

function submitForm() {
    const name = document.getElementById("name").value;
    const surname = document.getElementById("surname").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const workTitle = document.getElementById("workTitle").value;
    const birthDate = document.getElementById("birthDate").value;
    const gender = document.getElementById("gender").value;

    const employeeData = {
        name: name,
        surname: surname,
        email: email,
        password: password,
        workTitle: workTitle,
        birthDate: birthDate,
        gender: gender
    };

    fetch('/login/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(employeeData)
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            throw new Error('Something went wrong');
        }
    })
    .then(data => {
        alert('Employee registered successfully!');
        console.log('Server Response:', data);
        window.location.href = 'login'
    })
    .catch(error => {
        alert('Error: ' + error.message);
        console.error('Error:', error);
    });
}