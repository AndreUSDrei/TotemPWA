document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("modalCarrinho");
    const botaoCarrinho = document.getElementById("botaoCarrinho");
    const fecharModal = document.getElementById("fecharModal");
    botaoCarrinho.addEventListener("click", function () {
        modal.style.display = "flex";
    });
    fecharModal.addEventListener("click", function () {
        modal.style.display = "none";
    });
    window.addEventListener("click", function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    });
    const botaoAdicionar = document.querySelectorAll('.botaoAdiciona');
    const botaoRemover = document.querySelectorAll('.botaoRemove');
    function atualizarIngredienteQuantidade(elemento, operacao) {
        const IngredienteQuantidade = elemento.parentElement.querySelector('.IngredienteQuantidade');
        let valorAtual = parseInt(IngredienteQuantidade.innerText, 10); 
        if (isNaN(valorAtual)) {
            valorAtual = 0;
        }
        if (operacao === 'add') {
            valorAtual++;
        } 
        else if (operacao === 'remove') {
            if (valorAtual > 0) {
                valorAtual--;
            }
        }
        IngredienteQuantidade.innerText = `${valorAtual}x`;
    }
    botaoAdicionar.forEach((botao) => {
        botao.addEventListener('click', function () {
            atualizarIngredienteQuantidade(botao, 'add');
        });
    });
    botaoRemover.forEach((botao) => {
        botao.addEventListener('click', function () {
            atualizarIngredienteQuantidade(botao, 'remove');
        });
    });
});