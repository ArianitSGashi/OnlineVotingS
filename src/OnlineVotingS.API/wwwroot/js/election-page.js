// election-page.js

$(document).ready(function () {
    // Navbar scroll effect
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('.navbar').addClass('navbar-scrolled');
        } else {
            $('.navbar').removeClass('navbar-scrolled');
        }
    });

    // Search functionality
    $("#searchButton").on("click", function () {
        performSearch();
    });

    $("#electionSearch").on("keyup", function (event) {
        if (event.keyCode === 13) {
            performSearch();
        }
    });

    function performSearch() {
        var value = $("#electionSearch").val().toLowerCase();
        $("#electionTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    }

    // Modal handling
    var voteMessage = '@ViewBag.VoteMessage';
    var voteMessageType = '@ViewBag.VoteMessageType';
    if (voteMessage) {
        $('#voteConfirmationMessage').text(voteMessage);
        if (voteMessageType === 'success') {
            $('#modalHeader').removeClass('bg-danger').addClass('bg-success');
            $('#voteConfirmationModalLabel').text('Success');
        } else {
            $('#modalHeader').removeClass('bg-success').addClass('bg-danger');
            $('#voteConfirmationModalLabel').text('Alert');
        }
        var voteConfirmationModal = new bootstrap.Modal(document.getElementById('voteConfirmationModal'));
        voteConfirmationModal.show();
    }

    // Tooltip initialization
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
});