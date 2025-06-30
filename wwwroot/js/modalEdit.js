console.log("Script modalEdit.js carregado!");
document.addEventListener('DOMContentLoaded', function() {
    const modalEditar = document.getElementById('modalEditarItem');
    const fecharModalEditar = document.querySelector('.fechar-modal-editar');
    const botaoAdicionarEditar = document.querySelector('.modal-btn-confirm');

    let currentItemId = null;
    let currentItemBasePrice = 0;
    let editedItem = null; // Variável para armazenar o item editado temporariamente

    window.abrirModalEditar = function(elemento) {
        currentItemId = parseInt(elemento.getAttribute('data-id'));
        const titulo = elemento.querySelector('.pedido-titulo').textContent;
        const imagem = elemento.querySelector('.pedido-imagem').src;
        const precoText = elemento.querySelector('.pedido-preco').textContent;
        currentItemBasePrice = parseFloat(precoText.replace('R$', '').replace(',', '.').trim());
        const descricao = elemento.getAttribute('data-descricao');

        // Busca ingredientes do produto dinamicamente
        let ingredientes = [];
        if (window.ingredientesPorProduto && window.ingredientesPorProduto[currentItemId]) {
            ingredientes = JSON.parse(JSON.stringify(window.ingredientesPorProduto[currentItemId]));
            // Adiciona campo quantidade para cada ingrediente (default 1)
            ingredientes.forEach(ing => {
                if (typeof ing.quantidade === 'undefined') ing.quantidade = 1;
            });
        }

        editedItem = {
            id: currentItemId,
            nome: titulo,
            preco: currentItemBasePrice,
            imagem: imagem,
            quantidade: 1,
            ingredientes: ingredientes
        };

        document.querySelector('.modal-editar-titulo').textContent = titulo;
        document.querySelector('.modal-editar-imagem').src = imagem;
        document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
        document.querySelector('.modal-editar-descricao').textContent = descricao;

        const lista = document.querySelector('.modal-editar-ingredientes');
        lista.innerHTML = '';
        if (ingredientes.length > 0) {
            ingredientes.forEach(ing => {
                const divIngrediente = document.createElement('div');
                divIngrediente.className = 'modal-editar-ingrediente';
                divIngrediente.innerHTML = `
                    <span class="modal-editar-ingrediente-nome">${ing.nome}</span>
                    <button class="botao-remover-editar">-</button>
                    <span class="modal-editar-ingrediente-quantidade">${ing.quantidade}x</span>
                    <button class="botao-adicionar-editar">+</button>
                `;
                lista.appendChild(divIngrediente);
            });
        } else {
            lista.innerHTML = '<p>Nenhum ingrediente disponível para edição.</p>';
        }
        modalEditar.style.display = 'flex';
    };

    fecharModalEditar.addEventListener('click', function() {
        modalEditar.style.display = 'none';
        editedItem = null; // Descartar alterações se o modal for fechado
    });

    window.addEventListener('click', function(event) {
        if (event.target === modalEditar) {
            modalEditar.style.display = 'none';
            editedItem = null; // Descartar alterações se o modal for fechado
        }
    });

    document.querySelector('.modal-editar-ingredientes').addEventListener('click', function(e) {
        const target = e.target;
        const ingredienteDiv = target.closest('.modal-editar-ingrediente');
        if (!ingredienteDiv) return;

        const quantidadeSpan = ingredienteDiv.querySelector('.modal-editar-ingrediente-quantidade');
        const nomeIngrediente = ingredienteDiv.querySelector('.modal-editar-ingrediente-nome').textContent;
        let quantidade = parseInt(quantidadeSpan.textContent) || 0;
        const ingrediente = editedItem.ingredientes.find(ing => ing.nome === nomeIngrediente);

        if (target.classList.contains('botao-adicionar-editar')) {
            quantidade++;
            currentItemBasePrice += ingrediente.preco;
            ingrediente.quantidade = quantidade;
        } else if (target.classList.contains('botao-remover-editar')) {
            if (quantidade > 0) {
                quantidade--;
                currentItemBasePrice -= ingrediente.preco;
                ingrediente.quantidade = quantidade;
            }
        }

        quantidadeSpan.textContent = `${quantidade}x`;
        document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
        editedItem.preco = currentItemBasePrice;
    });

    botaoAdicionarEditar.addEventListener('click', function() {
        if (editedItem) {
            const carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || [];
            const existingItem = carrinhoItens.find(item => item.id === editedItem.id);
            if (existingItem) {
                existingItem.preco = editedItem.preco;
                existingItem.ingredientes = editedItem.ingredientes;
            } else {
                carrinhoItens.push(editedItem);
            }
            localStorage.setItem('carrinhoItens', JSON.stringify(carrinhoItens)); // Salvar no localStorage

            // Disparar evento para atualizar o carrinho
            const event = new CustomEvent('atualizarCarrinho');
            window.dispatchEvent(event);

            modalEditar.style.display = 'none';
            editedItem = null; // Limpar o item editado após confirmação
        }
    });
});