console.log("Script modalEdit.js carregado!");
document.addEventListener('DOMContentLoaded', function() {
    const modalEditar = document.getElementById('modalEditarItem');
    const fecharModalEditar = document.querySelector('.fechar-modal-editar');
    const botaoAdicionarEditar = document.querySelector('.botao-adicionar-editar');
    const ingredientesPorItem = {
        1: [ 
            { nome: 'Pão de gergelim', quantidade: 1 },
            { nome: 'Queijo cheddar', quantidade: 2 },
            { nome: 'Hambúrguer de costela', quantidade: 2 },
            { nome: 'Alface', quantidade: 1 },
            { nome: 'Cebola', quantidade: 1 }
        ],
    };
    window.abrirModalEditar = function(elemento) {
        const itemId = parseInt(elemento.getAttribute('data-id'));
        const titulo = elemento.querySelector('.pedido-titulo').textContent;
        const imagem = elemento.querySelector('.pedido-imagem').src;
        const preco = elemento.querySelector('.pedido-preco').textContent;
        const descricao = elemento.getAttribute('data-descricao');
        document.querySelector('.modal-editar-titulo').textContent = titulo;
        document.querySelector('.modal-editar-imagem').src = imagem;
        document.querySelector('.modal-editar-preco').textContent = preco;
        document.querySelector('.modal-editar-descricao').textContent = descricao;
        const lista = document.querySelector('.modal-editar-ingredientes');
        lista.innerHTML = '';
        if (ingredientesPorItem[itemId]) {
            ingredientesPorItem[itemId].forEach(ing => {
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
        let quantidade = parseInt(quantidadeSpan.textContent);
        
        if (target.classList.contains('botao-adicionar-editar')) {
            quantidade++;
        } else if (target.classList.contains('botao-remover-editar')) {
            if (quantidade > 0) {
                quantidade--;
            }
        }
        
        quantidadeSpan.textContent = quantidade + 'x';
    });
    botaoAdicionarEditar.addEventListener('click', function() {
        alert('Item adicionado ao carrinho!');
        modalEditar.style.display = 'none';
    });
});