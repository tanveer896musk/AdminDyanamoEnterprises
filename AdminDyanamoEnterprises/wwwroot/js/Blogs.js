document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".read-more").forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            alert("You clicked Read More for blog ID: " + this.dataset.id);
            // Add dynamic content loading here if needed
        });
    });
});
