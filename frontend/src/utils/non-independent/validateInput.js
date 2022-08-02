import showError from "./showError";

function validateInputOnChange(e) {
    validateInput(e.target);
}

function validateInput(input, validateCallback, validateMessage) {
    let error = false;

    if (!input.value) {
        input.classList.remove('is-valid');
        input.classList.add('is-invalid');

        showError(`Field ${input.placeholder.toLowerCase()} must be not empty`)

        error = true;
    }

    if (!error && validateCallback && !validateCallback(input.value)) {
        input.classList.remove('is-valid');
        input.classList.add('is-invalid');

        showError(`${validateMessage}`)

        error = true;
    }

    if (!error) {
        input.classList.remove('is-invalid');
        input.classList.add('is-valid');
    }

    return error;
}

export { validateInputOnChange, validateInput }