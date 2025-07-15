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
        modal.style.display = 'flex';
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

// Função para atualizar a exibição do carrinho (estilizada)
function atualizarCarrinho() {
    const carrinhoContainer = document.querySelector('.modal-carrinho-grid');
    const totalElement = document.querySelector('.modal-carrinho-total');
    const subtotalBarraInferior = document.getElementById('cartTotal');
    const carrinhoVazio = document.querySelector('.carrinho-vazio');
    const finalizarBtn = document.getElementById('finalizarPedidoBtn');
    const prosseguirBtn = document.getElementById('prosseguir');

    // Verifica se o carrinho está vazio
    if (!carrinhoItens || carrinhoItens.length === 0) {
        if (carrinhoContainer) {
            carrinhoContainer.innerHTML = `
                <div class="carrinho-vazio">
                    <p class="carrinho-vazio-texto">Seu carrinho está vazio</p>
                </div>
            `;
        }
        if (totalElement) totalElement.textContent = 'R$ 0,00';
        if (subtotalBarraInferior) subtotalBarraInferior.textContent = 'R$ 0,00';
        if (finalizarBtn) {
            finalizarBtn.classList.add('desabilitado');
            finalizarBtn.style.pointerEvents = 'none';
            finalizarBtn.style.opacity = '0.5';
        }
        if (prosseguirBtn) {
            prosseguirBtn.classList.add('desabilitado');
            prosseguirBtn.style.pointerEvents = 'none';
            prosseguirBtn.style.opacity = '0.5';
        }
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
                    <img class="img-carrinho" src="${item.imagem}" alt="${item.nome}">
                    <div class="detalhes-item-carrinho">
                        <h3 class="carrinho-titulo-lanche">${item.nome}</h3>
                        <p class="preco-item">R$ ${item.preco.toFixed(2)}</p>
                    </div>
                </div>
                <div class="controles-item">
                    <button class="botao-remove" onclick="removerItemCarrinho(${item.id})">-</button>
                    <span class="quantidade-item">${item.quantidade}x</span>
                    <button class="botao-adiciona" onclick="adicionarItemCarrinho(${item.id})">+</button>
                    <button class="btn-remover-carrinho" onclick="removerTodoItem(${item.id})">
                        <img src="/img/lixeira.png" alt="Remover" style="width:24px;height:24px;object-fit:contain;" />
                    </button>
                </div>
            </div>
        `;
    });
    
    // Atualiza a interface
    if (carrinhoContainer) carrinhoContainer.innerHTML = htmlItens;
    if (totalElement) totalElement.textContent = `R$ ${total.toFixed(2)}`;
    if (subtotalBarraInferior) subtotalBarraInferior.textContent = `R$ ${total.toFixed(2)}`;
    if (carrinhoVazio) carrinhoVazio.style.display = 'none';
    if (finalizarBtn) {
        finalizarBtn.classList.remove('desabilitado');
        finalizarBtn.style.pointerEvents = 'auto';
        finalizarBtn.style.opacity = '1';
    }
    if (prosseguirBtn) {
        prosseguirBtn.classList.remove('desabilitado');
        prosseguirBtn.style.pointerEvents = 'auto';
        prosseguirBtn.style.opacity = '1';
    }

    // Salva as alterações
    salvarCarrinho();
}

// Função para adicionar item ao carrinho
function adicionarAoCarrinho(id, nome, preco, imagem) {
    carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || []; // Sincronizar estado com localStorage
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

    salvarCarrinho(); // Salvar o estado atualizado no localStorage
    atualizarCarrinho();
}

// Função para adicionar quantidade de um item
function adicionarItemCarrinho(id) {
    carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || []; // Sincronizar estado com localStorage
    const item = carrinhoItens.find(item => item.id === id);
    if (item) {
        item.quantidade += 1;
        salvarCarrinho(); // Salvar o estado atualizado no localStorage
        atualizarCarrinho();
    }
}

// Função para remover quantidade de um item
function removerItemCarrinho(id) {
    carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || []; // Sincronizar estado com localStorage
    const itemIndex = carrinhoItens.findIndex(item => item.id === id);

    if (itemIndex !== -1) {
        if (carrinhoItens[itemIndex].quantidade > 1) {
            carrinhoItens[itemIndex].quantidade -= 1;
        } else {
            carrinhoItens.splice(itemIndex, 1); // Remover o item completamente se a quantidade for 0
        }
        salvarCarrinho(); // Salvar o estado atualizado no localStorage
        atualizarCarrinho();
    }
}

// Função para remover completamente um item
function removerTodoItem(id) {
    carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || []; // Sincronizar estado com localStorage
    carrinhoItens = carrinhoItens.filter(item => item.id !== id);
    salvarCarrinho(); // Salvar o estado atualizado no localStorage
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
    const fecharBtn = document.querySelector('.fechar-modal-carrinho');
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

    // Atualiza o carrinho ao carregar a página
    atualizarCarrinho();

    const prosseguirBtn = document.getElementById('prosseguir');
    if (prosseguirBtn) {
        prosseguirBtn.addEventListener('click', function(e) {
            if (!carrinhoItens || carrinhoItens.length === 0) {
                e.preventDefault();
                return;
            }
            // Salva o carrinho no localStorage para a tela de revisão
            localStorage.setItem('carrinhoItens', JSON.stringify(carrinhoItens));
            // Redireciona para a tela de revisão normalmente (link já faz isso)
        });
    }
});

// Listener para atualizar o carrinho quando um item for editado
window.addEventListener('atualizarCarrinho', function() {
    carrinhoItens = JSON.parse(localStorage.getItem('carrinhoItens')) || [];
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