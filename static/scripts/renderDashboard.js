
function fetchGraph1(period) {

    document.querySelectorAll('.period-selector button').forEach(btn => {
        btn.classList.remove('selected');
    });
    document.getElementById(`btn-${period}-1metric`).classList.add('selected');

    fetch(`/api/get-customers-graph?period=${period}`)
        .then(response => response.blob())
        .then(imageBlob => {
            const imageUrl = URL.createObjectURL(imageBlob);
            document.getElementById('graph-img-1metric').src = imageUrl;
        })
        .catch(error => {
            console.error('Error fetching graph:', error);
        });
}

function fetchCustomerStats() {
    fetch('/api/get-customer-stats')
        .then(response => response.json())
        .then(data => {
            document.getElementById('total-customers-1metric').textContent = data.total_customers;
            document.getElementById('customers-by-coach-1metric').textContent = data.avg_customers_per_coach.toFixed(2);  // Limit to 2 decimal places if necessary
        })
        .catch(error => {
            console.error('Error fetching customer stats:', error);
        });
}

function fetchGraph2() {
    fetch('/api/get-events-graph')
        .then(response => response.blob())
        .then(imageBlob => {
            const imageUrl = URL.createObjectURL(imageBlob);
            document.getElementById('graph-img-2metric').src = imageUrl;
        })
        .catch(error => {
            console.error('Error fetching second graph:', error);
        });
}

function loadMetrics() {
    fetch('/api/get-events-metrics')
        .then(response => response.json())
        .then(metrics => {
            document.getElementById('monthly-2metric').textContent = metrics.monthly_average.toFixed(2);
            document.getElementById('weekly-2metric').textContent = metrics.weekly_average.toFixed(2);
            document.getElementById('daily-2metric').textContent = metrics.daily_average.toFixed(2);
        })
        .catch(error => {
            console.error('Error fetching metrics:', error);
        });
}


document.addEventListener('DOMContentLoaded', function() {
    fetchGraph2();
});

document.addEventListener('DOMContentLoaded', function() {
    fetchCustomerStats();
});

document.addEventListener('DOMContentLoaded', function() {
    fetchGraph1('1M');
});

document.addEventListener('DOMContentLoaded', function() {
    loadMetrics();
});