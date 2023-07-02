$(document).ready(function () {
    $("form").validate({
        rules: {
            Username: {
                required: true,
                minlength: 4,
                maxlength: 20

            },
            Password: {
                required: true,
                minlength: 8,
                pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Username: {
                required: "Please enter a username",
                minlength: "UserName must be at least 4 characters long",
                maxlength: "UserName can't be over least 20 characters long",
            },
            Password: {
                required: "Please enter a password",
                minlength: "Password must be at least 8 characters long",
                pattern: "Password is too weak",
            },
            ConfirmPassword: {
                required: "Please confirm your password",
                equalTo: "Passwords do not match"
            },
            Email: {
                required: "Please enter an email",
                email: "Please enter a valid email address"
            }
        },
        errorPlacement: function (error, element) {
            error.addClass("text-danger");
            error.insertAfter(element);
        },
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        }
    });
});