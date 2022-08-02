export default function showError(errorMessage) {
    let container = document.querySelector('.toast-container');

    let toast = document.createElement('div');
    let flexContainer = document.createElement('div');
    let toasBody = document.createElement('div');
    let closeButton = document.createElement('button');

    toast.classList.add('toast', 'align-items-center', 'fade', 'text-bg-danger', 'show');
    toast.style.marginBottom = '0.3rem';

    flexContainer.classList.add('d-flex');
    toasBody.classList.add('toast-body');
    closeButton.classList.add('btn-close', 'btn-close-white', 'me-2', 'm-auto');

    toasBody.innerText = errorMessage;

    flexContainer.appendChild(toasBody);
    flexContainer.appendChild(closeButton);
    toast.appendChild(flexContainer);
    container.appendChild(toast);

    let onCloseClick = () => {
        toast.remove();
    }

    closeButton.addEventListener('click', onCloseClick);

    setTimeout(() => onCloseClick(), 10000);
}