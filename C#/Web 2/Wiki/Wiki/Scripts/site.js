(($) => {
    //Ajoute l'option de preview dans le browser
    $('#ShowPreview')
        .click(() => {
            $('#Preview').remove();
            $('form').after(`<div id="Preview"><hr/>${$('#Contenu').val()}</div>`);
        });

    //Pour rediriger vers un article
    $('#ViewArticle')
        .click(() => {
            window.location.href = window.location.origin + '/Home/Index/' + $('#id').val();
        });

})($);