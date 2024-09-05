$(document).ready(function () {
    $('#addCandidateForm').on('submit', function (e) {
        e.preventDefault();

        var formData = {
            ElectionID: $('#electionId').val(),
            FullName: $('#fullName').val(),
            Party: $('#party').val(),
            Description: $('#description').val(),
            Income: $('#income').val(),
            Works: $('#works').val()
        };

        $.ajax({
            url: '/api/Candidate',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (result) {
                alert('Candidate added successfully!');
                $('#addCandidateForm')[0].reset();
            },
            error: function (xhr, status, error) {
                alert('An error occurred: ' + xhr.responseText);
            }
        });
    });
});
