
function fetchGraph(period) {

    document.querySelectorAll('.period-selector button').forEach(btn => {
        btn.classList.remove('selected');
    });
    document.getElementById(`btn-${period}`).classList.add('selected');

    fetch(`http://10.68.244.110:5000/api/get-customers-graph?period=${period}`)
        .then(response => response.blob())
        .then(imageBlob => {
            const imageUrl = URL.createObjectURL(imageBlob);
            document.getElementById('graph-img').src = imageUrl;
        })
        .catch(error => {
            console.error('Error fetching graph:', error);
        });
}