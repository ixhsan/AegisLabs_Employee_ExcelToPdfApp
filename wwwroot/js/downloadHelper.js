window.downloadFile = function (url) {
    if (!window.Blazor) {
        console.error("Blazor not ready yet.");
        return;
    }

    const link = document.createElement('a');
    link.href = url;
    link.download = '';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
