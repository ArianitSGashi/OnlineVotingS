document.addEventListener('DOMContentLoaded', () => {
    const sidebar = document.getElementById('sidebar');

    // Function to toggle the sidebar
    const toggleSidebar = () => {
        sidebar.classList.toggle('d-none');
        sidebar.classList.toggle('active');
        document.body.classList.toggle('overlay');
    };

    // Card data configuration
    const cardData = [
        {
            title: 'VOTER',
            actions: [
                { label: 'Add Voter', url: '/Admin/Voter', onClick: () => navigateTo('/Admin/Voter') },
                { label: 'Edit/Delete Voter', url: '@Url.Action("Voter", "Admin")', onClick: toggleSidebar },
                { label: 'View Voter', url: '@Url.Action("Voter", "Admin")', onClick: toggleSidebar }
            ],
            icon: 'fa-user',
        },
        {
            title: 'CANDIDATE',
            actions: [
                { label: 'Add Candidate', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Edit/Delete Candidate', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'View Candidate', url: 'javascript:void(0)', onClick: toggleSidebar }
            ],
            icon: 'fa-hand-paper',
        },
        {
            title: 'ELECTION',
            actions: [
                { label: 'Generate Election', url: '/Admin/GenerateElection', onClick: () => navigateTo('/Admin/GenerateElection') },
                { label: 'Modify Election', url: '/Admin/GenerateElection', onClick: () => navigateTo('/Admin/GenerateElection') },
                { label: 'Complete Election', url: '/Admin/CompleteElection', onClick: () => navigateTo('/Admin/CompleteElection') },
            ],
            icon: 'fa-check-to-slot',
        },
        {
            title: 'VOTING',
            actions: [
                { label: 'Start Voting', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Stop Voting', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'View Results', url: 'javascript:void(0)', onClick: toggleSidebar }
            ],
            icon: 'fa-solid fa-check-to-slot',
        },
        {
            title: 'RESULT',
            actions: [
                { label: 'Show Results', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Publish Results', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Download Report', url: 'javascript:void(0)', onClick: toggleSidebar }
            ],
            icon: 'fa-chart-bar',
        },
        {
            title: 'COMPLAIN',
            actions: [
                { label: 'View Complaints', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Add Complaint', url: 'javascript:void(0)', onClick: toggleSidebar },
                { label: 'Resolve Complaint', url: 'javascript:void(0)', onClick: toggleSidebar }
            ],
            icon: 'fa-envelope',
        },
    ];

    const cardsContainer = document.getElementById('cards-container');

    // Create cards dynamically
    cardData.forEach(card => {
        const cardElement = document.createElement('div');
        cardElement.classList.add('col', 'custom-card');
        cardElement.innerHTML = `
            <div class="card text-center h-100">
                <div class="card-body">
                    <i class="fa-icon fas ${card.icon} mb-3"></i>
                    <h5 class="card-title">${card.title}</h5>
                    ${card.actions.map(action => `<a href="${action.url}" class="btn btn-primary mb-2" onclick="(${action.onClick})()">${action.label}</a>`).join('')}
                </div>
            </div>
        `;
        cardsContainer.appendChild(cardElement);
    });

    // Close sidebar if clicked outside
    document.addEventListener('click', function (event) {
        if (!sidebar.contains(event.target) && !event.target.closest('.btn-primary')) {
            sidebar.classList.add('d-none');
            sidebar.classList.remove('active');
            document.body.classList.remove('overlay');
        }
    });
});
