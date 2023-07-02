document.addEventListener('DOMContentLoaded', function () {
    var commentInput = document.getElementById('description');

    commentInput.addEventListener('input', function (event) {
        var inputValue = event.target.value;
        var newValue = inputValue.replace(/\s{2,}/g, ' ');

        if (newValue !== inputValue) {
            event.target.value = newValue;
        }
    });
});

//$(document).ready(function () {
//    var mode = $('#animalId').val() ? 'Edit' : 'Add';

//    // Initialize jQuery Validation plugin
//    $("form").validate({
//        // Specify validation rules for each field
//        rules: {
//            name: {
//                required: true,
//                minlength: 2,
//                maxlength: 20,
//                pattern: /^[A-Za-z\-]+$/,
//            },
//            age: {
//                required: true,
//                min: 1,
//                max: 20,
//                digits: true
//            },
//            picture: {
//                required: mode === 'Add'
//            },
//            description: {
//                required: true,
//                minlength: 10,
//                maxlength: 255,
//            },
//            category: {
//                required: true
//            }
//        },
//        // Specify validation error messages
//        messages: {
//            name: {
//                required: "Please enter a name",
//                minlength: "Name must be at least 2 characters long",
//                maxlength: "Name cannot exceed 20 characters",
//                pattern: "Invalid formt",
//            },
//            age: {
//                required: "Please enter an age",
//                min: "Age must be at least 0",
//                max: "Age cannot exceed 20"
//            },
//            picture: {
//                required: "Please select a picture"
//            },
//            description: {
//                required: "Please enter a description",
//                minlength: "Description must be at least 10 characters long",
//                maxlength: "Description cannot exceed 255 characters"
//            },
//            category: {
//                required: "Please select a category"
//            }
//        },
//        // Handle error placement and highlighting
//        errorPlacement: function (error, element) {
//            error.addClass("text-danger");
//            error.insertAfter(element);
//        },
//        highlight: function (element) {
//            $(element).addClass("is-invalid");
//        },
//        unhighlight: function (element) {
//            $(element).removeClass("is-invalid");
//        }
//    });
//});