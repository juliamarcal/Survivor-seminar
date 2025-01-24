const menuToggle = document.querySelector('.menu-toggle');
const navMenu = document.querySelector('header');

menuToggle.addEventListener('click', () => {
  navMenu.classList.toggle('nav-open');
});

document.addEventListener("DOMContentLoaded", function() {
    const customerData = {
        "name": "Francis Mitcham",
        "profilePicture": "profile-picture.jpg",
        "totalEncounters": 23,
        "positives": 20,
        "inProgress": 3,
        "userId": "UD003054",
        "email": "francis.mitcham@tmail.com",
        "address": "551 Swanston Street, Melbourne Victoria 3053 Australia",
        "lastActivity": "15 Feb, 2024",
        "coach": "Nicolas Latourne",
        "recentMeetings": [
            {"date": "23 Jul, 2024", "rating": 4, "report": "A very good moment !", "source": "Dating App"},
            {"date": "21 Jul, 2024", "rating": 3, "report": "She was a very good person but not my type.", "source": "Friends"},
            {"date": "19 Jun, 2024", "rating": 1, "report": "The meeting was not good, she was not interested.", "source": "Dating App"},
            {"date": "02 Jun, 2024", "rating": 2, "report": "Not bad, but not good.", "source": "Dating App"},
            {"date": "12 May, 2024", "rating": 3, "report": "Need to see her again, she was interesting.", "source": "Social Network"}
        ],
        "paymentsHistory": [
            {"date": "20 Jul, 2024", "paymentMethod": "Visa", "paymentMethodIcon": "../static/visa-icon.png", "amount": "-$49.00", "comment": "Monthly Subscription"},
            {"date": "20 Jun, 2024", "paymentMethod": "Visa", "paymentMethodIcon": "../static/visa-icon.png", "amount": "-$49.00", "comment": "Monthly Subscription"},
            {"date": "20 May, 2024", "paymentMethod": "Visa", "paymentMethodIcon": "../static/visa-icon.png", "amount": "-$49.00", "comment": "Monthly Subscription"},
            {"date": "20 Apr, 2024", "paymentMethod": "Visa", "paymentMethodIcon": "../static/visa-icon.png", "amount": "-$49.00", "comment": "Monthly Subscription"}
        ]
    };

    Handlebars.registerHelper('renderStars', function(rating) {
        let stars = '';
        for (let i = 0; i < 5; i++) {
            if (i < rating) {
                stars += '★';
            } else {
                stars += '☆';
            }
        }
        return new Handlebars.SafeString(stars);
    });

    const templateSource = document.getElementById("customer-template").innerHTML;
    const template = Handlebars.compile(templateSource);
    const html = template(customerData);
    document.getElementById("customer-details-container").innerHTML = html;
});