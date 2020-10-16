function ValidateForm() {
    let forma = document.querySelector("#f1");
    let isValid = true;

    let name = forma["Name"].value;
    let nameSpan = document.querySelector("#nameSpan");

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

    return isValid;
}