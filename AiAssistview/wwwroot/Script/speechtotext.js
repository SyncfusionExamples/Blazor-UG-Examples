// Checks if the contenteditable element contains meaningful text and cleans up.
window.isFooterContainsValue = function (elementref) {
    if (!elementref) return "";

    const text = (elementref.innerText || "").trim();

    // If empty, normalize stray <br> or empty HTML to empty string
    if (text === "") {
        const html = (elementref.innerHTML || "").trim();
        if (html === "<br>" || html === "") {
            elementref.innerHTML = "";
        }
        return "";
    }

    return elementref.innerText || "";
};

// Clears the text content of a contenteditable element.
window.emptyFooterValue = function (elementref) {
    if (elementref) {
        elementref.innerText = "";
    }
};

// Updates the text content of a contenteditable element with a specified value.
window.updateContentEditableDiv = function (element, value) {
    if (element) {
        // Using innerText preserves plain text; switch to innerHTML if you need HTML.
        element.innerText = value || "";
    }
};