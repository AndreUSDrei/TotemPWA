// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// PWA Enhancement Scripts

// Offline/Online indicator
function updateOnlineStatus() {
    const indicator = document.querySelector('.offline-indicator');
    if (!indicator) {
        const newIndicator = document.createElement('div');
        newIndicator.className = 'offline-indicator';
        newIndicator.textContent = 'Você está offline. Algumas funcionalidades podem não estar disponíveis.';
        document.body.appendChild(newIndicator);
    }
    
    if (!navigator.onLine) {
        document.querySelector('.offline-indicator').classList.add('show');
    } else {
        document.querySelector('.offline-indicator').classList.remove('show');
    }
}

// Add offline indicator to layout
window.addEventListener('online', updateOnlineStatus);
window.addEventListener('offline', updateOnlineStatus);
document.addEventListener('DOMContentLoaded', updateOnlineStatus);

// Prevent context menu on long press
document.addEventListener('contextmenu', function(e) {
    e.preventDefault();
});

// Add touch feedback
document.addEventListener('touchstart', function(e) {
    if (e.target.tagName === 'BUTTON' || e.target.closest('button')) {
        e.target.style.transform = 'scale(0.95)';
    }
});

document.addEventListener('touchend', function(e) {
    if (e.target.tagName === 'BUTTON' || e.target.closest('button')) {
        e.target.style.transform = 'scale(1)';
    }
});

// Prevent double tap zoom
let lastTouchEnd = 0;
document.addEventListener('touchend', function(event) {
    const now = (new Date()).getTime();
    if (now - lastTouchEnd <= 300) {
        event.preventDefault();
    }
    lastTouchEnd = now;
}, false);

// Add keyboard navigation support
document.addEventListener('keydown', function(e) {
    if (e.key === 'Enter' || e.key === ' ') {
        const activeElement = document.activeElement;
        if (activeElement && (activeElement.tagName === 'BUTTON' || activeElement.closest('button'))) {
            e.preventDefault();
            activeElement.click();
        }
    }
});

// --- Inatividade: resetar sistema após 60s sem interação ---
let inatividadeTimeout;
function resetarInatividade() {
    clearTimeout(inatividadeTimeout);
    inatividadeTimeout = setTimeout(() => {
        localStorage.removeItem('carrinhoItens');
        localStorage.removeItem('descontoPedido');
        localStorage.removeItem('cpfInformado');
        window.location.href = '/';
    }, 60000);
}
['click','touchstart','keydown','scroll'].forEach(evt => {
    document.addEventListener(evt, resetarInatividade, true);
});
resetarInatividade();
