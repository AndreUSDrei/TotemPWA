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
});
