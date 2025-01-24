function getPostIdFromUrl() {
    const urlParams = new URLSearchParams(window.location.search);
    const postId = urlParams.get('id'); // Gets the 'id' from the URL
    return postId;
}

// Example: Log the post ID to the console
const postId = getPostIdFromUrl();
console.log("Post ID:", postId);