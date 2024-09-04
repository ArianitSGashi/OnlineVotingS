$(document).ready(function () {
    $('#editCandidateForm').on('submit', function (e) {
        e.preventDefault();

        var candidateData = {
            CandidateID: $('#candidateId').val(),
            ElectionID: $('#electionId').val(),
            FullName: $('#fullName').val(),
            Party: $('#party').val(),
            Description: $('#description').val(),
            Income: $('#income').val(),
            Works: $('#works').val()
        };

        $.ajax({
            url: '/api/Candidate',  // Ensure this matches your API route for updating candidates
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(candidateData),
            success: function (result) {
                alert('Candidate updated successfully!');
                $('#editCandidateForm')[0].reset();
            },
            error: function (xhr, status, error) {
                alert('An error occurred: ' + xhr.responseText);
            }
        });
    });
});