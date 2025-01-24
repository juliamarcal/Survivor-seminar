const loginForm = document.querySelector('.login-form');

// login form submission
loginForm.addEventListener('submit', function(event) {
  event.preventDefault();

  const username = loginForm.querySelector('input[placeholder="username"]').value;
  const password = loginForm.querySelector('input[placeholder="password"]').value;

  console.log('Login Form Data:', { username, password });

  window.location.href = 'dashboard.html';
});