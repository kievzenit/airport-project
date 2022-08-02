export default function clearInputs(inputs) {
    inputs.forEach(input => {
        if (input) {
            input.classList.remove('is-valid');
            input.classList.remove('is-invalid');
            input.value = '';
        }
    });
}