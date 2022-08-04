import React from "react";
import { useParams } from "react-router-dom";

import HTTPRequestManager from "../utils/HttpRequestManager";
import showError from "../utils/non-independent/showError";
import createPagination from "../utils/non-independent/createPagination";
import { validateInput } from "../utils/non-independent/validateInput";
import clearInputs from "../utils/non-independent/clearInputs";

import Header from "./non-independent/Header";
import Modal from "./non-independent/Modal";

class Passengers extends React.Component {
    constructor(props) {
        super(props);

        this.apiUrl = 'http://api.airportproject.com/passenger/';
        this.ticketApiUrl = 'http://api.airportproject.com/tickets/search';

        this.search = this.search.bind(this);
        this.openSearch = this.openSearch.bind(this);
        this.closeSearch = this.closeSearch.bind(this);

        this.setPassengers = this.setPassengers.bind(this);
        this.showTickets = this.showTickets.bind(this);

        this.validateInputOnChange = this.validateInputOnChange.bind(this);

        this.addNewModalHandler = this.addNewModalHandler.bind(this);
        this.editModalHandler = this.editModalHandler.bind(this);
        this.ticketModalHandler = this.ticketModalHandler.bind(this);

        this.addNewModalCloseHandler = this.addNewModalCloseHandler.bind(this);
        this.editModalCloseHandler = this.editModalCloseHandler.bind(this);

        this.deleteTicket = this.deleteTicket.bind(this);
        this.addTicket = this.addTicket.bind(this);
        this.closeTickets = this.closeTickets.bind(this);
        this.deleteRow = this.deleteRow.bind(this);

        this.setCurrentPassengerId = this.setCurrentPassengerId.bind(this);

        this.isInvokedByEditModal = false;
        this.isInvokedBySearch = false;

        this.pageNumber = 0;
        this.pagesCount = 1;
        this.currentPageItems = 0;

        this.requestManager = new HTTPRequestManager();

        this.passengers = [];
    }

    componentDidMount() {
        this.pageNumber = this.props.params.page == undefined ? '0' : this.props.params.page;

        if (!this.pageNumber.isPositiveInteger()) {
            this.requestManager.GET(this.apiUrl + 1,
                (response, status) => {
                    response = JSON.parse(response);

                    this.pageNumber = 1
                    this.totalPassengersCount = response.totalCount;
                    this.passengers = response.items;
                    this.setPassengers();
                    createPagination(this.totalPassengersCount, this.pageNumber, 'passengers');
                },
                () => showError("Network error, try to reload this page")
            );            
            
            return;
        }

        this.requestManager.GET(this.apiUrl + this.pageNumber,
            (response, status) => {
                response = JSON.parse(response);

                this.totalPassengersCount = response.totalCount;
                this.passengers = response.items;
                this.setPassengers();
                createPagination(this.totalPassengersCount, this.pageNumber, 'passengers');
            },
            () => showError("Network error, try to reload this page")
        );
    }

