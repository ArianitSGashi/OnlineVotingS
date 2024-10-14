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
                { label: 'Add Voter', url: '/AdminVoter/AddVoter', onClick: () => navigateTo('/AdminVoter/AddVoter') },
                { label: 'Edit Voter', url: '/AdminVoter/EditVoter', onClick: () => navigateTo('/AdminVoter/EditVoter') },
                { label: 'Delete Voter', url: '/AdminVoter/DeleteVoter', onClick: () => navigateTo('/AdminVoter/DeleteVoter') },
                { label: 'View Voters', url: '/AdminVoter/ViewVoters', onClick: () => navigateTo('/AdminVoter/ViewVoters') }
            ],
            icon: 'fa-user',
        },
        {
            title: 'CANDIDATE',
            actions: [
                { label: 'Add Candidate', url: '/Candidate/AddCandidate', onClick: () => navigateTo('/Candidate/AddCandidate') },
                { label: 'Edit Candidate', url: '/Candidate/EditCandidate', onClick: () => navigateTo('/Candidate/EditCandidate') },
                { label: 'Delete Candidate', url: '/Candidate/DeleteCandidate', onClick: () => navigateTo('/Candidate/DeleteCandidate') },
                { label: 'View Candidates', url: '/Candidate/ViewCandidates', onClick: () => navigateTo('/Candidate/ViewCandidates') }
            ],
            icon: 'fa-hand-paper',
        },
        {
            title: 'ELECTION',
            actions: [
                { label: 'Generate Election', url: '/Election/GenerateElection', onClick: () => navigateTo('/Election/GenerateElection') },
                { label: 'Modify Election', url: '/Election/ModifyElection', onClick: () => navigateTo('/Election/ModifyElection') },
                { label: 'Complete Election', url: '/Election/CompleteElection', onClick: () => navigateTo('/Election/CompleteElection') },
                { label: 'Delete Election', url: '/Election/DeleteElection', onClick: () => navigateTo('/Election/DeleteElection') },
                { label: 'View Election', url: '/Election/ViewElection', onClick: () => navigateTo('/Election/ViewElection') },
            ],
            icon: 'fa-check-to-slot',
        },
        {
            title: 'RESULT',
            actions: [
                { label: 'Generate Results', url: '/Result/GenerateResult', onClick: () => navigateTo('/Result/GenerateResult') },
                { label: 'View Results', url: '/Result/ViewResult', onClick: () => navigateTo('/Result/ViewResult') },
            ],
            icon: 'fa-chart-bar',
        },
        {
            title: 'COMPLAIN',
            actions: [
                { label: 'Reply Complaint', url: '/Complain/ReplyComplain', onClick: () => navigateTo('/Complain/ReplyComplain') },
                { label: 'View Complaints', url: '/Complain/ViewComplain', onClick: () => navigateTo('/Complain/ViewComplain') },
                { label: 'View Feedbacks', url: '/Feedback/ViewFeedbacks', onClick: () => navigateTo('/Feedback/ViewFeedbacks') },
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
