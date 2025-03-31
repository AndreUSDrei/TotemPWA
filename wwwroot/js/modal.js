const openModalBtn = document.getElementById('botaoCarrinho');
const closeModalBtn = document.getElementById('fecharModal');
const modal = document.getElementById('modalCarrinho');    
openModalBtn.addEventListener('click', () => {
    modal.classList.add('active'); 
});
closeModalBtn.addEventListener('click', () => {
    modal.classList.remove('active');
});