    render() {
        return (
            <main>
                <Header activeLink="passengers" />
                <br />

                <div className="container">
                    <div className="d-flex justify-content-between">
                        <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addNewPassengerModal">Add new passenger</button>
                        <button type="button" className="btn btn-dark" onClick={this.openSearch}>Search</button>
                    </div>
                    <br />
                    <br />
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Firstname</th>
                                <th scope="col">Lastname</th>
                                <th scope="col">Passport</th>
                                <th scope="col">Nationality</th>
                                <th scope="col">Birtday</th>
                                <th scope="col">Gender</th>
                                <th scope="col">Tickets</th>
                                <th scope="col" style={{ width: "99px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>

                <div className="container" id="tickets-table" style={{
                    display: 'none'
                }}>
                    <button type="button" className="btn btn-danger" onClick={this.closeTickets}>Close tickets view</button>
                    <br />
                    <br />
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">From</th>
                                <th scope="col">To</th>
                                <th scope="col">Type</th>
                                <th scope="col">Departure time</th>
                                <th scope="col">Arrival time</th>
                                <th scope="col">Price</th>
                                <th scope="col" style={{ width: "58px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>

                <div className="container" id="passengers-found-table" style={{
                    display: 'none'
                }}>
                    <div className="d-flex justify-content-between">
                        <button type="button" className="btn btn-danger" onClick={this.closeSearch}>Close search</button>

                        <div className="btn-group">
                            <div className="dropdown">
                                <button className="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" style={{
                                    borderTopRightRadius: 0,
                                    borderBottomRightRadius: 0
                                }}>
                                    Search by passport
                                </button>
                                <ul className="dropdown-menu">
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by passport</button></li>
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by firstname</button></li>
                                    <li><button className="dropdown-item" onClick={this.setPlaceholderForSearch}>Search by lastname</button></li>
                                </ul>
                            </div>

                            <input type="text" className="form-control" placeholder="Passport" onChange={this.validateInputOnChange} style={{
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
                                <th scope="col">Firstname</th>
                                <th scope="col">Lastname</th>
                                <th scope="col">Passport</th>
                                <th scope="col">Nationality</th>
                                <th scope="col">Birtday</th>
                                <th scope="col">Gender</th>
                                <th scope="col">Tickets</th>
                                <th scope="col" style={{ width: "99px" }}>Controls</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>

                <Modal id="addNewPassengerModal" title="Add new passenger" body={
                    (
                        <form className="row g-3">
                            <div className="col-md-6">
                                <label htmlFor="firstname-input" className="form-label">First name</label>
                                <input type="text" className="form-control" id="firstname-input" placeholder="firstname" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="lastname-input" className="form-label">Last name</label>
                                <input type="text" className="form-control" id="lastname-input" placeholder="lastname" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="col-md-6">
                                <label htmlFor="passport-input" className="form-label">Passport</label>
                                <input type="text" className="form-control" id="passport-input" placeholder="passport" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="nationality-input" className="form-label">Nationality</label>
                                <input type="text" className="form-control" id="nationality-input" placeholder="nationality" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="col-md-4">
                                <label htmlFor="gender-input" className="form-label">Gender</label>
                                <select className="form-select" name='Gender' id="gender-input" gender-id='' defaultValue={''} onChange={this.validateInputOnChange} required>
                                    <option value={''} disabled>Choose...</option>
                                    <option value={'male'} onClick={this.selectedGender}>Male</option>
                                    <option value={'female'} onClick={this.selectedGender}>Female</option>
                                </select>
                            </div>
                            <div className="col-md-5">
                                <label htmlFor="birthday-input" className="form-label">Birthday</label>
                                <input type="date" className="form-control" id="birthday-input" placeholder="birthday" onChange={this.validateInputOnChange} required />
                            </div>

                            <input id="id-input" type='hidden' />

                        </form>
                    )
                } saveOnClick={this.addNewModalHandler} closeOnClick={this.addNewModalCloseHandler} />

                <Modal id="editPassengerModal" title="Edit passenger" body={
                    (
                        <form className="row g-3">
                            <div className="col-md-6">
                                <label htmlFor="firstname-input" className="form-label">First name</label>
                                <input type="text" className="form-control" id="firstname-input" placeholder="firstname" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="lastname-input" className="form-label">Last name</label>
                                <input type="text" className="form-control" id="lastname-input" placeholder="lastname" onChange={this.validateInputOnChange} required />
                            </div>

                            <div className="col-md-6">
                                <label htmlFor="passport-input" className="form-label">Passport</label>
                                <input type="text" className="form-control" id="passport-input" placeholder="passport" onChange={this.validateInputOnChange} required />
                            </div>
                            <div className="col-md-6">
                                <label htmlFor="nationality-input" className="form-label">Nationality</label>
                                <input type="text" className="form-control" id="nationality-input" placeholder="nationality" onChange={this.validateInputOnChange} required />
                            </div>

                            <input id="id-input" type='hidden' />

                        </form>
                    )
                } saveOnClick={this.editModalHandler} closeOnClick={this.editModalCloseHandler} />

                <Modal id="ticketModal" title="Choose ticket" body={
                    (
                        <form className="row g-3">
                            <div className="input-group">
                                <input type='text' id="from-input" className="form-control" placeholder="From" onChange={this.validateInputOnChange} />
                                <input type='text' id="to-input" className="form-control" placeholder="To" onChange={this.validateInputOnChange} />
                            </div>
                            <div className="col-md-4">
                                <label htmlFor="ticket-type-input" className="form-label">Ticket type</label>
                                <select className="form-select" name='Ticket type' id="ticket-type-input" ticket-id='' defaultValue={''} onChange={this.validateInputOnChange} required>
                                    <option value={''} disabled>Choose...</option>
                                    <option value={'economy'} onClick={this.selectedTicket}>Economy</option>
                                    <option value={'business'} onClick={this.selectedTicket}>Business</option>
                                </select>
                            </div>
                        </form>
                    )
                } saveOnClick={this.ticketModalHandler} closeOnClick={this.ticketModalCloseHandler} />

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
        let input = document.querySelector('#passengers-found-table .form-control');
        let tbody = document.querySelector('#passengers-found-table tbody');

        let searchType = input.placeholder;
        let searchValue = input.value;
        
        tbody.innerText = '';

        switch (searchType) {
            case 'Passport': {
                if (validateInput(input, (passport) => {
                    let re = /^[a-z]{2}\d{6}$/;
                    return re.test(passport);
                }, 'Passport must be in this format: ab123456')) { return; }

                this.requestManager.GET(this.apiUrl + `search/byPassport/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);
                                this.addNewPassenger(response, tbody);
                                break;
                            }
                            case 404: showError(`Cannot find passenger with passport: ${searchValue}`); break;
                            case 500: showError('Server error, please, contact administrator'); break;
                        }
                    },
                    () => showError("Cannot search, reason: network error. Try to reload this page")
                );

                break;
            }
            case 'Firstname': {
                if (validateInput(input, (firstname) => {
                    return firstname.length <= 50;
                }, "Firstname length must be not grater than 50")) { return; }

                this.requestManager.GET(this.apiUrl + `search/byFirtsname/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);

                                if (response.length == 0) {
                                    showError(`Cannot find passengers with firstname: ${searchValue}`);
                                }
        
                                response.map(p => {
                                    this.addNewPassenger(p, tbody);
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
            case 'Lastname': {
                if (validateInput(input, (lastname) => {
                    return lastname.length <= 50;
                }, "Lastname length must be not grater than 50")) { return; }

                this.requestManager.GET(this.apiUrl + `search/byLastname/${searchValue}`,
                    (response, status) => {
                        switch (status) {
                            case 200: {
                                response = JSON.parse(response);

                                if (response.length == 0) {
                                    showError(`Cannot find passengers with lastname: ${searchValue}`);
                                }
        
                                response.map(p => {
                                    this.addNewPassenger(p, tbody);
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
        let placeholder = itemText.split(' ')[2].capitalize();

        let input = document.querySelector('#passengers-found-table .form-control');
        let dropdownToggle = document.querySelector('#passengers-found-table .dropdown-toggle');

        dropdownToggle.innerText = itemText;
        input.placeholder = placeholder;
    }

    openSearch() {
        let containers = document.querySelectorAll('.container');

        let flightsContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];

        flightsContainer.style.display = 'none';
        ticketsContainer.style.display = 'none';
        searchContainer.style.display = '';

        this.isInvokedBySearch = true;
    }

    closeSearch() {
        let containers = document.querySelectorAll('.container');

        let flightsContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];

        flightsContainer.style.display = '';
        ticketsContainer.style.display = 'none';
        searchContainer.style.display = 'none';

        let tbody = searchContainer.querySelector('tbody');
        tbody.innerText = '';

        this.isInvokedBySearch = false;
    }

    closeTickets(e) {
        let containers = document.querySelectorAll('.container');

        let passengersContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];
        
        if (this.isInvokedBySearch) {
            passengersContainer.style.display = 'none';
            searchContainer.style.display = '';
        } else {
            passengersContainer.style.display = '';
            searchContainer.style.display = 'none';
        }
        
        ticketsContainer.style.display = 'none';

        let tbody = document.querySelector('#tickets-table tbody');
        tbody.innerText = '';

        this.invokedByAddTickets = false;
    }

    showTickets(e) {
        let containers = document.querySelectorAll('.container');
        let passengerId = e.target.closest('td').getAttribute('passenger-id');
        let loadingElement = document.querySelector('.spinner-border');

        let passengersContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];

        passengersContainer.style.display = 'none';
        searchContainer.style.display = 'none';
        ticketsContainer.style.display = '';
        loadingElement.style.display = '';

        this.currentPassengerId = passengerId;

        this.requestManager.GET(`http://api.airportproject.com/tickets/passenger/${passengerId}/`,
            (response, status) => {
                response = JSON.parse(response);

                loadingElement.style.display = 'none';
                response.forEach(t => this.addNewTicket(t, passengerId));
            },
            () => {
                passengersContainer.style.display = '';
                ticketsContainer.style.display = 'none';
                showError("Cannot get tickets, reason: network error. Try to reload this page");
            }
        );
    }

    setCurrentPassengerId(e) {
        this.currentPassengerId = e.target.closest('td').getAttribute('passenger-id');
    }

    addNewTicket(ticket, passengerId) {
        let tbody = document.querySelector('#tickets-table tbody');

        let tr = document.createElement('tr');

        let tdId = document.createElement('td');
        let tdFrom = document.createElement('td');
        let tdTo = document.createElement('td');
        let tdType = document.createElement('td');
        let tdDepartureTime = document.createElement('td');
        let tdArrivalTime = document.createElement('td');
        let tdPrice = document.createElement('td');
        let tdControls = document.createElement('td');

        let buttonDelete = document.createElement('button');

        tr.id = `ticket-${ticket.id}`;
        tr.setAttribute('ticket-id', ticket.id);
        tr.setAttribute('passenger-id', passengerId);

        tdId.innerText = ticket.id;

        tdFrom.innerText = ticket.from;
        tdTo.innerText = ticket.to;
        tdType.innerText = ticket.type;
        tdDepartureTime.innerText = new Date(ticket.departureTime).toLocaleString('en-us');
        tdArrivalTime.innerText = new Date(ticket.arrivalTime).toLocaleString('en-us');
        tdPrice.innerText = ticket.price;

        tdFrom.setAttribute('scope', 'row');
        tdTo.setAttribute('scope', 'row');
        tdType.setAttribute('scope', 'row');
        tdDepartureTime.setAttribute('scope', 'row');
        tdArrivalTime.setAttribute('scope', 'row');
        tdPrice.setAttribute('scope', 'row');
        tdControls.setAttribute('scope', 'row');

        if (this.invokedByAddTickets) {
            buttonDelete.classList.add('btn', 'btn-outline-success', 'bi', 'bi-plus-lg');
            buttonDelete.addEventListener('click', this.addTicket);
        } else {
            buttonDelete.classList.add('btn', 'btn-outline-danger', 'bi', 'bi-trash3-fill');
            buttonDelete.addEventListener('click', this.deleteTicket);
        }

        buttonDelete.style.marginLeft = '15px';

        tdControls.appendChild(buttonDelete);

        tr.appendChild(tdId);
        tr.appendChild(tdFrom);
        tr.appendChild(tdTo);
        tr.appendChild(tdType);
        tr.appendChild(tdDepartureTime);
        tr.appendChild(tdArrivalTime);
        tr.appendChild(tdPrice);
        tr.appendChild(tdControls);

        tbody.appendChild(tr);
    }

    addNewPassenger(passenger, tbody = null) {
        if (tbody == null) {
            tbody = document.querySelector('tbody');
            this.currentPageItems += 1;
        }

        let tr = document.createElement('tr');

        let tdId = document.createElement('td');
        let tdFirstname = document.createElement('td');
        let tdLastname = document.createElement('td');
        let tdPassport = document.createElement('td');
        let tdNationality = document.createElement('td');
        let tdBirthday = document.createElement('td');
        let tdGender = document.createElement('td');
        let tdTickets = document.createElement('td');
        let tdControls = document.createElement('td');

        let seeButton = document.createElement('i');
        let addButton = document.createElement('i');

        let buttonEdit = document.createElement('button');
        let buttonDelete = document.createElement('button');

        tr.id = `passenger-${passenger.id}`;

        tdId.innerText = passenger.id;

        if (passenger.id == undefined) {
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

        tdFirstname.innerText = passenger.firstname;
        tdLastname.innerText = passenger.lastname;
        tdPassport.innerText = passenger.passport;
        tdNationality.innerText = passenger.nationality;
        tdBirthday.innerText = new Date(passenger.birthday).toLocaleDateString('en-us');
        tdGender.innerText = passenger.gender;

        tdFirstname.setAttribute('scope', 'row');
        tdLastname.setAttribute('scope', 'row');
        tdPassport.setAttribute('scope', 'row');
        tdNationality.setAttribute('scope', 'row');
        tdBirthday.setAttribute('scope', 'row');
        tdGender.setAttribute('scope', 'row');
        tdTickets.setAttribute('scope', 'row');
        tdControls.setAttribute('scope', 'row');

        tdTickets.setAttribute('passenger-id', passenger.id);

        buttonEdit.setAttribute('data-bs-toggle', 'modal');
        buttonEdit.setAttribute('data-bs-target', '#editPassengerModal');

        addButton.setAttribute('data-bs-toggle', 'modal');
        addButton.setAttribute('data-bs-target', '#ticketModal');

        tdControls.classList.add('btn-group', 'rounded-0');
        buttonEdit.classList.add('btn', 'btn-outline-primary', 'bi', 'bi-pencil-fill');
        buttonDelete.classList.add('btn', 'btn-outline-danger', 'bi', 'bi-trash3-fill');

        seeButton.classList.add('bi', 'bi-eye-fill');
        addButton.classList.add('bi', 'bi-plus', 'btn-success');

        seeButton.style.marginLeft = '10px';
        addButton.style.marginLeft = '5px';

        seeButton.style.cursor = 'pointer';
        addButton.style.cursor = 'pointer';

        buttonEdit.addEventListener('click', this.addDataToEditModal);
        buttonDelete.addEventListener('click', this.deleteRow);
        seeButton.addEventListener('click', this.showTickets);
        addButton.addEventListener('click', this.setCurrentPassengerId);

        tdControls.appendChild(buttonEdit);
        tdControls.appendChild(buttonDelete);

        tdTickets.appendChild(seeButton);
        tdTickets.appendChild(addButton);

        tr.appendChild(tdId);
        tr.appendChild(tdFirstname);
        tr.appendChild(tdLastname);
        tr.appendChild(tdPassport);
        tr.appendChild(tdNationality);
        tr.appendChild(tdBirthday);
        tr.appendChild(tdGender);
        tr.appendChild(tdTickets);
        tr.appendChild(tdControls);

        tbody.appendChild(tr);

        return tr;
    }

    setPassengers() {
        if (this.passengers.length < 0) {
            return;
        }
        let loadingElement = document.querySelector('.spinner-border');
        loadingElement.style.display = 'none';

        this.passengers.map(p => {
            this.addNewPassenger(p);
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

    selectedGender(e) {
        let value = e.target.value;
        let selectTag = e.target.closest('select');

        selectTag.setAttribute('gender-id', value);
    }
    selectedTicket(e) {
        let value = e.target.value;
        let selectTag = e.target.closest('select');

        selectTag.setAttribute('ticket-id', value);
    }

    addDataToEditModal(e) {
        let tr = e.target.closest("tr");

        let id = tr.children[0].innerText;
        let firstname = tr.children[1].innerText;
        let lastname = tr.children[2].innerText;
        let passport = tr.children[3].innerText;
        let nationality = tr.children[4].innerText;

        let idInput = document.querySelector('#editPassengerModal #id-input');
        let firstnameInput = document.querySelector('#editPassengerModal #firstname-input');
        let lastnameInput = document.querySelector('#editPassengerModal #lastname-input');
        let passportInput = document.querySelector('#editPassengerModal #passport-input');
        let nationalityInput = document.querySelector('#editPassengerModal #nationality-input');

        idInput.value = id;
        firstnameInput.value = firstname;
        lastnameInput.value = lastname;
        passportInput.value = passport;
        nationalityInput.value = nationality;
    }

    saveDataFromModal(modalId) {
        let modalEl = document.getElementById(modalId);
        let modal = bootstrap.Modal.getInstance(modalEl);

        let error = false;
        const setError = (e) => {
            if (!error && e) {
                error = true;
            }
        }

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let firstnameInput = document.querySelector(`#${modalId} #firstname-input`);
        let lastnameInput = document.querySelector(`#${modalId} #lastname-input`);
        let passportInput = document.querySelector(`#${modalId} #passport-input`);
        let nationalityInput = document.querySelector(`#${modalId} #nationality-input`);
        let genderInput = document.querySelector(`#${modalId} #gender-input`);
        let birthdayInput = document.querySelector(`#${modalId} #birthday-input`);

        let genderId = '';
        if (genderInput) {
            genderId = genderInput.getAttribute('gender-id');
        }

        setError(validateInput(firstnameInput));
        setError(validateInput(lastnameInput));

        setError(validateInput(passportInput, (passport) => {
            let re = /^[a-z]{2}\d{6}$/;
            return re.test(passport);
        }, 'Passport must be in this format: ab123456'));

        if (modalId == 'addNewPassenger') {
            this.searchPassport(passportInput.value, tr => {
                showError("Passport must be unique");
                setError(true);

                tr.classList.remove('is-valid');
                tr.classList.add('is-invalid');
            });
        }

        setError(validateInput(nationalityInput));

        if (birthdayInput) {
            setError(validateInput(birthdayInput, (birthday) => {
                return Date.now() >= Date.parse(birthday);
            }, 'Birthday must be not day in the future'));
        }

        if (genderId == '' && genderInput) {
            showError('Gender must be selected');

            genderInput.classList.remove('is-valid');
            genderInput.classList.add('is-invalid');

            setError(true);
        }

        const returnObj = {
            id: idInput ? idInput.value : undefined,
            firstname: firstnameInput.value,
            lastname: lastnameInput.value,
            passport: passportInput.value,
            nationality: nationalityInput.value,
            birthday: birthdayInput ? birthdayInput.value : undefined,
            gender: genderId ? genderId : undefined,
            error
        };

        if (!error) {
            modal.hide();

            if (genderInput) {
                genderInput.classList.remove('is-invalid');
                genderInput.classList.remove('is-valid');

                genderInput.setAttribute('gender-id', '');
            }

            clearInputs([idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, birthdayInput]);
        }

        return returnObj;
    }

    editModalHandler(e) {
        let { id, firstname, lastname, passport, nationality, birthday, gender, error } = this.saveDataFromModal('editPassengerModal');

        if (error) {
            return;
        }

        this.requestManager.PUT(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: {
                        let tr = document.getElementById(`passenger-${id}`);
                        let tds = tr.children;

                        this.setPassengerByTDs(tds, id, firstname, lastname, passport, nationality);

                        if (this.isInvokedBySearch) {
                            tr = document.querySelectorAll(`#passenger-${id}`)[1];
                            tds = tr.children;

                            this.setPassengerByTDs(tds, id, firstname, lastname, passport, nationality);
                        }

                        break;
                    }
                    case 404: showError(`Passenger with id: ${id} cannot be found`); break;
                    case 500: showError("Server error, try again"); break;
                }
            },
            () => showError("Cannot edit row, reason: network error. Try to reload this page"),
            true,
            JSON.stringify({
                id,
                firstname,
                lastname,
                passport,
                nationality,
                birthday,
                gender
            })
        );
    }
    addNewModalHandler(e) {
        let { id, firstname, lastname, passport, nationality, birthday, gender, ticketId, error } = this.saveDataFromModal('addNewPassengerModal');

        if (error) {
            return;
        }

        let tr;

        this.totalPassengersCount += 1;

        if (this.currentPageItems == 6) {
            this.createPagination(this.totalPassengersCount, this.pageNumber, 'passengers');
        } else {
            tr = this.addNewPassenger({
                id: undefined,
                firstname,
                lastname,
                passport,
                nationality,
                birthday,
                gender,
                ticketId
            });
        }

        this.requestManager.POST(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: {
                        response = JSOn.parse(response);

                        this.searchPassport(response.passport, tr => {
                            tr.id = `passenger-${response.id}`;
        
                            let tdId = tr.children[0];
                            tdId.innerText = response.id;
        
                            let tdTickets = tr.children[7];
                            tdTickets.setAttribute('passenger-id', response.id);
                        });

                        break;
                    }
                    case 500: {
                        showError(`Passenger with passport: ${passport} is already exists!`);
                        tr.remove();
                        this.currentPageItems -= 1;
                        break;
                    }
                }
            },
            () => showError('Cannot add row, reason: network error. Try to reload this page'),
            true,
            JSON.stringify({
                firstname,
                lastname,
                passport,
                nationality,
                birthday,
                gender,
                ticketId
            })
        );
    }
    ticketModalHandler(e) {
        let ticketModal = document.getElementById('ticketModal');
        let modal = bootstrap.Modal.getInstance(ticketModal);

        let fromInput = document.querySelector('#ticketModal #from-input');
        let toInput = document.querySelector('#ticketModal #to-input');

        let ticketTypeInput = document.querySelector('#ticketModal #ticket-type-input');

        let ticketTypeId = ticketTypeInput.getAttribute('ticket-id');

        let error = false;
        let setError = (e) => {
            if (!error && e) {
                error = true;
            }
        }

        if (ticketTypeId == '') {
            ticketTypeInput.classList.remove('is-valid');
            ticketTypeInput.classList.add('is-invalid');

            showError('Ticket type must be chosen');

            setError(true);
        }

        setError(validateInput(fromInput));
        setError(validateInput(toInput));

        if (error) {
            return;
        }

        let from = fromInput.value;
        let to = toInput.value;

        this.requestManager.POST(this.apiUrl,
            (response, status) => {
                switch (status) {
                    case 200: {
                        response = JSON.parse(response);

                        let passengerId = this.currentPassengerId;
                        response.forEach(t => {
                            this.invokedByAddTickets = true;
                            this.addNewTicket(t, passengerId);
                        });
        
                        let containers = document.querySelectorAll('.container');
        
                        let passengersContainer = containers[0];
                        let ticketsContainer = containers[1];
                        let searchContainer = containers[2];
        
                        passengersContainer.style.display = 'none';
                        searchContainer.style.display = 'none';
                        ticketsContainer.style.display = '';
        
                        modal.hide();
                        this.ticketModalCloseHandler();

                        break;
                    }
                    case 404: {
                        showError(`Ticket from: ${from}, to: ${to} cannot be found`);
                        break;
                    }
                    case 500: {
                        showError("Server error, try again");
                        break;
                    }
                }
            },
            () => showError("Cannot delete row, reason: network error. Try to reload this page"),
            true,
            JSON.stringify({
                from,
                to,
                type: ticketTypeId
            })
        );
    }

    setPassengerByTDs(tds, id, firstname, lastname, passport, nationality) {
        let tdId = tds[0];
        let tdFirstname = tds[1];
        let tdLastname = tds[2];
        let tdPassport = tds[3];
        let tdNationality = tds[4];

        tdId.innerText = id;
        tdFirstname.innerText = firstname;
        tdLastname.innerText = lastname;
        tdPassport.innerText = passport;
        tdNationality.innerText = nationality;
    }

    addNewModalCloseHandler(e) {
        let modalId = 'addNewPassengerModal';

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let firstnameInput = document.querySelector(`#${modalId} #firstname-input`);
        let lastnameInput = document.querySelector(`#${modalId} #lastname-input`);
        let passportInput = document.querySelector(`#${modalId} #passport-input`);
        let nationalityInput = document.querySelector(`#${modalId} #nationality-input`);
        let genderInput = document.querySelector(`#${modalId} #gender-input`);
        let birthdayInput = document.querySelector(`#${modalId} #birthday-input`);

        genderInput.setAttribute('gender-id', '');

        clearInputs([idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, birthdayInput]);

        genderInput.classList.remove('is-valid');
        genderInput.classList.remove('is-invalid');
    }
    editModalCloseHandler(e) {
        let modalId = 'addNewPassengerModal';

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let firstnameInput = document.querySelector(`#${modalId} #firstname-input`);
        let lastnameInput = document.querySelector(`#${modalId} #lastname-input`);
        let passportInput = document.querySelector(`#${modalId} #passport-input`);
        let nationalityInput = document.querySelector(`#${modalId} #nationality-input`);

        clearInputs([idInput, firstnameInput, lastnameInput, passportInput, nationalityInput]);
    }
    ticketModalCloseHandler(e) {
        let fromInput = document.querySelector('#ticketModal #from-input');
        let toInput = document.querySelector('#ticketModal #to-input');
        let ticketTypeInput = document.querySelector('#ticketModal #ticket-type-input');

        fromInput.classList.remove('is-valid');
        fromInput.classList.remove('is-invalid');
        toInput.classList.remove('is-invalid');
        toInput.classList.remove('is-valid');
        ticketTypeInput.classList.remove('is-invalid');
        ticketTypeInput.classList.remove('is-valid');

        fromInput.value = '';
        toInput.value = '';
        ticketTypeInput.value = '';
    }

    searchPassport(passport, foundCallback) {
        let trs = document.querySelectorAll('tbody tr');

        trs.forEach(tr => {
            let tdPassport = tr.children[3];

            if (tdPassport.innerText == passport) {
                foundCallback(tr);
            }
        });
    }

    addTicket(e) {
        let button = e.target;
        let tr = button.closest('tr');
        let ticketId = tr.getAttribute('ticket-id');
        let passengerId = tr.getAttribute('passenger-id');

        button.classList.add('disabled');

        fetch(this.apiUrl + `ticket/?passengerId=${passengerId}&ticketId=${ticketId}`, {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ passengerId, ticketId })
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: {
                            tr.remove();
                            return result.json();
                        }
                        case 404: {
                            tr.style.display = '';
                            showError(`Ticket with id: ${ticketId} or passenger with id: ${passengerId} cannot be found`);
                            break;
                        }
                        case 500: {
                            tr.style.display = '';
                            showError("Server error, try again");
                            break;
                        }
                    }
                },
                (error) => {
                    tr.style.display = '';
                    showError("Cannot add ticket, reason: network error. Try to reload this page");
                });
    }

    deleteTicket(e) {
        let tr = e.target.closest('tr');
        let ticketId = tr.getAttribute('ticket-id');
        let passengerId = tr.getAttribute('passenger-id');

        tr.style.display = 'none';

        this.requestManager.DELETE(this.apiUrl + `ticket/?passengerId=${passengerId}&ticketId=${ticketId}`,
            (response, status) => {
                switch (status) {
                    case 200: {
                        tr.remove();
                        break;
                    }
                    case 404: {
                        tr.style.display = '';
                        showError(`Ticket with id: ${ticketId} or passenger with id: ${passengerId} cannot be found`);
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
                showError("Cannot remove ticket, reason: network error. Try to reload this page");
            },
            false
        );
    }

    deleteRow(e) {
        let tr = e.target.closest("tr");
        let id = tr.children[0].innerText;
        let tr2;

        if (this.isInvokedBySearch) {
            tr2 = document.getElementById(`passenger-${id}`);
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

                        showError(`Passenger with id: ${id} cannot be found`);
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

Object.defineProperty(String.prototype, 'capitalize', {
    value: function () {
        return this.charAt(0).toUpperCase() + this.slice(1);
    },
    enumerable: false
});

Object.defineProperty(String.prototype, 'isPositiveInteger', {
    value: function () {
        const num = Number(this);

        if (typeof num != 'number') {
            return false;
        }

        return num.isPositive();
    },
    enumerable: false
});

Object.defineProperty(Number.prototype, 'isPositive', {
    value: function () {
        return this > 0;
    },
    enumerable: false
});

export default (props) => (
    <Passengers
        {...props}
        params={useParams()}
    />
);