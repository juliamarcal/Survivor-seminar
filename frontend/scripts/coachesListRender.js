// const coachesData = {
//     "coaches": [
//         {
//             "id": 1,
//             "name": "Alfreds Futterkiste",
//             "email": "alfreds@example.com",
//             "phone": "123-456-7890",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "number_of_customers": 10
//         },
//         {
//             "id": 2,
//             "name": "Centro comercial Moctezuma",
//             "email": "centro@example.com",
//             "phone": "234-567-8901",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "number_of_customers": 15
//         },
//         {
//             "id": 3,
//             "name": "Ernst Handel",
//             "email": "ernst@example.com",
//             "phone": "345-678-9012",
//             "profile_picture": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtuphMb4mq-EcVWhMVT8FCkv5dqZGgvn_QiA&s",
//             "number_of_customers": 8
//         }
//     ]
// };
//
// function renderCoachTemplate(data) {
//     const source = document.getElementById('coach-template').innerHTML;
//     const template = Handlebars.compile(source);
//     const html = template(data);
//     document.getElementById('coach-list').innerHTML = html;
// }
//
// document.addEventListener('DOMContentLoaded', function() {
//     renderCoachTemplate(coachesData);
// });


document.addEventListener('DOMContentLoaded', () => {
    fetch('http://10.68.244.110:5000/api/coaches')
        .then(response => response.json())
        .then(data => {
            const source = document.getElementById('coach-template').innerHTML;
            const template = Handlebars.compile(source);
            const context = { coaches: data.coaches };
            document.getElementById('coach-list').innerHTML = template(context);
        })
        .catch(error => console.error('Error fetching data:', error));
});
