console.log("Script modalEdit.js carregado!");
document.addEventListener('DOMContentLoaded', function() {
    const modalEditar = document.getElementById('modalEditarItem');
    const fecharModalEditar = document.querySelector('.fechar-modal-editar');
    const botaoAdicionarEditar = document.querySelector('.modal-btn-confirm');
    let currentItemId = null;
    let currentItemBasePrice = 0;
    let editedItem = null;
    let currentCategoriaSlug = null;
    let currentQuantidade = 1;

    // ... mantenha seu objeto ingredientesPorItem ...
    // (não repito aqui para não poluir, mas mantenha igual)

    window.abrirModalEditar = async function(elemento) {
        currentItemId = parseInt(elemento.getAttribute('data-id'));
        currentCategoriaSlug = elemento.getAttribute('data-categoria-slug');
        console.log('abrirModalEditar', { currentItemId, currentCategoriaSlug });
        const titulo = elemento.querySelector('.pedido-titulo').textContent;
        const imagem = elemento.querySelector('.pedido-imagem').src;
        const precoText = elemento.querySelector('.pedido-preco').textContent;
        currentItemBasePrice = parseFloat(precoText.replace('R$', '').replace(',', '.').trim());
        const descricao = elemento.getAttribute('data-descricao');
        currentQuantidade = 1;

        editedItem = {
            id: currentItemId,
            nome: titulo,
            preco: currentItemBasePrice,
            imagem: imagem,
            quantidade: 1,
            ingredientes: []
        };

        document.querySelector('.modal-editar-titulo').textContent = titulo;
        document.querySelector('.modal-editar-imagem').src = imagem;
        document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
        document.querySelector('.modal-editar-descricao').textContent = descricao;

        const lista = document.querySelector('.modal-editar-ingredientes');
        lista.innerHTML = '';
        // Se for lanche, busca ingredientes do backend
        if (currentCategoriaSlug && currentCategoriaSlug.toLowerCase().trim() === 'lanches') {
            try {
                console.log('Buscando ingredientes do backend para o produto', currentItemId);
                const resp = await fetch(`/api/produto/${currentItemId}/ingredientes`);
                const ingredientes = await resp.json();
                console.log('Ingredientes recebidos:', ingredientes);
                if (ingredientes.length > 0) {
                    editedItem.ingredientes = ingredientes.map(ing => ({
                        id: ing.id,
                        nome: ing.nome,
                        preco: ing.preco,
                        quantidade: 1
                    }));
                    // Limitar todos os ingredientes a no máximo 10 ao abrir
                    let total = 0;
                    editedItem.ingredientes.forEach(ing => {
                        if (ing.quantidade > 10) ing.quantidade = 10;
                        if (ing.quantidade < 0 || isNaN(ing.quantidade)) ing.quantidade = 0;
                        total += ing.quantidade;
                    });
                    if (total > 10) {
                        // Se a soma já passar de 10, zera todos e coloca 1 nos primeiros até 10
                        editedItem.ingredientes.forEach(ing => ing.quantidade = 0);
                        for (let i = 0; i < 10; i++) {
                            if (editedItem.ingredientes[i]) editedItem.ingredientes[i].quantidade = 1;
                        }
                    }
                    ingredientes.forEach(ing => {
                        const divIngrediente = document.createElement('div');
                        divIngrediente.className = 'modal-editar-ingrediente';
                        divIngrediente.innerHTML = `
                            <span class='modal-editar-ingrediente-nome' style='font-weight:400; text-align:left; flex:1;'>${ing.nome}</span>
                            <span style='display:flex;align-items:center;gap:2px;'>
                                <button class='botao-remover-editar'>-</button>
                                <span class='modal-editar-ingrediente-quantidade'>1x</span>
                                <button class='botao-adicionar-editar'>+</button>
                            </span>
                        `;
                        lista.appendChild(divIngrediente);
                    });
                } else {
                    lista.innerHTML = '<p>Nenhum ingrediente disponível para edição.</p>';
                }
            } catch (e) {
                console.error('Erro ao carregar ingredientes:', e);
                lista.innerHTML = '<p>Erro ao carregar ingredientes.</p>';
            }
        } else {
            // Para produtos não editáveis: só campo de quantidade
            lista.innerHTML = `
                <div style="display:flex;align-items:center;justify-content:center;gap:16px;">
                    <button class="botao-remover-editar">-</button>
                    <span class="modal-editar-quantidade">1x</span>
                    <button class="botao-adicionar-editar">+</button>
                </div>
            `;
        }
        modalEditar.style.display = 'flex';
    };

    fecharModalEditar.addEventListener('click', function() {
        modalEditar.style.display = 'none';
        editedItem = null;
    });

    window.addEventListener('click', function(event) {
        if (event.target === modalEditar) {
            modalEditar.style.display = 'none';
            editedItem = null;
        }
    });

    document.querySelector('.modal-editar-ingredientes').addEventListener('click', function(e) {
        const target = e.target;
        // Se for lanche: ingredientes customizáveis
        if (currentCategoriaSlug && currentCategoriaSlug.toLowerCase().trim() === 'lanches') {
            const ingredienteDiv = target.closest('.modal-editar-ingrediente');
            if (!ingredienteDiv) return;
            const quantidadeSpan = ingredienteDiv.querySelector('.modal-editar-ingrediente-quantidade');
            const nomeIngrediente = ingredienteDiv.querySelector('.modal-editar-ingrediente-nome').textContent;
            let quantidade = parseInt(quantidadeSpan.textContent) || 0;
            const ingrediente = editedItem.ingredientes.find(ing => ing.nome === nomeIngrediente);
            // Corrige qualquer ingrediente acima de 10
            editedItem.ingredientes.forEach(ing => {
                if (ing.quantidade > 10) ing.quantidade = 10;
                if (ing.quantidade < 0 || isNaN(ing.quantidade)) ing.quantidade = 0;
            });
            let totalAdicionais = editedItem.ingredientes.reduce((acc, ing) => acc + (ing.quantidade || 0), 0);
            if (target.classList.contains('botao-adicionar-editar')) {
                if (quantidade < 10 && totalAdicionais < 10) {
                    quantidade++;
                    ingrediente.quantidade = quantidade;
                    currentItemBasePrice += ingrediente.preco;
                }
            } else if (target.classList.contains('botao-remover-editar')) {
                if (quantidade > 0) {
                    quantidade--;
                    ingrediente.quantidade = quantidade;
                    currentItemBasePrice -= ingrediente.preco;
                }
            }
            // Recalcula total após ajuste
            totalAdicionais = editedItem.ingredientes.reduce((acc, ing) => acc + (ing.quantidade || 0), 0);
            // Nunca deixar NaN, negativo ou passar de 10 por ingrediente
            if (isNaN(quantidade) || quantidade < 0) quantidade = 0;
            if (quantidade > 10) quantidade = 10;
            if (totalAdicionais > 10) {
                // Se passou, desfaz o último incremento
                quantidade--;
                ingrediente.quantidade = quantidade;
                currentItemBasePrice -= ingrediente.preco;
            }
            quantidadeSpan.textContent = `${quantidade}x`;
            document.querySelector('.modal-editar-preco').textContent = `R$ ${currentItemBasePrice.toFixed(2)}`;
            editedItem.preco = currentItemBasePrice;
        } else {
            // Para produtos não editáveis: só quantidade
            if (target.classList.contains('botao-adicionar-editar')) {
                currentQuantidade++;
            } else if (target.classList.contains('botao-remover-editar')) {
                if (currentQuantidade > 1) currentQuantidade--;
            }
            if (isNaN(currentQuantidade) || currentQuantidade < 1) currentQuantidade = 1;
            document.querySelector('.modal-editar-quantidade').textContent = `${currentQuantidade}x`;
            editedItem.quantidade = currentQuantidade;
            // Atualiza preço total (preço unitário * quantidade)
            document.querySelector('.modal-editar-preco').textContent = `R$ ${(currentItemBasePrice * currentQuantidade).toFixed(2)}`;
            editedItem.preco = currentItemBasePrice * currentQuantidade;
        }
    });

    botaoAdicionarEditar.addEventListener('click', function() {
        if (editedItem) {
            let precoTotal = 0;
            // Lanches: preço base + ingredientes (até 10, mínimo 0)
            if (currentCategoriaSlug && currentCategoriaSlug.toLowerCase().trim() === 'lanches') {
                // Limitar ingredientes entre 0 e 10
                if (Array.isArray(editedItem.ingredientes)) {
                    editedItem.ingredientes = editedItem.ingredientes.filter(ing => ing.quantidade > 0);
                    if (editedItem.ingredientes.length > 10) {
                        editedItem.ingredientes = editedItem.ingredientes.slice(0, 10);
                    }
                }
                precoTotal = currentItemBasePrice; // preço base do produto
                if (Array.isArray(editedItem.ingredientes)) {
                    precoTotal += editedItem.ingredientes.reduce((acc, ing) => acc + (ing.preco * (ing.quantidade || 0)), 0);
                }
                editedItem.quantidade = 1;
            } else {
                // Outros produtos: preço base × quantidade
                precoTotal = currentItemBasePrice * currentQuantidade;
                editedItem.quantidade = currentQuantidade;
                editedItem.ingredientes = [];
            }
            editedItem.preco = precoTotal;
            // Adicionar ao carrinho
            const carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || [];
            const existingItem = carrinhoItens.find(item => item.id === editedItem.id);
            if (existingItem) {
                existingItem.preco = editedItem.preco;
                existingItem.ingredientes = editedItem.ingredientes;
                existingItem.quantidade = editedItem.quantidade;
            } else {
                carrinhoItens.push(editedItem);
            }
            localStorage.setItem('carrinhoItens', JSON.stringify(carrinhoItens));
            const event = new CustomEvent('atualizarCarrinho');
            window.dispatchEvent(event);
            modalEditar.style.display = 'none';
            editedItem = null;
        }
    });
});