import React from "react";
import { useParams } from "react-router-dom";
import * as bootstrap from "bootstrap";

import HTTPRequestManager from "../utils/HttpRequestManager";
import createPagination from "../utils/non-independent/createPagination";
import showError from "../utils/non-independent/showError";
import { validateInput, validateInputOnChange } from "../utils/non-independent/validateInput";
import clearInputs from "../utils/non-independent/clearInputs";

import Header from "./non-independent/Header";
import Modal from "./non-independent/Modal";

class Airports extends React.Component {
    constructor(props) {
        super(props);

        this.apiUrl = 'http://api.airportproject.com/airport/';

        this.requestManager = new HTTPRequestManager();

        this.setAirports = this.setAirports.bind(this);

        this.addDataToEditModal = this.addDataToEditModal.bind(this);
        this.saveDataFromModal = this.saveDataFromModal.bind(this);
        this.modalCloseHandler = this.modalCloseHandler.bind(this);

        this.addNewModalHandler = this.addNewModalHandler.bind(this);
        this.editModalHandler = this.editModalHandler.bind(this);

        this.addNewModalCloseHandler = this.addNewModalCloseHandler.bind(this);
        this.editModalCloseHandler = this.editModalCloseHandler.bind(this);

        this.deleteRow = this.deleteRow.bind(this);

        this.pageNumber = 0;
        this.pagesCount = 1;
        this.currentPageItems = 0;

        this.airports = [];
    }

    componentDidMount() {
        this.pageNumber = this.props.params.page == undefined ? '0' : this.props.params.page;

        if (!this.pageNumber.isPositiveInteger()) {
            this.requestManager.GET(this.apiUrl + 1,
                (response) => {
                    response = JSON.parse(response);

                    this.pageNumber = 1
                    this.totalAirportsCount = response.totalCount;
                    this.airports = response.items;
                    this.setAirports();
                    createPagination(this.totalAirportsCount, this.pageNumber, 'airports');
                },
                () => showError("Network error, try to reload this page")
            );

            return;
        }

        this.requestManager.GET(this.apiUrl + this.pageNumber,
            (response) => {
                response = JSON.parse(response);
                    
                this.totalAirportsCount = response.totalCount;
                this.airports = response.items;
                this.setAirports();
                createPagination(this.totalAirportsCount, this.pageNumber, 'airports');
            },
            () => showError("Network error, try to reload this page")
        );
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
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="Airport name" />
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="Country" />
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="City" />
                        </form>
                    )
                } saveOnClick={this.addNewModalHandler} closeOnClick={this.addNewModalCloseHandler} />

                <Modal id="editAirportModal" title="Edit airport" body={
                    (
                        <form className="input-group mb-3">
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="Airport name" />
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="Country" />
                            <input type="text" className="form-control" onChange={validateInputOnChange} placeholder="City" />
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
        this.currentPageItems += 1;

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
        if (this.airports.length < 0) {
            return;
        }

        this.airports.map(a => {
            this.addNewAirport(a);

            let loadingElement = document.querySelector('.spinner-border');
            loadingElement.style.display = 'none';
        });
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

        clearInputs([idInput, nameInput, countryInput, cityInput]);

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

        setError(validateInput(nameInput));
        setError(validateInput(countryInput));
        setError(validateInput(cityInput));

        if (!this.invokedByEdit) {
            this.searchName(nameInput.value, () => {
                setError(true);
                showError('Airport with this name is already exist');
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
            document.querySelector('.modal-backdrop').remove();
            clearInputs([idInput, nameInput, countryInput, cityInput]);
        }

        this.invokedByEdit = false;

        return returnObj;
    }
    modalCloseHandler(e) {
        let inputs = e.target.closest('.modal-content').querySelectorAll('input');
        let nameInput = inputs[0];
        let countryInput = inputs[1];
        let cityInput = inputs[2];


        clearInputs([nameInput, countryInput, cityInput]);
    }

    editModalHandler(e) {
        this.invokedByEdit = true;
        let { id, name, country, city, error } = this.saveDataFromModal('editAirportModal');

        if (error) {
            return;
        }

        this.requestManager.PUT(this.apiUrl,
            (response, status) => {
                switch (status) {
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
                    case 404: showError(`Airport with id: ${id} cannot be found`); break;
                    case 500: showError("Server error, try again"); break;
                }
            },
            () => showError("Cannot edit row, reason: network error. Try to reload this page"),
            true,
            JSON.stringify({
                id,
                name,
                country,
                city
            })
        );
    }
    addNewModalHandler(e) {
        let { id, name, country, city, error } = this.saveDataFromModal('addNewAirportModal');

        if (error) {
            return;
        }

        let tr;

        this.totalAirportsCount  += 1;

        if (this.currentPageItems == 6) {
            createPagination(this.totalAirportsCount, this.pageNumber, 'airports');
        } else {
            tr = this.addNewAirport({
                id: undefined,
                name,
                country,
                city
            });
        }

        this.requestManager.POST(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: {
                        response = JSON.parse(response);

                        this.searchName(response.name, tr => {
                            tr.id = `airport-${response.id}`;
        
                            let tdId = tr.children[0];
                            tdId.innerText = response.id;
                        });

                        break;
                    }
                    case 500: showError("Server error, try again"); break;
                }
            },
            () => { 
                showError("Cannot add row, reason: network error. Try to reload this page"); 
                tr.remove() 
            },
            true,
            JSON.stringify({
                id,
                name,
                country,
                city
            })
        );
    }

    editModalCloseHandler(e) {
        this.modalCloseHandler(e);
    }
    addNewModalCloseHandler(e) {
        this.modalCloseHandler(e);
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

        this.requestManager.DELETE(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: tr.remove(); break;
                    case 404: {
                        tr.style.display = '';
                        showError(`Airport with id: ${id} cannot be found`);
                        break;
                    }
                    case 500: {
                        tr.style.display = '';
                        showError("Server error, try again"); 
                        break;
                    }
                }
            },
            () => {
                tr.style.display = '';
                showError("Cannot delete row, reason: network error. Try to reload this page");
            },
            true,
            id
        );
    }
}

export default (props) => (
    <Airports
        {...props}
        params={useParams()}
    />
);