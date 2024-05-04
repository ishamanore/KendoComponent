function username() {
  var username = document.getElementById("c_username").value;

  if (username.length >= 3) {
    document.getElementById("username").innerText = "";
    return true;
  } else if (username == "" && username == null) {
    document.getElementById("username").innerText = "Please eneter username";
    return false;
  } else {
    document.getElementById("username").innerText =
      "username should more than 3 character";
    return false;
  }
}

function email() {
  var emailid = document.getElementById("c_email").value;
  var emialregular = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

  if (emailid.trim() === "") {
    document.getElementById("emailid").innerText = "Please enter Email";
    return false;
  } else if (!emialregular.test(emailid)) {
    document.getElementById("emailid").innerText = "Enter valid Email";
    return false;
  } else {
    document.getElementById("emailid").innerText = "";
    return true;
  }
}

function password() {
  var password = document.getElementById("c_password").value;
  var passwordregular = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{4,}$/;

  if (password.trim() === "") {
    document.getElementById("password").innerText = "Please enter password";
    return false;
  } else if (!(passwordregular).test(password)) {
    document.getElementById("password").innerText =
      "password must include uppercase, lowercase, special character, number(atleast 4 character)";
      return false;
  } else {
    document.getElementById("password").innerText = "";
    return true;
  }
}
