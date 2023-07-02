$(document).ready(function () {
    $('#categorySelect').change(function () {
        var selectedCategory = $(this).val();

        if (selectedCategory === 'All') {
            $('.styled-table tbody tr').show();
        } else {
            $('.styled-table tbody tr').hide();
            $('.styled-table tbody .category-' + selectedCategory).show();
        }
    });
});