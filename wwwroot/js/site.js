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

// Add loading indicator for navigation
document.addEventListener('click', function(e) {
    if (e.target.tagName === 'A' || e.target.closest('a') || 
        e.target.tagName === 'BUTTON' || e.target.closest('button')) {
        
        const loading = document.querySelector('.loading');
        if (!loading) {
            const newLoading = document.createElement('div');
            newLoading.className = 'loading';
            newLoading.innerHTML = '<div>Carregando...</div>';
            document.body.appendChild(newLoading);
        }
        
        setTimeout(() => {
            document.querySelector('.loading').classList.add('show');
        }, 100);
    }
});

// Hide loading when page loads
window.addEventListener('load', function() {
    const loading = document.querySelector('.loading');
    if (loading) {
        loading.classList.remove('show');
    }
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
