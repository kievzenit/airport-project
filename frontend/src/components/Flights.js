import React from "react";
import { useParams } from "react-router-dom";
import * as bootstrap from "bootstrap";

import HTTPRequestManager from "../utils/HttpRequestManager";
import showError from "../utils/non-independent/showError";
import createPagination from "../utils/non-independent/createPagination";
import { validateInput } from "../utils/non-independent/validateInput";
import clearInputs from "../utils/non-independent/clearInputs";

import Header from "./non-independent/Header";
import Modal from "./non-independent/Modal";

class Flights extends React.Component {
    constructor(props) {
        super(props);

        this.apiUrl = 'http://api.airportproject.com/flight/';

        this.search = this.search.bind(this);
        this.openSearch = this.openSearch.bind(this);
        this.closeSearch = this.closeSearch.bind(this);

        this.validateInputOnChange = this.validateInputOnChange.bind(this);

        this.addNewFlight = this.addNewFlight.bind(this);

        this.addDataToEditModal = this.addDataToEditModal.bind(this);
        this.saveDataFromModal = this.saveDataFromModal.bind(this);
        this.modalCloseHandler = this.modalCloseHandler.bind(this);

        this.addNewModalHandler = this.addNewModalHandler.bind(this);
        this.editModalHandler = this.editModalHandler.bind(this);

        this.addNewModalCloseHandler = this.addNewModalCloseHandler.bind(this);
        this.editModalCloseHandler = this.editModalCloseHandler.bind(this);

        this.deleteRow = this.deleteRow.bind(this);

        this.setFlightByTDs = this.setFlightByTDs.bind(this);

        this.isInvokedBySearch = false;

        this.pageNumber = 0;
        this.pagesCount = 1;
        this.currentPageItems = 0;

        this.requestManager = new HTTPRequestManager();

        this.flights = [];
    }

    componentDidMount() {
        this.pageNumber = this.props.params.page == undefined ? '0' : this.props.params.page;

        if (!this.pageNumber.isPositiveInteger()) {
            this.requestManager.GET(this.apiUrl + 1,
                (response, status) => {
                    response = JSON.parse(response);

                    this.pageNumber = 1
                    this.totalFlightsCount = response.totalCount;
                    this.flights = response.items;
                    this.setFlights();
                    createPagination(this.totalFlightsCount, this.pageNumber, 'flights');
                },
                () => showError("Network error, try to reload this page")
            );            
            
            return;
        }

        this.requestManager.GET(this.apiUrl + this.pageNumber,
            (response, status) => {
                response = JSON.parse(response);

                this.totalFlightsCount = response.totalCount;
                this.flights = response.items;
                this.setFlights();
                createPagination(this.totalFlightsCount, this.pageNumber, 'flights');
            },
            () => showError("Network error, try to reload this page")
        );
    }

