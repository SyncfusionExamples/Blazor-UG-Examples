window.RichTextEditor = {
    setBackground: function () {
        var rteInstance = document.getElementById('defaultRTE').blazor__instance;
        rteInstance.formatter.editorManager.nodeSelection.setSelectionText(
            document, rteInstance.inputElement.childNodes[0].childNodes[0], rteInstance.inputElement.childNodes[0].childNodes[0], 9, 20);
        return true;
    }
}