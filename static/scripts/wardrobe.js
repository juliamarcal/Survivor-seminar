const menuToggle = document.querySelector('.menu-toggle');
const navMenu = document.querySelector('header');

menuToggle.addEventListener('click', () => {
  navMenu.classList.toggle('nav-open');
});

function openPopup(type) {
    document.getElementById("image-popup").style.display = "block";
    loadImages(type);
}
  
function closePopup() {
    document.getElementById(`image-popup`).style.display = "none";
}

function loadImages(type) {
    const imageFolder = '../clothes/';
    let images;
    switch (type) {
        case "cap":
            images = ['3.jpg', '4.jpg'];
            break;
        case "top":
            images = ['1.jpg', '2.jpg', '5.jpg', '6.jpg', '7.jpg', '8.jpg'];
            break;
        case "bottom":
            images = ['9.jpg', '10.jpg', '11.jpg', '12.jpg'];
            break;
        case "shoes":
            images = ['13.jpg', '14.jpg', '15.jpg'];
            break;
        default:
            images = ['1.jpg', '2.jpg', '5.jpg', '6.jpg', '7.jpg', '8.jpg', '9.jpg', '10.jpg', '11.jpg', '12.jpg', '13.jpg', '14.jpg', '15.jpg'];
            break;
    }
    
    const gallery = document.getElementById("image-gallery");
    
    gallery.innerHTML = "";
  
    images.forEach(imageName => {
        const img = document.createElement("img");
        img.src = imageFolder + imageName;
        img.alt = imageName;
        img.onclick = () => selectImage(imageFolder + imageName, type); // Set click event to select image
        gallery.appendChild(img);
    });
}
  
function selectImage(imageSrc, type) {
    const selectedImage = document.getElementById(`selected-image-${type}`);
    selectedImage.src = imageSrc;
    closePopup();
}

function clearAll() {
    document.getElementById(`selected-image-cap`).src = "";
    document.getElementById(`selected-image-top`).src = "";
    document.getElementById(`selected-image-bottom`).src = "";
    document.getElementById(`selected-image-shoes`).src = "";
}