function ValidateForm() {
    let forma = document.querySelector("#f1");
    let isValid = true;

    let name = forma["Book.Name"].value;
    let nameSpan = document.querySelector("#nameSpan");

    let price = forma["Book.Price"].value;
    let priceSpan = document.querySelector("#priceSpan");

    if (!name) {
        nameSpan.innerHTML = "Ovo polje je obavezno!!!";
        isValid = false;
    }
    else if (name.length < 3 || name.length > 50) {
        nameSpan.innerHTML = "Vrednost polja mora biti izmedju 3 i 50!!!";
        isValid = false;
    }
    else {
        nameSpan.innerHTML = "";
    }

    if (!price) {
        priceSpan.innerHTML = "Ovo polje je obavezno!!!";
        isValid = false;
    }
    else if (price< 0) {
        priceSpan.innerHTML = "Vrednost polja mora biti pozitivan broj!!!";
        isValid = false;
    }
    else {
        priceSpan.innerHTML = "";
    }

    return isValid;
}