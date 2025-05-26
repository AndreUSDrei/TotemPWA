// wwwroot/js/filtroCategorias.js

// Função para filtrar itens por categoria
function filtrarPorCategoria(categoria, tipo) {
    const conteudo = document.getElementById(`conteudo-${tipo}`);
    const itens = conteudo.querySelectorAll('.pedido-item');
    
    // Remove a classe active de todos os links de categoria
    conteudo.querySelectorAll('.nav-categorias a').forEach(link => {
        link.classList.remove('active');
    });
    
    // Adiciona a classe active ao link clicado
    event.currentTarget.classList.add('active');

    // Filtra os itens baseado na categoria
    itens.forEach(item => {
        const itemCategoria = item.getAttribute('data-categoria');
        if (categoria === 'todos' || itemCategoria === categoria) {
            item.style.display = 'flex';
        } else {
            item.style.display = 'none';
        }
    });
}

// Função para inicializar os filtros de uma seção
function inicializarFiltros(secao) {
    const conteudo = document.getElementById(`conteudo-${secao}`);
    if (!conteudo) return;

    const links = conteudo.querySelectorAll('.nav-categorias a');
    links.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const categoria = this.getAttribute('data-categoria');
            filtrarPorCategoria(categoria, secao);
        });
    });

    // Ativa o filtro "Todos" por padrão
    const filtroTodos = conteudo.querySelector('.nav-categorias a[data-categoria="todos"]');
    if (filtroTodos) {
        filtroTodos.click();
    }
}

// Adiciona os event listeners para os links de categoria
document.addEventListener('DOMContentLoaded', function() {
    // Configura os filtros para cada seção
    const secoes = ['lanches', 'sobremesas', 'molhos', 'ofertas', 'bebidas'];
    
    // Inicializa os filtros para cada seção
    secoes.forEach(secao => {
        inicializarFiltros(secao);
    });

    // Atualiza a função alternarConteudo para reinicializar os filtros
    const alternarConteudoOriginal = window.alternarConteudo;
    window.alternarConteudo = function(tipo) {
        // Chama a função original
        alternarConteudoOriginal(tipo);
        
        // Reinicializa os filtros para a seção atual
        inicializarFiltros(tipo);
    };
});