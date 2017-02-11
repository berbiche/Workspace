(($, Globalize) => {
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

    // Redéfinit la méthode number de jQuery Validation qui valide un nombre en général
    $.validator.methods.number = function(value, element) {
        return this.optional(element) || !isNaN(Globalize.parseFloat(value));
    };

    // Redéfinit la méthode range de jQuery Validation qui valide une intervalle de nombres
    $.validator.methods.range = function(value, element, param) {
        return this.optional(element) ||
            Globalize.parseFloat(value) >= param[0] && Globalize.parseFloat(value) <= param[1];
    };

    //Set la culture à celle du cookie
    $(() => {
        if (typeof Globalize !== "function")
            return;
        var a = document.cookie.match(/langue=([-a-z]+)/i);
        Globalize.culture(a != null ? a[1] : "fr-CA");
        console.log("Globalize.culture().name: "+Globalize.culture().name);
    });

})($, Globalize);