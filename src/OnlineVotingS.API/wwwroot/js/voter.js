document.addEventListener('DOMContentLoaded', () => {
    const sidebar = document.getElementById('sidebar');
    const toggleSidebar = () => {
        sidebar.classList.toggle('d-none');
        sidebar.classList.toggle('active');
        document.body.classList.toggle('overlay');
    };
    toggleSidebar();
});
