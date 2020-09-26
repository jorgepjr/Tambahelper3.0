$(document).ready(function () {
    CarregarSelect();

    $('#TipoDeProblema').change();

});

function CarregarSelect() {
    $.getJSON('/json/helps.json', function (data) {
        var options = '<option data-id="" value="">Selecione o tipo de help</option>';

        $.each(data, function (key, val) {
            options += '<option data-id="' + val.id + '"value="' + val.help + '">' + val.help + '</option>';

            $("#TipoDeProblema").html(options);
        });
    });
}