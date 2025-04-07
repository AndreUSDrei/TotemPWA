// modal.js - Controle do modal do carrinho (versão corrigida)

// Variáveis globais
let carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || [];

// Função para salvar o carrinho no localStorage
function salvarCarrinho() {
    localStorage.setItem('carrinhoItens', JSON.stringify(carrinhoItens));
}

// Função para abrir o modal do carrinho
function abrirModalCarrinho() {
    const modal = document.getElementById('modalCarrinho');
    if (modal) {
        modal.style.display = 'block';
        atualizarCarrinho();
        document.body.style.overflow = 'hidden'; // Impede scroll da página
    }
}

// Função para fechar o modal do carrinho
function fecharModalCarrinho() {
    const modal = document.getElementById('modalCarrinho');
    if (modal) {
        modal.style.display = 'none';
        document.body.style.overflow = 'auto'; // Restaura scroll da página
    }
}

// Função para atualizar a exibição do carrinho (CORRIGIDA)
function atualizarCarrinho() {
    const carrinhoContainer = document.querySelector('.modal-pedido');
    const listaItens = document.querySelector('.listaIngredientes');
    const totalElement = document.querySelector('.barra-inferior .preco span');
    const carrinhoVazio = document.querySelector('.carrinho-vazio');

    // Verifica se o carrinho está vazio
    if (!carrinhoItens || carrinhoItens.length === 0) {
        if (carrinhoContainer) {
            carrinhoContainer.innerHTML = `
                <div class="carrinho-vazio">
                    <img src="~/img/carrinho-vazio.png" alt="Carrinho vazio">
                    <p>Seu carrinho está vazio</p>
                </div>
            `;
        }
        if (listaItens) listaItens.innerHTML = '';
        if (totalElement) totalElement.textContent = 'R$ 0,00';
        return;
    }

    // Se há itens no carrinho, atualiza a lista
    let total = 0;
    let htmlItens = '';
    
    carrinhoItens.forEach(item => {
        total += item.preco * item.quantidade;
        htmlItens += `
            <div class="item-carrinho" data-id="${item.id}">
                <div class="info-item">
                    <img src="${item.imagem}" alt="${item.nome}">
                    <div class="detalhes-item">
                        <h3>${item.nome}</h3>
                        <p class="preco-item">R$ ${item.preco.toFixed(2)}</p>
                    </div>
                </div>
                <div class="controles-item">
                    <button class="botaoRemove" onclick="removerItemCarrinho(${item.id})">-</button>
                    <span class="quantidade-item">${item.quantidade}x</span>
                    <button class="botaoAdiciona" onclick="adicionarItemCarrinho(${item.id})">+</button>
                    <button class="botaoRemover" onclick="removerTodoItem(${item.id})">
                        <img src="~/img/lixeira.png" alt="Remover">
                    </button>
                </div>
            </div>
        `;
    });
    
    // Atualiza a interface
    if (listaItens) listaItens.innerHTML = htmlItens;
    if (totalElement) totalElement.textContent = `R$ ${total.toFixed(2)}`;
    if (carrinhoVazio) carrinhoVazio.style.display = 'none';

    // Salva as alterações
    salvarCarrinho();
}

// Função para adicionar item ao carrinho
function adicionarAoCarrinho(id, nome, preco, imagem) {
    const itemExistente = carrinhoItens.find(item => item.id === id);
    
    if (itemExistente) {
        itemExistente.quantidade += 1;
    } else {
        carrinhoItens.push({
            id,
            nome,
            preco,
            imagem,
            quantidade: 1
        });
    }
    
    atualizarCarrinho();
}

// Função para adicionar quantidade de um item
function adicionarItemCarrinho(id) {
    const item = carrinhoItens.find(item => item.id === id);
    if (item) {
        item.quantidade += 1;
        atualizarCarrinho();
    }
}

// Função para remover quantidade de um item
function removerItemCarrinho(id) {
    const itemIndex = carrinhoItens.findIndex(item => item.id === id);
    
    if (itemIndex !== -1) {
        if (carrinhoItens[itemIndex].quantidade > 1) {
            carrinhoItens[itemIndex].quantidade -= 1;
        } else {
            carrinhoItens.splice(itemIndex, 1);
        }
        atualizarCarrinho();
    }
}

// Função para remover completamente um item
function removerTodoItem(id) {
    carrinhoItens = carrinhoItens.filter(item => item.id !== id);
    atualizarCarrinho();
}

// Função para limpar todo o carrinho
function limparCarrinho() {
    carrinhoItens = [];
    atualizarCarrinho();
    localStorage.removeItem('carrinhoItens');
}

// Configuração dos eventos quando o DOM estiver carregado
document.addEventListener('DOMContentLoaded', function() {
    // Fechar modal quando clicar no X
    const fecharBtn = document.querySelector('.fechar-modal');
    if (fecharBtn) {
        fecharBtn.addEventListener('click', fecharModalCarrinho);
    }
    
    // Fechar modal quando clicar fora da área de conteúdo
    const modal = document.getElementById('modalCarrinho');
    if (modal) {
        modal.addEventListener('click', function(e) {
            if (e.target === modal) {
                fecharModalCarrinho();
            }
        });
    }
    
    // Evento para os itens do menu (exemplo)
    document.querySelectorAll('.pedido-item').forEach(item => {
        item.addEventListener('click', function() {
            const id = parseInt(this.getAttribute('data-id'));
            const nome = this.querySelector('.pedido-titulo').textContent;
            const precoText = this.querySelector('.pedido-preco').textContent;
            const preco = parseFloat(precoText.replace('R$', '').replace(',', '.').trim());
            const imagem = this.querySelector('.pedido-imagem').getAttribute('src');
            
            adicionarAoCarrinho(id, nome, preco, imagem);
        });
    });

    // Atualiza o carrinho ao carregar a página
    atualizarCarrinho();
});

// Expor funções para o escopo global
window.abrirModalCarrinho = abrirModalCarrinho;
window.fecharModalCarrinho = fecharModalCarrinho;
window.adicionarAoCarrinho = adicionarAoCarrinho;
window.adicionarItemCarrinho = adicionarItemCarrinho;
window.removerItemCarrinho = removerItemCarrinho;
window.removerTodoItem = removerTodoItem;
window.limparCarrinho = limparCarrinho;