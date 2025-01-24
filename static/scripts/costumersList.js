// menu togle for responsivity
const menuToggle = document.querySelector('.menu-toggle');
const navMenu = document.querySelector('header');

menuToggle.addEventListener('click', () => {
  navMenu.classList.toggle('nav-open');
});

// Dropdown
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

function selectItem(element) {
    var button = document.getElementById("dropdownButton");
    button.textContent = element.textContent + " "; 

    var icon = document.createElement("i");
    icon.className = "fa-solid fa-caret-down";

    button.appendChild(icon);
    
    toggleDropdown();
}
  
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

// searchbar
document.getElementById('search-icon').addEventListener('click', function() {
    var searchInput = document.getElementById('search-input');
    searchInput.classList.toggle('show');
    searchInput.focus();
});

// compare astrological sighns 
function updateCompareButton() {
    const checkboxes = document.querySelectorAll('.client-checkbox');

    const checkedCount = Array.from(checkboxes).filter(checkbox => checkbox.checked).length;

    const compareButton = document.getElementById('compare-astrology-btn');

    if (checkedCount === 2) {
        compareButton.disabled = false;
    } else {
        compareButton.disabled = true;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const checkboxesCheckInterval = setInterval(() => {
        const checkboxes = document.querySelectorAll('.client-checkbox');
        if (checkboxes.length) {
            checkboxes.forEach(checkbox => {
                checkbox.addEventListener('change', updateCompareButton);
            });
            clearInterval(checkboxesCheckInterval);
        }
    }, 100);
    updateCompareButton();
});

// popup for comparing astrology
function openPopup() {
    document.getElementById('popupModal').style.display = 'block';
}

function closePopup() {
    document.getElementById('popupModal').style.display = 'none';
}

document.getElementById('compare-astrology-btn').addEventListener('click', function() {
    console.log("Clicked the button");
    const selectedCustomerIds = [];
    const checkboxes = document.querySelectorAll('.client-checkbox:checked');

    checkboxes.forEach(checkbox => {
        const customerRow = checkbox.closest('tr');
        const customerId = customerRow.getAttribute('data-customer-id');
        if (customerId) {
            selectedCustomerIds.push(customerId);
        }
    });

    if (selectedCustomerIds.length !== 2) {
        alert('Please select exactly two customers to compare.');
        return;
    }

    // Send selected customer IDs to backend
    fetch('/check_compatibility', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            customer1_id: selectedCustomerIds[0],
            customer2_id: selectedCustomerIds[1]
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.error) {
                alert(data.error);
            } else {
                document.getElementById('popupModal').querySelector('h2').textContent = `Astrological Comparison`;
                document.getElementById('popupModal').querySelector('p').textContent = `${data.customer1_name} (${data.sign1}) + ${data.customer2_name} (${data.sign2}) = ${data.compatibility}`;

                document.getElementById('popupModal').style.display = 'block';
            }
        })
        .catch(error => console.error('Error:', error));
});

function closePopup() {
    document.getElementById('popupModal').style.display = 'none';
}

window.onclick = function(event) {
    if (event.target === document.getElementById('popupModal')) {
        closePopup();
    }
}

function goToClientPage(event, row) {
    if (event.target.classList.contains('client-checkbox')) {
        event.stopPropagation();
        return;
    }

    let customerId = row.attributes['data-customer-id'].value;
    console.log(customerId);
    // Redirect to the new page with the customer ID as a query parameter
    window.location.href = `client?id=${customerId}`;
}

// popup for new costumer
const clientPopup = document.getElementById('add-client-popup');

document.getElementById('add-btn').addEventListener('click', function() {
    clientPopup.style.display = 'block';
});

function closeClientPopup() {
    clientPopup.style.display = 'none';
}

document.getElementById('add-client-form').addEventListener('submit', function(event) {
    event.preventDefault();

    // Get form values
    const birthDate = document.getElementById('client-birth-date').value;
    const newClient = {
        email: document.getElementById('client-email').value,
        name: document.getElementById('client-name').value,
        surname: document.getElementById('client-surname').value,
        birth_date: birthDate,
        gender: document.getElementById('client-gender').value,
        phone: document.getElementById('client-phone').value,
        description: document.getElementById('client-description').value,
        astrological_sign: getAstrologicalSign(birthDate),
        address: document.getElementById('client-address').value
    };

    // TODO: Handle adding the new client to your data source
    console.log('New Client:', newClient);

    window.location.reload();
});


// other functions
function getAstrologicalSign(dateString) { // gets the astrological sign given the bith date
    const date = new Date(dateString);
    const month = date.getMonth() + 1;
    const day = date.getDate();

    const zodiacSigns = [
        { sign: 'Capricorn', endDate: { month: 1, day: 19 } },
        { sign: 'Aquarius', endDate: { month: 2, day: 18 } },
        { sign: 'Pisces', endDate: { month: 3, day: 20 } },
        { sign: 'Aries', endDate: { month: 4, day: 19 } },
        { sign: 'Taurus', endDate: { month: 5, day: 20 } },
        { sign: 'Gemini', endDate: { month: 6, day: 20 } },
        { sign: 'Cancer', endDate: { month: 7, day: 22 } },
        { sign: 'Leo', endDate: { month: 8, day: 22 } },
        { sign: 'Virgo', endDate: { month: 9, day: 22 } },
        { sign: 'Libra', endDate: { month: 10, day: 22 } },
        { sign: 'Scorpio', endDate: { month: 11, day: 21 } },
        { sign: 'Sagittarius', endDate: { month: 12, day: 21 } },
        { sign: 'Capricorn', endDate: { month: 12, day: 31 } }
    ];

    for (let i = 0; i < zodiacSigns.length; i++) {
        const { sign, endDate } = zodiacSigns[i];
        if (month === endDate.month && day <= endDate.day) {
            return sign;
        } else if (month === endDate.month - 1 || (month === endDate.month && day > endDate.day)) {
            return sign;
        }
    }

    return null;
}