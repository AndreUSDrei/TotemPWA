console.log("Script modalEdit.js carregado!");
document.addEventListener('DOMContentLoaded', function() {
    const modalEditar = document.getElementById('modalEditarItem');
    const fecharModalEditar = document.querySelector('.fechar-modal-editar');
    const botaoAdicionarEditar = document.querySelector('.modal-btn-confirm');
    const ingredientesPorItem = {
        1: [ 
            { nome: 'Pão de gergelim', quantidade: 1, preco: 0.50 },
            { nome: 'Queijo cheddar', quantidade: 2, preco: 1.00 },
            { nome: 'Hambúrguer de costela', quantidade: 2, preco: 5.00 },
            { nome: 'Alface', quantidade: 1, preco: 0.30 },
            { nome: 'Cebola', quantidade: 1, preco: 0.20 }
        ],
        2: [
            { nome: 'Pão brioche', quantidade: 1, preco: 1.00 },
            { nome: 'Queijo prato', quantidade: 1, preco: 1.50 },
            { nome: 'Hambúrguer de costela', quantidade: 1, preco: 5.00 },
            { nome: 'Bacon', quantidade: 1, preco: 2.00 },
            { nome: 'Cebola caramelizada', quantidade: 1, preco: 0.50 }
        ],
        3: [
            { nome: 'Pão de gergelim', quantidade: 1, preco: 0.50 },
            { nome: 'Queijo cheddar', quantidade: 3, preco: 1.50 },
            { nome: 'Hambúrguer de costela', quantidade: 3, preco: 7.50 },
            { nome: 'Cebola crispy', quantidade: 1, preco: 0.50 },
            { nome: 'Molho especial', quantidade: 1, preco: 0.70 }
        ],
        4: [
            { nome: 'Pão australiano', quantidade: 1, preco: 1.20 },
            { nome: 'Hambúrguer de fraldinha', quantidade: 1, preco: 5.50 },
            { nome: 'Queijo', quantidade: 1, preco: 1.00 },
            { nome: 'Picles', quantidade: 1, preco: 0.40 },
            { nome: 'Cebola roxa', quantidade: 1, preco: 0.30 }
        ],
        5: [
            { nome: 'Pão integral', quantidade: 1, preco: 0.80 },
            { nome: 'Hambúrguer de grão-de-bico', quantidade: 1, preco: 4.00 },
            { nome: 'Abobrinha', quantidade: 1, preco: 0.50 },
            { nome: 'Berinjela', quantidade: 1, preco: 0.50 },
            { nome: 'Molho de iogurte', quantidade: 1, preco: 0.70 }
        ],
        6: [
            { nome: 'Pão de hambúrguer', quantidade: 1, preco: 0.50 },
            { nome: 'Hambúrguer de costela', quantidade: 1, preco: 5.00 },
            { nome: 'Queijo prato', quantidade: 1, preco: 1.50 },
            { nome: 'Alface', quantidade: 1, preco: 0.30 },
            { nome: 'Tomate', quantidade: 1, preco: 0.40 }
        ],
        7: [
            { nome: 'Pão brioche', quantidade: 1, preco: 1.00 },
            { nome: 'Hambúrguer de costela', quantidade: 2, preco: 10.00 },
            { nome: 'Queijo cheddar', quantidade: 2, preco: 2.00 },
            { nome: 'Bacon', quantidade: 1, preco: 2.00 },
            { nome: 'Molho barbecue', quantidade: 1, preco: 0.70 }
        ],
        8: [
            { nome: 'Pão de gergelim', quantidade: 1, preco: 0.50 },
            { nome: 'Hambúrguer de costela', quantidade: 1, preco: 5.00 },
            { nome: 'Queijo', quantidade: 1, preco: 1.00 },
            { nome: 'Bacon', quantidade: 1, preco: 2.00 },
            { nome: 'Cebola crispy', quantidade: 1, preco: 0.50 }
        ],
        9: [
            { nome: 'Pão de batata', quantidade: 1, preco: 0.70 },
            { nome: 'Hambúrguer de costela', quantidade: 1, preco: 5.00 },
            { nome: 'Queijo coalho', quantidade: 1, preco: 1.50 },
            { nome: 'Molho de pimenta', quantidade: 1, preco: 0.50 }
        ],
        10: [
            { nome: 'Pão de hambúrguer', quantidade: 1, preco: 0.50 },
            { nome: 'Hambúrguer de fraldinha', quantidade: 1, preco: 5.50 },
            { nome: 'Queijo', quantidade: 1, preco: 1.00 },
            { nome: 'Alface', quantidade: 1, preco: 0.30 },
            { nome: 'Tomate', quantidade: 1, preco: 0.40 }
        ]
    };

    let currentItemId = null;
    let currentItemBasePrice = 0;

    window.abrirModalEditar = function(elemento) {
        currentItemId = parseInt(elemento.getAttribute('data-id'));
        const titulo = elemento.querySelector('.pedido-titulo').textContent;
        const imagem = elemento.querySelector('.pedido-imagem').src;
        const precoText = elemento.querySelector('.pedido-preco').textContent;
        currentItemBasePrice = parseFloat(precoText.replace('R$', '').replace(',', '.').trim());
        const descricao = elemento.getAttribute('data-descricao');

        document.querySelector('.modal-editar-titulo').textContent = titulo;
        document.querySelector('.modal-editar-imagem').src = imagem;
        document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
        document.querySelector('.modal-editar-descricao').textContent = descricao;

        const lista = document.querySelector('.modal-editar-ingredientes');
        lista.innerHTML = '';
        if (ingredientesPorItem[currentItemId]) {
            ingredientesPorItem[currentItemId].forEach(ing => {
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
    });

    window.addEventListener('click', function(event) {
        if (event.target === modalEditar) {
            modalEditar.style.display = 'none';
        }
    });

    document.querySelector('.modal-editar-ingredientes').addEventListener('click', function(e) {
        const target = e.target;
        const ingredienteDiv = target.closest('.modal-editar-ingrediente');
        if (!ingredienteDiv) return;

        const quantidadeSpan = ingredienteDiv.querySelector('.modal-editar-ingrediente-quantidade');
        const nomeIngrediente = ingredienteDiv.querySelector('.modal-editar-ingrediente-nome').textContent;
        let quantidade = parseInt(quantidadeSpan.textContent);
        const ingrediente = ingredientesPorItem[currentItemId].find(ing => ing.nome === nomeIngrediente);

        if (target.classList.contains('botao-adicionar-editar')) {
            quantidade++;
            currentItemBasePrice += ingrediente.preco;
        } else if (target.classList.contains('botao-remover-editar')) {
            if (quantidade > 0) {
                quantidade--;
                currentItemBasePrice -= ingrediente.preco;
            }
        }

        quantidadeSpan.textContent = `${quantidade}x`;
        document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
    });

    botaoAdicionarEditar.addEventListener('click', function() {
        alert('Item adicionado ao carrinho com os ingredientes personalizados!');
        modalEditar.style.display = 'none';
    });
});