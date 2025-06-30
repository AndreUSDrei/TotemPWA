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
        // Esconde todos os conteúdos
        document.getElementById('conteudo-lanches').style.display = 'none';
        document.getElementById('conteudo-sobremesas').style.display = 'none';
        document.getElementById('conteudo-molhos').style.display = 'none';
        document.getElementById('conteudo-ofertas').style.display = 'none';
        document.getElementById('conteudo-bebidas').style.display = 'none';
        document.getElementById('conteudo-extras').style.display = 'none';
        document.getElementById('conteudo-combos').style.display = 'none';
        document.getElementById('banner-lanches').style.display = 'none';
        document.getElementById('banner-sobremesas').style.display = 'none';
        document.getElementById('banner-molhos').style.display = 'none';
        document.getElementById('banner-ofertas').style.display = 'none';
        document.getElementById('banner-bebidas').style.display = 'none';
        document.getElementById('banner-extras').style.display = 'none';
        document.getElementById('banner-combos') && (document.getElementById('banner-combos').style.display = 'none');

        // Remove a classe active de todos os itens do menu
        document.querySelectorAll('.menu-item').forEach(item => {
            item.classList.remove('active');
        });

        // Mostra o conteúdo selecionado
        if (tipo === 'combos') {
            document.getElementById('conteudo-combos').style.display = 'block';
            // Se tiver um banner específico para combos, exiba
            document.getElementById('banner-combos') && (document.getElementById('banner-combos').style.display = 'block');
        } else {
            document.getElementById('conteudo-' + tipo).style.display = 'block';
            document.getElementById('banner-' + tipo).style.display = 'block';
        }

        // Adiciona a classe active ao item do menu clicado
        event.currentTarget.classList.add('active');

        // Atualiza os filtros ativos
        document.querySelectorAll('.nav-categorias a').forEach(link => {
            link.classList.remove('active');
        });
        var nav = tipo === 'combos' ? document.querySelector('#conteudo-combos .nav-categorias a:first-child') : document.querySelector(`#conteudo-${tipo} .nav-categorias a:first-child`);
        nav && nav.classList.add('active');
    };
});