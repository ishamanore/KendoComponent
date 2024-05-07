function triptypeValidation() {
    var input = $("input[name='c_triptype']:checked").val();
    if (input === undefined) {
        document.getElementById("errorgender").style.color = "red";
        document.getElementById("errorgender").innerHTML = "Please select student gender.";
        return false;
    }
    else {
        document.getElementById("errorgender").style.color = "red";
        document.getElementById("errorgender").innerHTML = "";
        return true;
    }
}