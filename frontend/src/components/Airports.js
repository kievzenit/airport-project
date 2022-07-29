import React, { useEffect, useState } from "react";

import Header from "./non-independent/Header";
import Modal from "./non-independent/Modal";

class Airports extends React.Component {
    constructor(props) {
        super(props);

        this.apiUrl = 'http://api.airportproject.com/airport/';

        this.setAirports = this.setAirports.bind(this);

        this.validateInput = this.validateInput.bind(this);
        this.validateInputOnChange = this.validateInputOnChange.bind(this);

        this.addDataToEditModal = this.addDataToEditModal.bind(this);
        this.saveDataFromModal = this.saveDataFromModal.bind(this);
        this.modalCloseHandler = this.modalCloseHandler.bind(this);

        this.addNewModalHandler = this.addNewModalHandler.bind(this);
        this.editModalHandler = this.editModalHandler.bind(this);

        this.addNewModalCloseHandler = this.addNewModalCloseHandler.bind(this);
        this.editModalCloseHandler = this.editModalCloseHandler.bind(this);

        this.deleteRow = this.deleteRow.bind(this);

        this.airports = [];
    }

    componentDidMount() {
        fetch(this.apiUrl + 'all')
            .then(res => res.json())
            .then(
                (result) => {
                    this.airports = result;
                    this.setAirports();
                },
                (error) => {
                    this.showError("Network error, try to reload this page");
                });
    }

