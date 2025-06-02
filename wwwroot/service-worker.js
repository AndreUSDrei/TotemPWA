const CACHE_NAME = 'totem-cache-v1';
const urlsToCache = [
  '/',
  '/css/tela2.css',
  '/js/modal.js',
  // Adicione outros arquivos essenciais aqui
];

self.addEventListener('install', event => {
  event.waitUntil(
    caches.open(CACHE_NAME)
      .then(cache => cache.addAll(urlsToCache))
  );
});

self.addEventListener('fetch', event => {
  event.respondWith(
    caches.match(event.request)
      .then(response => response || fetch(event.request))
  );
}); 