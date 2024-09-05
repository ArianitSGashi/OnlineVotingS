$(document).ready(function () {
    $('#searchCandidate').on('keyup', function () {
        var value = $(this).val().toLowerCase();
        $('#candidateTableBody tr').filter(function () {
            $(this).toggle(
                $(this).text().toLowerCase().indexOf(value) > -1
            );
        });
    });
});