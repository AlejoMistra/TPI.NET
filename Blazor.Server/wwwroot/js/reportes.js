// Visor y descarga de los PDF de reportes (Blazor los pasa como Uint8Array)
window.reportes = {
    _urls: {},

    // Muestra el PDF en el iframe indicado, liberando la URL anterior
    mostrarPdf: function (iframeId, bytes) {
        const iframe = document.getElementById(iframeId);
        if (!iframe) return;

        if (reportes._urls[iframeId]) {
            URL.revokeObjectURL(reportes._urls[iframeId]);
        }

        const url = URL.createObjectURL(new Blob([bytes], { type: 'application/pdf' }));
        reportes._urls[iframeId] = url;
        iframe.src = url;
    },

    // Descarga el PDF con el nombre indicado
    descargarPdf: function (nombreArchivo, bytes) {
        const url = URL.createObjectURL(new Blob([bytes], { type: 'application/pdf' }));
        const enlace = document.createElement('a');
        enlace.href = url;
        enlace.download = nombreArchivo;
        document.body.appendChild(enlace);
        enlace.click();
        enlace.remove();
        URL.revokeObjectURL(url);
    }
};
