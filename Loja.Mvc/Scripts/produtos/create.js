var pesquisarButton = $("#pesquisarButton");

pesquisarButton.click(obterProdutoPorCategoria);

//$("#closePopover").click(fecharPopover);
$(document).on("click", "#closePopover", fecharPopover);

function obterProdutoPorCategoria() {
    /*var*/ /*let*/ const categoriaId = $("#CategoriaId").val();
    //var x = 10;
    //var x = 5;

    //var ajax = new XMLHttpRequest();

    $.ajax({
        url: "/produtos/categoria",
        type: "GET",
        data: {categoriaId}
    })
        .done(function (response) { exibirPopover(response); })
        .fail()
        .always();

    //alert("Cheguei");
}

function exibirPopover(response) {
    pesquisarButton
        .popover("destroy")
        .popover({
            content: montarGridProdutos(response),
            html: true,
            animation: true,
            title: "Produtos desta categoria <span class='close' id='closePopover'>&times;</span>"
        })
        .popover("show");
}

function montarGridProdutos(produtos) {
    var html = "<table class='table table-striped'>";

    html += "<tr><th>Produto</th><th>Preço</th><th>Estoque</th></tr>";

    $(produtos).each(
        function (i) {
            html += "<tr>";
            html += "<td>" + produtos[i].Nome + "</td>";
            html += "<td>" + produtos[i].Preco.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + "</td>";
            html += "<td>" + produtos[i].Estoque + "</td>";
            html += "</tr>";
        }
    );

    return html + "</table>";
}

function fecharPopover() {
    pesquisarButton.popover("destroy");
}