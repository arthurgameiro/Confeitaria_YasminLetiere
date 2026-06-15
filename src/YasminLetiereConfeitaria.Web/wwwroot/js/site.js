// Mobile menu toggle e interações básicas
document.addEventListener('DOMContentLoaded', function() {
    const mobileMenuBtn = document.getElementById('mobile-menu-btn');
    const navMenu = document.getElementById('nav-menu');

    if (mobileMenuBtn && navMenu) {
        mobileMenuBtn.addEventListener('click', function() {
            navMenu.classList.toggle('active');
            
            // Alterna o ícone entre menu e fechar
            const icon = mobileMenuBtn.querySelector('.material-icons');
            if (icon) {
                if (icon.textContent === 'menu') {
                    icon.textContent = 'close';
                } else {
                    icon.textContent = 'menu';
                }
            }
        });
    }

    // Intercepta cliques nos links sazonais inativos
    const disabledSazonalLinks = document.querySelectorAll('.disabled-sazonal-link');
    disabledSazonalLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            showNoSeasonalAlert();
    });

    // Controle do Carrossel Sensorial
    const sensoryCarousel = document.getElementById('sensory-carousel');
    const sensoryPrev = document.getElementById('sensory-prev');
    const sensoryNext = document.getElementById('sensory-next');

    if (sensoryCarousel && sensoryPrev && sensoryNext) {
        sensoryPrev.addEventListener('click', function() {
            sensoryCarousel.scrollBy({
                left: -sensoryCarousel.offsetWidth * 0.6,
                behavior: 'smooth'
            });
        });

        sensoryNext.addEventListener('click', function() {
            sensoryCarousel.scrollBy({
                left: sensoryCarousel.offsetWidth * 0.6,
                behavior: 'smooth'
            });
        });
    }
});

// Exibe o Toast informativo público quando não há sazonalidades ativas
function showNoSeasonalAlert() {
    // Remove o toast anterior se houver
    let existingToast = document.getElementById('publicSazonalToast');
    if (existingToast) {
        clearTimeout(existingToast._timer);
        existingToast.remove();
    }

    // Cria o elemento do toast público
    const toast = document.createElement('div');
    toast.id = 'publicSazonalToast';
    toast.className = 'public-toast';
    toast.setAttribute('role', 'alert');
    toast.innerHTML = `
        <span class="material-icons toast-icon">info</span>
        <div class="toast-body">
            <div class="toast-title">Cardápio Indisponível</div>
            <div class="toast-msg">Não há nenhum cardápio sazonal ativo no momento. Fique de olho em nossas redes sociais para novidades!</div>
        </div>
        <button class="toast-close" aria-label="Fechar">✕</button>
    `;

    document.body.appendChild(toast);

    const closeBtn = toast.querySelector('.toast-close');
    closeBtn.addEventListener('click', function() {
        dismissPublicToast(toast);
    });

    // Desaparece automaticamente após 5 segundos
    toast._timer = setTimeout(() => {
        dismissPublicToast(toast);
    }, 5000);
}

function dismissPublicToast(toast) {
    if (!toast) return;
    clearTimeout(toast._timer);
    toast.classList.add('toast-hiding');
    toast.style.animation = 'toastSlideOutLeft 0.3s cubic-bezier(0.25, 0.8, 0.25, 1) forwards';
    toast.addEventListener('animationend', () => {
        toast.remove();
    }, { once: true });
}
