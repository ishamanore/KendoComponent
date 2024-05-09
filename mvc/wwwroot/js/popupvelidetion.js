function TypeValidation() {
    var input = $("input[name='c_triptype']:checked").val();
    if (input === undefined) {
        $("#errortriptype").html("<span style='color: red; font-size: small;'>Please select trip type.</span>");
        return false;
    } else {
        $("#errortriptype").html("");
        return true;
    }
}

function NameValidation() {
    var input = $("#c_tripid").val();
    if (!input) {
        $("#errorname").html("<span style='color: red; font-size: small;'>Please select trip name.</span>");
        return false;
    } else {
        $("#errorname").html("");
        return true;
    }
}

function DateValidation() {
    var input = $("#c_date").val();
    if (!input) {
        $("#errordate").html("<span style='color: red; font-size: small;'>Please select trip date.</span>");
        return false;
    } else {
        $("#errordate").html("");
        return true;
    }
}

function TimeValidation() {
    var input = $("#c_time").val();
    if (!input) {
        $("#errortime").html("<span style='color: red; font-size: small;'>Please select trip time.</span>");
        return false;
    } else {
        $("#errortime").html("");
        return true;
    }
}

function DaysValidation() {
    var input = $("#c_days").val();
    if (!input) {
        $("#errordays").html("<span style='color: red; font-size: small;'>Please select trip days.</span>");
        return false;
    } else {
        $("#errordays").html("");
        return true;
    }
}

function PhotoValidation() {
    var input = $("#c_image").val();
    if (!input) {
        $("#errorphoto").html("<span style='color: red; font-size: small;'>Please select photo.</span>");
        return false;
    } else {
        $("#errorphoto").html("");
        return true;
    }
}

function PriceValidation() {
    var input = $("#c_price").val();
    if (!input) {
        $("#errorprice").html("<span style='color: red; font-size: small;'>Please enter trip price.</span>");
        return false;
    } else {
        $("#errorprice").html("");
        return true;
    }
}

function availableseatValidation() {
    var input = $("#c_availableseat").val();
    if (!input) {
        $("#erroravailableseat").html("<span style='color: red; font-size: small;'>Please enter available seat.</span>");
        return false;
    } else {
        $("#erroravailableseat").html("");
        return true;
    }
}

function initialseatValidation() {
    var input = $("#c_initialseat").val();
    if (!input) {
        $("#errorinitialseat").html("<span style='color: red; font-size: small;'>Please enter initial seat.</span>");
        return false;
    } else {
        $("#errorinitialseat").html("");
        return true;
    }
}

function validation() {
    var name = NameValidation();
    var type = TypeValidation();
    var date = DateValidation();
    var time = TimeValidation();
    var days = DaysValidation();
    var photo = PhotoValidation();
    var price = PriceValidation();
    var available = availableseatValidation();
    var initial = initialseatValidation();
    return name && type && date && time && days && photo && price && available && initial;
}
