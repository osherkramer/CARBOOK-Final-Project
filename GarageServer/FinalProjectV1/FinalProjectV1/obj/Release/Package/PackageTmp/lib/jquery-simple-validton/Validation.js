$(document).ready(function () {

    $('#myform').validate({ // initialize the plugin
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 5
            },
            ConfirmPassword: {
                required: true,
                minlength: 5
            }
        },
        submitHandler: function (form) { // for demo
            var pass1 = document.getElementById("pass1").value;
            var pass2 = document.getElementById("pass2").value;
            if (pass1 != pass2) {
                //alert("Passwords Do not match");
                document.getElementById("pass1").style.borderColor = "#E34234";
                document.getElementById("pass2").style.borderColor = "#E34234";
            }
            else {
                alert("Passwords Match!!!");
            }
        }
    });

});