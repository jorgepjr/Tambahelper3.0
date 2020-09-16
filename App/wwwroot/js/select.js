$(document).ready(function () {
    CarregarSelect();

    $('#TipoDeProblema').change();

});

function CarregarSelect() {
    $.getJSON('/json/problemas.json', function (data) {
        var options = '<option data-id="" value="">Selecione um Tipo de Probelma</option>';

        $.each(data, function (key, val) {
            options += '<option data-id="' + val.id + '"value="' + val.problema + '">' + val.problema + '</option>';

            $("#TipoDeProblema").html(options);
        });
    });
}