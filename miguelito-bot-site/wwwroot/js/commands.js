window.onload = function () {
    $('.commandSpan').click( function (e) { 
        e.preventDefault();

        var expansiveElement = $(this).parent().find('.expansive');
        var time = 200;

        if (expansiveElement.is(':hidden')) expansiveElement.slideDown(time);
        else expansiveElement.slideUp(time);

    });    
}

function exibirComandos (id) {
    var divs = document.getElementById('comandos').querySelectorAll('.commandsDiv');
    
    if (id == 'todos') { 
        divs.forEach(e => {
            if ($(e).is(':hidden')) $(e).show(200);
        });
    } else {
        divs.forEach(e => {
            if ($(e).is(`#${id}`)) {
                $(e).show(200);
            } else {
                $(e).hide(200);
            }
        })
    }
}