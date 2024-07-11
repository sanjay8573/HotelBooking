function validateField(field) {
    var isValid = true;
    var value = $(field).val().trim();
   var filedAttr = $(field).attr('valType');
   
    //console.log(filedAttr);
   
    // Check if the field is empty
    if (value === "") {
        $(field).addClass("errRedBorder");
        isValid = false;
    } else {
        $(field).removeClass("errRedBorder");
    }
   
    if (filedAttr === "NumbersOnly" && !checkNumberOnly(value)) {
        $(field).addClass("errRedBorder");
        isValid = false;
    }
    // Specific validation for email
    if (filedAttr === "Email" && !checkEmail(value)) {
        $(field).addClass("errRedBorder");
        isValid = false;
    }

    // Specific validation for mobile number
    if (filedAttr === "Phone" && !checkMobile(value)) {
        $(field).addClass("errRedBorder");
        isValid = false;
    }
    if (filedAttr === "DropDown") {
        if ($(field).val() == 0) {
            $(field).addClass("errRedBorder");
            isValid = false;
        }

    }
    return isValid;
}

function checkEmail(email) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    return emailPattern.test(email);
}
function checkMobile(mobile) {
    var mobilePattern = /^\d{10,15}$/;
    return mobilePattern.test(mobile);
}

function checkNumberOnly(elt) {
    var numberPattern = /[^0-9]/g;
    return !numberPattern.test(elt);
}
function checkAmountWith2Decimal(elt) {
    var amtPattern = /^[1-9]\d*(\.\d+)?$/;
    return amtPattern.test(elt);
}
function validateBooking() {
    var isValid = true;

    $(".ValidateInput").each(function () {
        if (!validateField(this)) {
            isValid = false;
        }
    });

    
   
    return isValid;
}
function validateMyForm(eltCls) {
    var isValid = true;

    $("." + eltCls).each(function () {
        if (!validateField(this)) {
            isValid = false;
        }
    });
    return isValid;
}