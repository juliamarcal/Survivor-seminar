function redirectToPost(postId) {
    window.location.href = `blogPost.html?id=${postId}`;
}

// pop up
const writeBtn = document.getElementById("write-btn");
const popupForm = document.getElementById("popup-form");
const closeBtn = document.querySelector(".close-btn");
const blogForm = document.getElementById("blog-form");

writeBtn.addEventListener("click", function() { // Show the pop-up when 'Write +' is clicked
    popupForm.style.display = "flex";
});

closeBtn.addEventListener("click", function() { // Hide pop-up when 'X' is clicked
    popupForm.style.display = "none";
});

window.addEventListener("click", function(event) { // Hide pop-up if clicked outside the form
    if (event.target == popupForm) {
        popupForm.style.display = "none";
    }
});

blogForm.addEventListener("submit", async function(event) { 
    event.preventDefault();

    const title = document.getElementById("title").value;
    const content = document.getElementById("content").value;
    const image = document.getElementById("image").value || 'https://via.placeholder.com/300x200';

    const blogPost = {
        title: title,
        content: content,
        image: image
    };

    try {
        // Send a POST request to your API endpoint
        const response = await fetch('https://your-api-endpoint.com/posts', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(blogPost)
        });

        if (!response.ok) {
            throw new Error('Failed to submit the blog post.');
        }

        // If the post was successful, reload the page
        window.location.reload();
    } catch (error) {
        console.error(error);
        alert('There was an error submitting your blog post. Please try again.');
    }
});