    render() {
        return (
            <main>
                <Header activeLink="airports" />
                <br />

                <div className="container">
                    <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addNewAirportModal">Add new airport</button>
                    <br />
                    <br />
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Airport name</th>
                                <th scope="col">Country</th>
                                <th scope="col">City</th>
                                <th scope="col" style={{ width: "99px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                </div>

                <Modal id="addNewAirportModal" title="Add new airport" body={
                    (
                        <form className="input-group mb-3">
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="Airport name" />
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="Country" />
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="City" />
                        </form>
                    )
                } saveOnClick={this.addNewModalHandler} closeOnClick={this.addNewModalCloseHandler} />

                <Modal id="editAirportModal" title="Edit airport" body={
                    (
                        <form className="input-group mb-3">
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="Airport name" />
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="Country" />
                            <input type="text" className="form-control" onChange={this.validateInputOnChange} placeholder="City" />
                            <input type="hidden" />
                        </form>
                    )
                } saveOnClick={this.editModalHandler} closeOnClick={this.editModalCloseHandler} />

                <div className="toast-container bottom-0 end-0"></div>

                <div className="spinner-border" style={{
                    width: "3rem",
                    height: "3rem",
                    position: "absolute",
                    left: "calc(50% - 3rem)",
                    top: "calc(50% - 3rem)"
                }} role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>


            </main>
        );

    }

    addNewAirport(airport) {
        let tbody = document.querySelector('tbody');

        let tr = document.createElement('tr');

        let tdId = document.createElement('td');
        let tdName = document.createElement('td');
        let tdCountry = document.createElement('td');
        let tdCity = document.createElement('td');
        let tdControls = document.createElement('td');

        let buttonEdit = document.createElement('button');
        let buttonDelete = document.createElement('button');

        tr.id = `airport-${airport.id}`;

        tdId.innerText = airport.id;

        if (airport.id == undefined) {
            tdId.innerText = '';

            let spinner = document.createElement('div');
            let spinnerSpan = document.createElement('span');

            spinner.classList.add('spinner-grow', 'spinner-grow-sm');
            spinnerSpan.classList.add('visually-hidden');

            spinnerSpan.innerText = 'Loading...';

            spinner.setAttribute('role', 'status');

            spinner.appendChild(spinnerSpan);
            tdId.appendChild(spinner);
        }

        tdName.innerText = airport.name;
        tdCountry.innerText = airport.country;
        tdCity.innerText = airport.city;

        tdId.setAttribute('scope', 'row');
        tdName.setAttribute('scope', 'row');
        tdCountry.setAttribute('scope', 'row');
        tdCity.setAttribute('scope', 'row');
        tdControls.setAttribute('scope', 'row');

        buttonEdit.setAttribute('data-bs-toggle', 'modal');
        buttonEdit.setAttribute('data-bs-target', '#editAirportModal');

        tdControls.classList.add('btn-group', 'rounded-0');
        buttonEdit.classList.add('btn', 'btn-outline-primary', 'bi', 'bi-pencil-fill');
        buttonDelete.classList.add('btn', 'btn-outline-danger', 'bi', 'bi-trash3-fill');

        buttonEdit.addEventListener('click', this.addDataToEditModal);
        buttonDelete.addEventListener('click', this.deleteRow);

        tdControls.appendChild(buttonEdit);
        tdControls.appendChild(buttonDelete);

        tr.appendChild(tdId);
        tr.appendChild(tdName);
        tr.appendChild(tdCountry);
        tr.appendChild(tdCity);
        tr.appendChild(tdControls);

        tbody.appendChild(tr);
    }

    setAirports() {
        let tbody = document.querySelector('tbody');

        if (this.airports.length < 0) {
            return;
        }

        this.airports.map(a => {
            this.addNewAirport(a);

            let loadingElement = document.querySelector('.spinner-border');
            loadingElement.style.display = 'none';
        });
    }

    showError(message) {
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

        toasBody.innerText = message;

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

    validateInputOnChange(e) {
        this.validateInput(e.target);
    }

    validateInput(input, validateCallback, validateMessage) {
        let error = false;

        if (!input.value) {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');

            this.showError(`Field ${input.placeholder.toLowerCase()} must be not empty`)

            error = true;
        }

        if (!error && validateCallback && !validateCallback(input.value)) {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');

            this.showError(`${validateMessage}`)

            error = true;
        }

        if (!error) {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        }

        return error;
    }


    addDataToEditModal(e) {
        let tr = e.target.closest("tr");
        let id = tr.children[0].innerText;

        let name = tr.children[1].innerText;
        let country = tr.children[2].innerText;
        let city = tr.children[3].innerText;

        let editModal = document.getElementById('editAirportModal');
        let editModalInputs = editModal.querySelectorAll('input');

        let nameInput = editModalInputs[0];
        let countryInput = editModalInputs[1];
        let cityInput = editModalInputs[2];
        let idInput = editModalInputs[3];

        this.clearInputs(idInput, nameInput, countryInput, cityInput);

        idInput.value = id;
        nameInput.value = name;
        countryInput.value = country;
        cityInput.value = city;
    }
    saveDataFromModal(modalId) {
        let modalEl = document.getElementById(modalId);
        let modal = bootstrap.Modal.getInstance(modalEl);

        let inputs = modalEl.querySelector('.input-group').children;
        let nameInput = inputs[0];
        let countryInput = inputs[1];
        let cityInput = inputs[2];
        let idInput = inputs[3];

        let error = false;
        const setError = (e) => {
            if (!error && e) {
                error = true;
            }
        }

        setError(this.validateInput(nameInput));
        setError(this.validateInput(countryInput));
        setError(this.validateInput(cityInput));

        if (!this.invokedByEdit) {
            this.searchName(nameInput.value, () => {
                setError(true);
                this.showError('Airport with this name is already exist');
            });    
        }

        const returnObj = {
            id: idInput ? idInput.value : 0,
            name: nameInput.value,
            country: countryInput.value,
            city: cityInput.value,
            error
        };

        if (!error) {
            modal.hide();
            this.clearInputs(idInput, nameInput, countryInput, cityInput);
        }

        this.invokedByEdit = false;

        return returnObj;
    }
    modalCloseHandler(e) {
        let inputs = e.target.closest('.modal-content').querySelectorAll('input');
        let nameInput = inputs[0];
        let countryInput = inputs[1];
        let cityInput = inputs[2];


        this.clearInputs(null, nameInput, countryInput, cityInput);
    }

    editModalHandler(e) {
        this.invokedByEdit = true;
        let { id, name, country, city, error } = this.saveDataFromModal('editAirportModal');

        if (error) {
            return;
        }

        fetch(this.apiUrl, {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id,
                name,
                country,
                city
            })
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: {
                            let tr = document.getElementById(`airport-${id}`);
                            let tds = tr.children;

                            let tdId = tds[0];
                            let tdName = tds[1];
                            let tdCountry = tds[2];
                            let tdCity = tds[3];

                            tdId.innerText = id;
                            tdName.innerText = name;
                            tdCountry.innerText = country;
                            tdCity.innerText = city;

                            break;
                        }
                        case 404: this.showError(`Airport with id: ${id} cannot be found`); break;
                        case 500: this.showError("Server error, try again"); break;
                    }
                },
                (error) => {
                    this.showError("Cannot edit row, reason: network error. Try to reload this page");
                });
    }
    addNewModalHandler(e) {
        let { id, name, country, city, error } = this.saveDataFromModal('addNewAirportModal');

        if (error) {
            return;
        }

        this.addNewAirport({
            id: undefined,
            name,
            country,
            city
        });

        fetch(this.apiUrl, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id,
                name,
                country,
                city
            })
        })
            .then(res => {
                switch (res.status) {
                    case 200: return res.json();
                    case 500: this.showError("Server error, try again"); break;
                }
            }, error => this.showError('Cannot add row, reason: network error. Try to reload this page'))
            .then(result => {
                this.searchName(result.name, tr => {
                    tr.id = `airport-${result.id}`;

                    let tdId = tr.children[0];
                    tdId.innerText = result.id;
                });

            });
    }

    editModalCloseHandler(e) {
        this.modalCloseHandler(e);
    }
    addNewModalCloseHandler(e) {
        this.modalCloseHandler(e);
    }

    clearInputs(idInput, nameInput, countryInput, cityInput) {
        nameInput.classList.remove('is-valid');
        nameInput.classList.remove('is-invalid');
        countryInput.classList.remove('is-valid');
        countryInput.classList.remove('is-invalid');
        cityInput.classList.remove('is-valid');
        cityInput.classList.remove('is-invalid');

        if (idInput) {
            idInput.value = '';
        }

        nameInput.value = '';
        countryInput.value = '';
        cityInput.value = '';
    }

    searchName(name, foundCallback) {
        let trs = document.querySelectorAll('tbody tr');

        trs.forEach(tr => {
            let tdName = tr.children[1];

            if (tdName.innerText == name) {
                foundCallback(tr);
            }
        });
    }

    deleteRow(e) {
        let tr = e.target.closest("tr");
        let id = tr.children[0].innerText;

        if (!window.confirm('This operation cannot be undone')) {
            return;
        }

        tr.style.display = 'none';

        fetch(this.apiUrl, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            },
            body: id
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: tr.remove(); break;
                        case 404: {
                            tr.style.display = '';
                            this.showError(`Airport with id: ${id} cannot be found`);
                            break;
                        }
                        case 500: {
                            tr.style.display = '';
                            this.showError("Server error, try again"); 
                            break;}
                    }
                },
                (error) => {
                    tr.style.display = '';
                    this.showError("Cannot delete row, reason: network error. Try to reload this page");
                });
    }
}

export default Airports