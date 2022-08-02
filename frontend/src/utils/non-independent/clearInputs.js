export default function clearInputs(inputs) {
    inputs.map(input => {
        input.classList.remove('is-valid');
        input.classList.remove('is-invalid');
        input.value = '';
    });
}