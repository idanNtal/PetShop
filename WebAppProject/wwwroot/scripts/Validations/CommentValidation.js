document.addEventListener('DOMContentLoaded', function () {
    var commentInput = document.getElementById('comment');

    commentInput.addEventListener('input', function (event) {
        var inputValue = event.target.value;
        var newValue = inputValue.replace(/\s{2,}/g, ' ');

        if (newValue !== inputValue) {
            event.target.value = newValue;
        }
    });
});

$(function () {
    // Add custom validation method for comment field
    $.validator.addMethod(
        'customCommentValidation',
        function (value, element) {
            // Modify the validation logic as per your requirements
            return value.trim().length >= 3; // Comment should have at least 10 characters
        },
        'Comment must be at least 3 characters long.'
    );

    // Configure the validation rules and messages
    $('.addComment').validate({
        rules: {
            comment: {
                required: true,
                maxlength: 230,
                minlength: 3,
                customCommentValidation: true,
            },
        },
        messages: {
            comment: {
                required: 'Enter a Comment',
                maxlength: 'Comment can\'t be over 230 characters',
            },
        },
    });
});