    render() {
        return (
            <main>
                <Header activeLink="flights" />
                <br />

                <div className="container">
                    <div className="d-flex justify-content-between">
                        <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addNewFlightModal">Add new flight</button>
                        <button type="button" className="btn btn-dark" onClick={this.openSearch}>Search</button>
                    </div>
                    
                    <br />
                    <br />
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">From</th>
                                <th scope="col">To</th>
                                <th scope="col">Terminal</th>
                                <th scope="col">Departure time</th>
                                <th scope="col">Arrival time</th>
                                <th scope="col">Economy</th>
                                <th scope="col">Business</th>
                                <th scope="col">Status</th>
                                <th scope="col" style={{ width: "99px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>

                <div className="container" id="flights-found-table" style={{
                    display: 'none'
                }}>
                    <div className="d-flex justify-content-between">
                        <button type="button" className="btn btn-danger" onClick={this.closeSearch}>Close search</button>

                        <div className="btn-group">
                            <div className="dropdown" id="dropdown">
                                <button className="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" style={{
                                    borderTopRightRadius: 0,
                                    borderBottomRightRadius: 0
                                }}>
                                    Search by flight number
                                </button>
                                <ul className="dropdown-menu">
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by flight number</button></li>
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by arrival airport</button></li>
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by departure airport</button></li>
                                </ul>
                            </div>

                            <input type="text" className="form-control" placeholder="Flight number" onChange={this.validateInputOnChange} style={{
                                borderRadius: 0
                            }}></input>
                            <button className="btn btn-secondary" onClick={this.search} style={{
                                borderTopLeftRadius: 0,
                                borderBottomLeftRadius: 0
                            }}><i className="bi bi-search"></i></button>
                        </div>
                    </div>
                    <br />
                    <br />
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">From</th>
                                <th scope="col">To</th>
                                <th scope="col">Terminal</th>
                                <th scope="col">Departure time</th>
                                <th scope="col">Arrival time</th>
                                <th scope="col">Economy</th>
                                <th scope="col">Business</th>
                                <th scope="col">Status</th>
                                <th scope="col" style={{ width: "99px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>

                <Modal id="addNewFlightModal" title="Add new flight" body={
                    (
                        <form className="row g-3">
                            <div className="col-md-5">
                                <label htmlFor="departure-airport-input" className="form-label">Departure airport</label>
                                <input type="text" className="form-control" id="departure-airport-input" placeholder="departure airport" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-5">
                                <label htmlFor="arrival-airport-input" className="form-label">Arrival airport</label>
                                <input type="text" className="form-control" id="arrival-airport-input" placeholder="arrival airport" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-2">
                                <label htmlFor="terminal-input" className="form-label">Terminal</label>
                                <input type="text" className="form-control" id="terminal-input" onChange={this.validateInputOnChange} required />
                            </div>
                            
                            <div className="col-md-6">
                                <label htmlFor="departure-date-input" className="form-label">Departure date</label>
                                <input type="datetime-local" className="form-control" id="departure-date-input" placeholder="departure date" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="arrival-date-input" className="form-label">Arrival date</label>
                                <input type="datetime-local" className="form-control" id="arrival-date-input" placeholder="arrival date" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="input-group">
                                <span className="input-group-text">$</span>
                                <input id="economy-input" type="text" className="form-control" placeholder="economy price" aria-label="Amount (to the nearest dollar)" onChange={this.validateInputOnChange} />
                                <span className="input-group-text">$</span>
                                <input id="business-input" type="text" className="form-control" placeholder="business price" aria-label="Amount (to the nearest dollar)" onChange={this.validateInputOnChange} />
                            </div>
                            <div className="col-md-4">
                                <label htmlFor="status-input" className="form-label">Status</label>
                                <select className="form-select" name='Status' id="status-input" status-id='' defaultValue={''} onChange={this.validateInputOnChange} required>
                                    <option value={''} disabled>Choose...</option>
                                    <option value={'normal'} onClick={this.selectedStatus}>Normal</option>
                                    <option value={'delayed'} onClick={this.selectedStatus}>Delayed</option>
                                    <option value={'canceled'} onClick={this.selectedStatus}>Canceled</option>
                                </select>
                            </div>
                        </form>
                    )
                } saveOnClick={this.addNewModalHandler} closeOnClick={this.addNewModalCloseHandler} />

                <Modal id="editFlightModal" title="Edit flight" body={
                    (
                        <form className="row g-3">
                            <div className="col-md-5">
                                <label htmlFor="departure-airport-input" className="form-label">Departure airport</label>
                                <input type="text" className="form-control" id="departure-airport-input" placeholder="departure airport" disabled readOnly required />
                            </div>
                            <div className="col-md-5">
                                <label htmlFor="arrival-airport-input" className="form-label">Arrival airport</label>
                                <input type="text" className="form-control" id="arrival-airport-input" placeholder="arrival airport" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-2">
                                <label htmlFor="terminal-input" className="form-label">Terminal</label>
                                <input type="text" className="form-control" id="terminal-input" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="col-md-6">
                                <label htmlFor="departure-date-input" className="form-label">Departure date</label>
                                <input type="datetime-local" className="form-control" id="departure-date-input" placeholder="departure date" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="arrival-date-input" className="form-label">Arrival date</label>
                                <input type="datetime-local" className="form-control" id="arrival-date-input" placeholder="arrival date" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="input-group">
                                <span className="input-group-text">$</span>
                                <input id="economy-input" type="text" className="form-control" placeholder="economy price" aria-label="Amount (to the nearest dollar)" onChange={this.validateInputOnChange} />
                                <span className="input-group-text">$</span>
                                <input id="business-input" type="text" className="form-control" placeholder="business price" aria-label="Amount (to the nearest dollar)" onChange={this.validateInputOnChange} />
                            </div>
                            <div className="col-md-4">
                                <label htmlFor="status-input" className="form-label">Status</label>
                                <select className="form-select" name='Status' id="status-input" status-id='' defaultValue={''} onChange={this.validateInputOnChange} required>
                                    <option value={''} disabled>Current...</option>
                                    <option value={'normal'} onClick={this.selectedStatus}>Normal</option>
                                    <option value={'delayed'} onClick={this.selectedStatus}>Delayed</option>
                                    <option value={'canceled'} onClick={this.selectedStatus}>Canceled</option>
                                </select>
                            </div>

                            <input id="id-input" type="hidden" />
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

    search() {
        let input = document.querySelector('#flights-found-table .form-control');
        let tbody = document.querySelector('#flights-found-table tbody');

        let searchType = input.placeholder;
        let searchValue = input.value;
        
        tbody.innerText = '';

        switch (searchType) {
            case 'Flight number': {
                if (validateInput(input, (flightNumber) => {
                    return flightNumber > 0;
                }, 'Flight number must be not less or equal to zero')) { return; }

                this.requestManager.GET(this.apiUrl + `search/byFlightId/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);
                                this.addNewFlight(response, tbody);
                                break;
                            }
                            case 404: showError(`Cannot find flight with number: ${searchValue}`); break;
                            case 500: showError('Server error, please, contact administrator'); break;
                        }
                    },
                    () => showError("Cannot search, reason: network error. Try to reload this page")
                );

                break;
            }
            case 'Arrival airport': {
                if (validateInput(input, (arrivalAirport) => {
                    return arrivalAirport.length <= 50;
                }, "Arrival airport length must be not grater than 50")) { return; }

                this.requestManager.GET(this.apiUrl + `search/byFlightArrivalAirport/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);

                                if (response.length == 0) {
                                    showError(`Cannot find flight with arrival airport: ${searchValue}`);
                                    break;
                                }
        
                                response.map(f => {
                                    this.addNewFlight(f, tbody);
                                });

                                break;
                            }
                            case 500: showError('Server error, please, contact administrator'); break;
                        }
                    },
                    () => showError("Cannot search, reason: network error. Try to reload this page")
                );

                break;
            }
            case 'Departure airport': {
                if (validateInput(input, (departureAirport) => {
                    return departureAirport.length <= 50;
                }, "Departure airport length must be not grater than 50")) { return; }

                this.requestManager.GET(this.apiUrl + `search/byFlightDepartureAirport/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);

                                if (response.length == 0) {
                                    showError(`Cannot find flight with departure airport: ${searchValue}`);
                                }
        
                                response.map(f => {
                                    this.addNewFlight(f, tbody);
                                });

                                break;
                            }
                            case 500: showError('Server error, please, contact administrator'); break;
                        }
                    },
                    () => showError("Cannot search, reason: network error. Try to reload this page")
                );

                break;
            }
            default: break;
        }

        input.classList.remove('is-valid');
        input.classList.remove('is-invalid');
        input.value = '';
    }

    setPlaceholderForSearch(e) {
        let item = e.target;

        let itemText = item.innerText;
        let placeholder = itemText.split(' ').slice(2, 4).join(' ').capitalize();

        let input = document.querySelector('#flights-found-table .form-control');
        let dropdownToggle = document.querySelector('#flights-found-table .dropdown-toggle');

        dropdownToggle.innerText = itemText;
        input.placeholder = placeholder;
    }

    openSearch() {
        let containers = document.querySelectorAll('.container');

        let passengersContainer = containers[0];
        let searchContainer = containers[1];

        passengersContainer.style.display = 'none';
        searchContainer.style.display = '';

        this.isInvokedBySearch = true;
    }

    closeSearch() {
        let containers = document.querySelectorAll('.container');

        let passengersContainer = containers[0];
        let searchContainer = containers[1];

        passengersContainer.style.display = '';
        searchContainer.style.display = 'none';

        let tbody = searchContainer.querySelector('tbody');
        tbody.innerText = '';

        this.isInvokedBySearch = false;
    }

    addNewFlight(flight, tbody = null) {
        if (tbody == null) {
            tbody = document.querySelector('tbody');
            this.currentPageItems += 1;
        }

        let tr = document.createElement('tr');

        let tdId = document.createElement('td');
        let tdDepartureAirport = document.createElement('td');
        let tdArrivalAirport = document.createElement('td');
        let tdTerminal = document.createElement('td');
        let tdDepartureTime = document.createElement('td');
        let tdArrivalTime = document.createElement('td');
        let tdEconomyPrice = document.createElement('td');
        let tdBusinessPrice = document.createElement('td');
        let tdStatus = document.createElement('td');
        let tdControls = document.createElement('td');

        let buttonEdit = document.createElement('button');
        let buttonDelete = document.createElement('button');

        tr.id = `flight-${flight.id}`;

        tdId.innerText = flight.id;

        if (flight.id == undefined) {
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

        tdDepartureAirport.innerText = flight.departureAirportName;
        tdArrivalAirport.innerText = flight.arrivalAirportName;
        tdTerminal.innerText = flight.terminal;
        tdDepartureTime.innerText = this.convertISOToDate(flight.departureTime);
        tdArrivalTime.innerText = this.convertISOToDate(flight.arrivalTime);
        tdEconomyPrice.innerText = flight.economyPrice + '$';
        tdBusinessPrice.innerText = flight.businessPrice + '$';
        tdStatus.innerText = flight.status;

        tdDepartureAirport.setAttribute('scope', 'row');
        tdArrivalAirport.setAttribute('scope', 'row');
        tdTerminal.setAttribute('scope', 'row');
        tdDepartureTime.setAttribute('scope', 'row');
        tdArrivalTime.setAttribute('scope', 'row');
        tdEconomyPrice.setAttribute('scope', 'row');
        tdBusinessPrice.setAttribute('scope', 'row');
        tdStatus.setAttribute('scope', 'row');
        tdControls.setAttribute('scope', 'row');

        buttonEdit.setAttribute('data-bs-toggle', 'modal');
        buttonEdit.setAttribute('data-bs-target', '#editFlightModal');

        tdControls.classList.add('btn-group', 'rounded-0');
        buttonEdit.classList.add('btn', 'btn-outline-primary', 'bi', 'bi-pencil-fill');
        buttonDelete.classList.add('btn', 'btn-outline-danger', 'bi', 'bi-trash3-fill');

        buttonEdit.addEventListener('click', this.addDataToEditModal);
        buttonDelete.addEventListener('click', this.deleteRow);

        tdControls.appendChild(buttonEdit);
        tdControls.appendChild(buttonDelete);

        tr.appendChild(tdId);
        tr.appendChild(tdDepartureAirport);
        tr.appendChild(tdArrivalAirport);
        tr.appendChild(tdTerminal);
        tr.appendChild(tdDepartureTime);
        tr.appendChild(tdArrivalTime);
        tr.appendChild(tdEconomyPrice);
        tr.appendChild(tdBusinessPrice);
        tr.appendChild(tdStatus);
        tr.appendChild(tdControls);

        tbody.appendChild(tr);

        return tr;
    }

    setFlights() {
        if (this.flights.length < 0) {
            return;
        }

        this.flights.map(f => {
            this.addNewFlight(f);

            let loadingElement = document.querySelector('.spinner-border');
            loadingElement.style.display = 'none';
        });
    }

    validateInputOnChange(e) {
        if (e.target.id == 'birthday-input') {
            validateInput(e.target, (birthday) => {
                return Date.now() >= Date.parse(birthday);
            }, 'Birthday must be not day in the future');

            return;
        }

        validateInput(e.target);
    }

    selectedStatus(e) {
        let value = e.target.value;
        let selectTag = e.target.closest('select');

        selectTag.setAttribute('status-id', value);
    }


    addDataToEditModal(e) {
        let tr = e.target.closest("tr");

        let id = tr.children[0].innerText;
        let arrivalAirport = tr.children[2].innerText;
        let departureAirport = tr.children[1].innerText;
        let terminal = tr.children[3].innerText;
        let arrivalTime = tr.children[5].innerText;
        let departureTime = tr.children[4].innerText;
        let economyPrice = tr.children[6].innerText;
        let businessPrice = tr.children[7].innerText;
        let status = tr.children[8].innerText;

        let modalId = 'editFlightModal';

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let arrivalAirportInput = document.querySelector(`#${modalId} #arrival-airport-input`);
        let departureAirportInput = document.querySelector(`#${modalId} #departure-airport-input`);
        let terminalInput = document.querySelector(`#${modalId} #terminal-input`);
        let arrivalDateInput = document.querySelector(`#${modalId} #arrival-date-input`);
        let departureDateInput = document.querySelector(`#${modalId} #departure-date-input`);
        let economyPriceInput = document.querySelector(`#${modalId} #economy-input`);
        let businessPriceInput = document.querySelector(`#${modalId} #business-input`);
        let statusInput = document.querySelector(`#${modalId} #status-input`);

        idInput.value = id;
        arrivalAirportInput.value = arrivalAirport;
        departureAirportInput.value = departureAirport;
        terminalInput.value = terminal;
        arrivalDateInput.setAttribute('arrival-date', arrivalTime);
        departureDateInput.setAttribute('departure-date', departureTime);
        economyPriceInput.value = economyPrice.replace('$', '');
        businessPriceInput.value = businessPrice.replace('$', '');

        statusInput.setAttribute('status-id', status.toLowerCase());
    }
    saveDataFromModal(modalId) {
        let modalEl = document.getElementById(modalId);
        let modal = bootstrap.Modal.getInstance(modalEl);

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let arrivalAirportInput = document.querySelector(`#${modalId} #arrival-airport-input`);
        let departureAirportInput = document.querySelector(`#${modalId} #departure-airport-input`);
        let terminalInput = document.querySelector(`#${modalId} #terminal-input`);
        let arrivalDateInput = document.querySelector(`#${modalId} #arrival-date-input`);
        let departureDateInput = document.querySelector(`#${modalId} #departure-date-input`);
        let economyPriceInput = document.querySelector(`#${modalId} #economy-input`);
        let businessPriceInput = document.querySelector(`#${modalId} #business-input`);
        let statusInput = document.querySelector(`#${modalId} #status-input`);

        let statusId = statusInput.getAttribute('status-id');

        let error = false;
        const setError = (e, onErrorCallback) => {
            if (!error && e) {
                error = true;
            }

            if (error && e && onErrorCallback) {
                onErrorCallback();
            }
        }

        setError(validateInput(arrivalAirportInput));
        setError(validateInput(departureAirportInput));
        setError(arrivalAirportInput.value == departureAirportInput.value, () => showError("Arrival and departure airports can't be the same"));
        setError(validateInput(terminalInput, (terminal) => {return terminal.length == 1}, 'Terminal could have only one character'));

        if (this.invokedByAddNewFlight) {
            setError(validateInput(arrivalDateInput));
            setError(validateInput(departureDateInput));
        }

        if (!this.invokedByAddNewFlight && arrivalDateInput.value && departureDateInput.value) {
            let arrivalDate = new Date(arrivalDateInput.value);
            let departureDate = new Date(departureDateInput.value);

            setError(arrivalDate <= departureDate, () => showError("Departure date must be less than arrival and not equal"));
        }

        if (this.invokedByAddNewFlight) {
            let arrivalDate = new Date(arrivalDateInput.value);
            let departureDate = new Date(departureDateInput.value);

            setError(arrivalDate <= departureDate, () => showError("Departure date must be less than arrival and not equal"));
        }
        
        setError(validateInput(economyPriceInput, (price) => {return price >= 100 && price <= 100000}, "Economy ticket price must be in rage: from 100$ to 100000$"));
        setError(validateInput(businessPriceInput, (price) => {return price >= 100 && price <= 100000}, "Business ticket price must be in rage: from 100$ to 100000$"));

        if (statusId == '') {
            statusInput.classList.remove('is-valid');
            statusInput.classList.add('is-invalid');

            showError('Status must be selected');

            setError(true);
        }

        let arrivalTime = arrivalDateInput.value;
        let departureTime = departureDateInput.value;

        this.arrivalTimeChanged = true;
        this.departureTimeChanged = true;

        if (arrivalDateInput.value == '') {
            arrivalTime = arrivalDateInput.getAttribute('arrival-date');
            this.arrivalTimeChanged = false;
        }

        if (departureDateInput.value == '') {
            departureTime = departureDateInput.getAttribute('departure-date');
            this.departureTimeChanged = false;
        }

        const returnObj = {
            id: idInput ? idInput.value : undefined,
            arrivalAirport: arrivalAirportInput.value,
            departureAirport: departureAirportInput.value,
            terminal: terminalInput.value,
            arrivalTime,
            departureTime,
            economyPrice: economyPriceInput.value,
            businessPrice: businessPriceInput.value,
            status: statusId,
            error
        };

        if (!error) {
            modal.hide();
            document.querySelector('.modal-backdrop').remove();

            statusInput.classList.remove('is-valid');
            statusInput.classList.remove('is-invalid');

            statusInput.setAttribute('status-id', '');

            clearInputs([idInput, arrivalAirportInput, departureAirportInput, terminalInput, arrivalDateInput, departureDateInput, economyPriceInput, businessPriceInput]);
        }

        this.invokedByAddNewFlight = false;

        return returnObj;
    }
    modalCloseHandler(e) {
        let modalId = e.target.closest('.modal').id;
        let idInput = document.querySelector(`#${modalId} #id-input`);
        let arrivalAirportInput = document.querySelector(`#${modalId} #arrival-airport-input`);
        let departureAirportInput = document.querySelector(`#${modalId} #departure-airport-input`);
        let terminalInput = document.querySelector(`#${modalId} #terminal-input`);
        let arrivalDateInput = document.querySelector(`#${modalId} #arrival-date-input`);
        let departureDateInput = document.querySelector(`#${modalId} #departure-date-input`);
        let economyPriceInput = document.querySelector(`#${modalId} #economy-input`);
        let businessPriceInput = document.querySelector(`#${modalId} #business-input`);
        let statusInput = document.querySelector(`#${modalId} #status-input`);

        statusInput.classList.remove('is-valid');
        statusInput.classList.remove('is-invalid');

        statusInput.setAttribute('status-id', '');

        clearInputs([idInput, arrivalAirportInput, departureAirportInput, terminalInput, arrivalDateInput, departureDateInput, economyPriceInput, businessPriceInput]);
    }

    convertDateToISO(inputDate) {
        let splitted = inputDate.split(' ');
        let date = splitted[0].split('.');
        let time = splitted[1].split(':').slice(0, 2);

        let year = date[2];
        let month = date[1];
        let day = date[0];

        let hours = time[0];
        let minutes = time[1];

        return `${year}-${month}-${day}T${hours}:${minutes}:00Z`;
    }

    convertISOToDate(isoDate) {
        let splitted = isoDate.split('T');
        let date = splitted[0].split('-');
        let time = splitted[1].split(':').slice(0, 2);

        let year = date[0];
        let month = date[1];
        let day = date[2];

        let hours = time[0];
        let minutes = time[1];

        return `${day}.${month}.${year} ${hours}:${minutes}`;
    }

    editModalHandler(e) {
        let { id, arrivalAirport, departureAirport, terminal, arrivalTime, departureTime, economyPrice, businessPrice, status, error } = this.saveDataFromModal('editFlightModal');

        if (error) {
            return;
        }

        this.requestManager.PUT(this.apiUrl,
            (response, reqStatus) => {
                switch (reqStatus) {
                    case 200: {
                        let tr = document.getElementById(`flight-${id}`);
                        let tds = tr.children;

                        this.setFlightByTDs(tds, id, departureAirport, arrivalAirport, terminal, departureTime, arrivalTime, economyPrice, businessPrice, status);

                        if (this.isInvokedBySearch) {
                            tr = document.querySelectorAll(`#flight-${id}`)[1];
                            tds = tr.children;

                            this.setFlightByTDs(tds, id, departureAirport, arrivalAirport, terminal, departureTime, arrivalTime, economyPrice, businessPrice, status);
                        }

                        break;
                    }
                    case 404: showError(`Flight with id: ${id} cannot be found`); break;
                    case 500: showError("Server error, try again"); break;
                }
            },
            () => showError("Cannot edit row, reason: network error. Try to reload this page"),
            true,
            JSON.stringify({
                id,
                arrivalAirportName: arrivalAirport,
                departureAirportName: departureAirport,
                terminal,
                arrivalTime: this.arrivalTimeChanged ? arrivalTime : this.convertDateToISO(arrivalTime), //FIXME: internationalization bug
                departureTime: this.departureTimeChanged ? departureTime : this.convertDateToISO(departureTime),
                economyPrice,
                businessPrice,
                status
            })
        );
    }
    addNewModalHandler(e) {
        this.invokedByAddNewFlight = true;
        let { id, arrivalAirport, departureAirport, terminal, arrivalTime, departureTime, economyPrice, businessPrice, status, error } = this.saveDataFromModal('addNewFlightModal');

        if (error) {
            return;
        }

        let tr;

        this.totalFlightsCount += 1;

        if (this.currentPageItems == 6) {
            createPagination(this.totalFlightsCount, this.pageNumber, 'flights');
        } else {
            tr = this.addNewFlight({
                id: undefined,
                arrivalAirportName: arrivalAirport,
                departureAirportName: departureAirport,
                terminal,
                arrivalTime,
                departureTime,
                economyPrice,
                businessPrice,
                status
            });
        }

        this.requestManager.POST(this.apiUrl,
            (response, reqStatus) => {
                switch (reqStatus) {
                    case 200: {
                        response = JSON.parse(response);
                        tr.id = `flight-${response.id}`;

                        let tdId = tr.children[0];
                        tdId.innerText = response.id;
                        break;
                    }
                    case 500: showError("Server error, try again"); break;
                }
            },
            () => {
                tr.remove();
                showError('Cannot add row, reason: network error. Try to reload this page')
            },
            true,
            JSON.stringify({
                id,
                arrivalAirportName: arrivalAirport,
                departureAirportName: departureAirport,
                terminal,
                arrivalTime,
                departureTime,
                economyPrice, 
                businessPrice,
                status
            })
        );
    }

    setFlightByTDs(tds, id, departureAirport, arrivalAirport, terminal, departureTime, arrivalTime, economyPrice, businessPrice, status) {
        let tdId = tds[0];
        let tdDepartureAirport = tds[1];
        let tdArrivalAirport = tds[2];
        let tdTerminal = tds[3];
        let tdDepartureTime = tds[4];
        let tdArrivalTime = tds[5];
        let tdEconomyPrice = tds[6];
        let tdBusinessPrice = tds[7];
        let tdStatus = tds[8];

        tdId.innerText = id;
        tdDepartureAirport.innerText = departureAirport;
        tdArrivalAirport.innerText = arrivalAirport;
        tdTerminal.innerText = terminal;
        tdDepartureTime.innerText = this.departureTimeChanged ? this.convertISOToDate(departureTime) : departureTime;
        tdArrivalTime.innerText = this.arrivalTimeChanged ? this.convertISOToDate(arrivalTime) : arrivalTime;
        tdEconomyPrice.innerText = economyPrice + '$';
        tdBusinessPrice.innerText = businessPrice + '$';
        tdStatus.innerText = status;
    }

    editModalCloseHandler(e) {
        this.modalCloseHandler(e);
    }
    addNewModalCloseHandler(e) {
        this.modalCloseHandler(e);
    }

    deleteRow(e) {
        let tr = e.target.closest("tr");
        let id = tr.children[0].innerText;
        let tr2;

        if (this.isInvokedBySearch) {
            tr2 = document.getElementById(`flight-${id}`);
        }

        if (!window.confirm('This operation cannot be undone')) {
            return;
        }

        if (this.isInvokedBySearch) {
            tr2.style.display = 'none';
        }
        
        tr.style.display = 'none';

        this.requestManager.DELETE(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: {
                        tr.remove();
                        this.currentPageItems -= 1;
                        
                        if (this.isInvokedBySearch) {
                            tr2.remove();
                        }

                        break;
                    }
                    case 404: {
                        tr.style.display = '';

                        if (this.isInvokedBySearch) {
                            tr2.style.display = '';
                        }

                        showError(`Flight with id: ${id} cannot be found`);
                        break;
                    }
                    case 500: {
                        tr.style.display = '';

                        if (this.isInvokedBySearch) {
                            tr2.style.display = '';
                        }

                        showError("Server error, try again"); 
                        break;
                    }
                }
            },
            () => {
                tr.style.display = '';

                if (this.isInvokedBySearch) {
                    tr2.style.display = '';
                }

                showError("Cannot delete row, reason: network error. Try to reload this page");
            },
            true,
            id
        );
    }
}

export default (props) => (
    <Flights
        {...props}
        params={useParams()}
    />
);