const menuToggle = document.querySelector('.menu-toggle');
const navMenu = document.querySelector('header');

menuToggle.addEventListener('click', () => {
  navMenu.classList.toggle('nav-open');
});

function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

function selectItem(element) {
    var button = document.getElementById("dropdownButton");
    button.textContent = element.textContent + " "; // Update button text with selected item
    
    // Create the icon element
    var icon = document.createElement("i");
    icon.className = "fa-solid fa-caret-down";
    
    // Append the icon element to the button
    button.appendChild(icon);
    
    toggleDropdown(); // Close the dropdown after selection
}
  

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}


document.getElementById('search-icon').addEventListener('click', function() {
    var searchInput = document.getElementById('search-input');
    searchInput.classList.toggle('show');
    searchInput.focus();
});

// add coach
const popup = document.getElementById('add-coach-popup');

document.getElementById('add-btn').addEventListener('click', function() {
    popup.style.display = 'block';
});

function closePopup() {
    popup.style.display = 'none';
}

document.getElementById('add-coach-form').addEventListener('submit', function(event) { // Handle form submission
    event.preventDefault();

    const newCoach = {
        id: document.getElementById('coach-id').value,
        email: document.getElementById('coach-email').value,
        name: document.getElementById('coach-name').value,
        surname: document.getElementById('coach-surname').value,
        birth_date: document.getElementById('coach-birth-date').value,
        gender: document.getElementById('coach-gender').value,
        work: document.getElementById('coach-work').value
    };

    // TODO: Handle adding the new coach to your data source

    // Close the popup after submission
    closePopup();
});
