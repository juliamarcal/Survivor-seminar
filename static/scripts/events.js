document.addEventListener('DOMContentLoaded', function() {
    var calendarEl = document.getElementById('calendar');
  
    var calendar = new FullCalendar.Calendar(calendarEl, {
      initialView: 'dayGridMonth',
      initialDate: '2024-09-07',
      headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
      },
      events: [
        {
          title: 'All Day Event',
          start: '2024-09-01'
        },
        {
          title: 'Long Event',
          start: '2024-09-07',
          end: '2024-09-10'
        },
        {
          groupId: '999',
          title: 'Repeating Event',
          start: '2024-09-T16:00:00'
        },
        {
          groupId: '999',
          title: 'Repeating Event',
          start: '2024-09-16T16:00:00'
        },
        {
          title: 'Conference',
          start: '2024-09-11',
          end: '2024-09-13'
        },
        {
          title: 'Meeting',
          start: '2024-09-12T10:30:00',
          end: '2024-09-12T12:30:00'
        },
        {
          title: 'Lunch',
          start: '2024-09-12T12:00:00'
        },
        {
          title: 'Meeting',
          start: '2024-09-12T14:30:00'
        },
        {
          title: 'Birthday Party',
          start: '2024-09-13T07:00:00'
        },
        {
          title: 'Click for Google',
          url: 'https://google.com/',
          start: '2024-09-28'
        }
      ]
    });
  
    calendar.render();
  });

// map
var map = L.map('map').setView([48.8566, 2.3522], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
  maxZoom: 19,
  attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

// Add a marker at a specific location in Paris
var marker = L.marker([48.8566, 2.3522]).addTo(map);


// add new event popup

document.addEventListener('DOMContentLoaded', function() {
  var popup = document.getElementById('event-popup');
  var addBtn = document.querySelector('.add-btn');
  var closeBtn = document.querySelector('.close-btn');
  var eventForm = document.getElementById('event-form');

  addBtn.addEventListener('click', function() {
      popup.style.display = 'flex';
  });

  closeBtn.addEventListener('click', function() {
      popup.style.display = 'none';
  });

  window.addEventListener('click', function(event) {
      if (event.target === popup) {
          popup.style.display = 'none';
      }
  });

  eventForm.addEventListener('submit', async function(e) {
    e.preventDefault();

    // Get the form data
    const name = document.getElementById('event-name').value;
    const date = document.getElementById('event-date').value;
    const duration = parseInt(document.getElementById('event-duration').value, 10);
    const maxParticipants = parseInt(document.getElementById('event-max-participants').value, 10);
    const address = document.getElementById('event-address').value;
    const type = document.getElementById('event-type').value;
    const locationName = document.getElementById('event-location-name').value;
    const coordinates = await getCoordinates(address);

    if (coordinates) {
      // need to add the user id to this
        const eventData = {
            name: name,
            date: date,
            duration: duration,
            max_participants: maxParticipants,
            address: address,
            latitude: coordinates.latitude,
            longitude: coordinates.longitude,
            type: type,
            location_name: locationName
        };

        // Send data to the server
        try {
            const response = await fetch('YOUR_ENDPOINT_URL_HERE', {
                method: 'POST', // or 'PUT' depending on your use case
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(eventData)
            });

            if (response.ok) {
                popup.style.display = 'none';
                window.location.reload();
            } else {
                console.error('Failed to submit event:', response.statusText);
                alert('Failed to submit event. Please try again.');
            }
        } catch (error) {
            console.error('Error submitting event:', error);
            alert('Error submitting event. Please try again.');
        }
    } else {
        alert('Unable to get coordinates for the given address.');
    }
  });
});


async function getCoordinates(address) {
  const encodedAddress = encodeURIComponent(address);
  const url = `https://nominatim.openstreetmap.org/search?q=${encodedAddress}&format=json&addressdetails=1`;

  try {
      const response = await fetch(url);
      const data = await response.json();

      if (data.length > 0) {
          const { lat, lon } = data[0];
          return {
              latitude: parseFloat(lat),
              longitude: parseFloat(lon)
          };
      } else {
          throw new Error('Address not found');
      }
  } catch (error) {
      console.error('Error fetching coordinates:', error);
      return null;
  }
}