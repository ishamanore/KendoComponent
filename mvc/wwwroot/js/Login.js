function email() {
  var email = document.getElementById("c_email").value;
  var emailregular = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

  if (email.trim() === "") {
    document.getElementById("email").innerText = "Please enter email-id";
    return false;
  } else if (!emailregular.test(email)) {
    document.getElementById("email").innerText = "Enter valid email-id";
    return false;
  } else {
    document.getElementById("email").innerText = "";
    return true;
  }
}

function password() {
  var password = document.getElementById("c_password").value;
  var passwordregular =
    /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{4,}$/;

  if (password.trim() === "") {
    document.getElementById("password").innerText = "Please enter password";
    return false;
  } else if (!passwordregular.test(password)) {
    document.getElementById("password").innerText =
      "password must include uppercase, lowercase, special character, number(atleast 4 character)";
    return false;
  } else {
    document.getElementById("password").innerText = "";
  }
}

function checkbox() {
  var checkbox = document.getElementById("c_terms");
  if (checkbox.checked) {
    document.getElementById("checkbox").innerText = "";
  } else {
    document.getElementById("checkbox").innerText = "Please check the checkbox";
  }
}
