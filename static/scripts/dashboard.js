const menuToggle = document.querySelector('.menu-toggle');
const navMenu = document.querySelector('header');

menuToggle.addEventListener('click', () => {
  navMenu.classList.toggle('nav-open');
});