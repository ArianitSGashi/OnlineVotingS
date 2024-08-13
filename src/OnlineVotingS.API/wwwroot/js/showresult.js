document.addEventListener('DOMContentLoaded', () => {
    const sidebar = document.getElementById('sidebar');

    // Function to toggle the sidebar
    const toggleSidebar = () => {
        sidebar.classList.toggle('d-none');
        sidebar.classList.toggle('active');
        document.body.classList.toggle('overlay');
    };

    // Show sidebar initially
    toggleSidebar();

    // Close sidebar if clicked outside

});
