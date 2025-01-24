const loginForm = document.querySelector('.login-form');

loginForm.addEventListener('submit', function(event) {
  event.preventDefault();

  const email = loginForm.querySelector('input[placeholder="email"]').value;
  const password = loginForm.querySelector('input[placeholder="password"]').value;

  fetch('/login/auth', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      email: email,
      password: password
    })
  })
      .then(response => response.json())
      .then(data => {
        if (data.operation === 'successiful') {
          window.location.href = '/dashboard';
        } else {
          console.log('Login failed:', data.operation);
          alert('Login failed: ' + data.operation);
        }
      })
      .catch(error => {
        console.error('Error:', error);
        alert('An error occurred during login.');
      });
});