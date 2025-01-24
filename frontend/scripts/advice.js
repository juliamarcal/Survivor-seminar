document.querySelectorAll('.dropdown-title').forEach(button => {
    button.addEventListener('click', () => {
      const description = button.nextElementSibling;
      const icon = button.querySelector('i'); // Select the <i> element inside the button
  
      // Toggle the display of the description
      if (description.style.display === 'block') {
        description.style.display = 'none';
        icon.classList.remove('fa-caret-up');  // Change to down caret when closing
        icon.classList.add('fa-caret-down');
      } else {
        description.style.display = 'block';
        icon.classList.remove('fa-caret-down');  // Change to up caret when opening
        icon.classList.add('fa-caret-up');
      }
    });
});