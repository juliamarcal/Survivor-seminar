// const customersData = {
//     "customers": [
//         {
//             "id": 1,
//             "name": "Alfreds Futterkiste",
//             "email": "alfreds@example.com",
//             "phone": "123-456-7890",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "payment_method_img": "https://www.ecranmobile.fr/photo/art/grande/73986582-51458295.jpg?v=1689052016"
//         },
//         {
//             "id": 2,
//             "name": "Centro comercial Moctezuma",
//             "email": "centro@example.com",
//             "phone": "234-567-8901",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "payment_method_img": "https://www.ecranmobile.fr/photo/art/grande/73986582-51458295.jpg?v=1689052016"
//         },
//         {
//             "id": 3,
//             "name": "Ernst Handel",
//             "email": "ernst@example.com",
//             "phone": "345-678-9012",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "payment_method_img": "https://www.ecranmobile.fr/photo/art/grande/73986582-51458295.jpg?v=1689052016"
//         }
//     ]
// };
//
//
//
// function renderTemplate(data) {
//     const source = document.getElementById('customer-template').innerHTML;
//     const template = Handlebars.compile(source);
//     const html = template(data);
//     document.getElementById('customer-list').innerHTML = html;
// }
//
// document.addEventListener('DOMContentLoaded', function() {
//     renderTemplate(customersData);
// });


document.addEventListener('DOMContentLoaded', () => {
    fetch('/api/customers')
        .then(response => response.json())
        .then(data => {
            const source = document.getElementById('customer-template').innerHTML;
            const template = Handlebars.compile(source);
            const context = { customers: data.customers };
            document.getElementById('customer-list').innerHTML = template(context);
        })
        .catch(error => console.error('Error fetching data:', error));
});
