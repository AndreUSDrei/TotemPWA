// wwwroot/js/filtroCategorias.js

function inicializarFiltroCategorias() {
    // Mapeamento dos itens para suas categorias
    const itensCategorias = {
        'Burguer 404': 'carne',
        'Full Stack': 'carne',
        'Looping Triplo': 'carne',
        'Hello Word': 'carne',
        'VS Veggie': 'vegano',
        'Backend': 'frango',
        'Frontend': 'carne',
        'DevOps Bacon': 'carne',
        'Byte Burguer': 'carne',
        'Index Burguer': 'carne'
    };

    // Seleciona todos os links de categoria
    const linksCategorias = document.querySelectorAll('.nav-categorias a');
    
    // Seleciona todos os itens do cardápio
    const itensCardapio = document.querySelectorAll('.pedido-item');

    // Adiciona evento de clique para cada link de categoria
    linksCategorias.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Verifica se o botão clicado já está ativo
            const jaAtivo = this.classList.contains('active');
            
            // Remove a classe 'active' de todos os links
            linksCategorias.forEach(l => l.classList.remove('active'));
            
            // Se o botão já estava ativo, mostra todos os itens
            if (jaAtivo) {
                itensCardapio.forEach(item => {
                    item.style.display = 'block';
                });
                return; // Sai da função sem adicionar a classe active
            }
            
            // Adiciona a classe 'active' apenas no link clicado
            this.classList.add('active');
            
            // Obtém a categoria selecionada (vegano, carne ou frango)
            const categoriaSelecionada = this.textContent.trim().toLowerCase();
            
            // Filtra os itens do cardápio
            itensCardapio.forEach(item => {
                const nomeItem = item.querySelector('.pedido-titulo').textContent;
                const categoriaItem = itensCategorias[nomeItem];
                
                // Mostra ou esconde o item baseado na categoria selecionada
                if (categoriaSelecionada === 'todos' || categoriaItem === categoriaSelecionada) {
                    item.style.display = 'block';
                } else {
                    item.style.display = 'none';
                }
            });
        });
    });

    // Opcional: Ativa a categoria "Todos" por padrão
    // document.querySelector('.nav-categorias a:first-child').click();
}

document.addEventListener('DOMContentLoaded', inicializarFiltroCategorias);