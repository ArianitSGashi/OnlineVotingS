$(document).ready(function () {
    $('#deleteCandidateForm').on('submit', function (e) {
        e.preventDefault();

        var candidateId = $('#candidateId').val();

        $.ajax({
            url: '/api/Candidate/' + candidateId,
            type: 'DELETE',
            success: function (result) {
                alert('Candidate deleted successfully!');
                $('#deleteCandidateForm')[0].reset();
            },
            error: function (xhr, status, error) {
                alert('An error occurred: ' + xhr.responseText);
            }
        });
    });
});