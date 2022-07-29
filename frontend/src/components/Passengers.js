import React from "react";

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

        this.passengers = [];
    }

    componentDidMount() {
        fetch(this.apiUrl + 'all')
            .then(res => res.json())
            .then(
                (result) => {
                    this.passengers = result;
                    this.setPassengers();
                },
                (error) => {
                    this.showError("Network error, try to reload this page");
                });
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

                            <input type="text" className="form-control" placeholder="Passport" style={{
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
                if (this.validateInput(input, (passport) => {
                    let re = /^[a-z]{2}\d{6}$/;
                    return re.test(passport);
                }, 'Passport must be in this format: ab123456')) { return; }

                fetch(this.apiUrl + `search/byPassport/${searchValue}`, {
                    method: "GET"
                })
                    .then(res => res)
                    .then(
                        (result) => {
                            switch (result.status) {
                                case 200: {
                                    return result.json();
                                }
                                case 404: this.showError(`Cannot find passenger with passport: ${searchValue}`); break;
                                case 500: this.showError('Server error, please, contact administrator'); break;
                            }
                        },
                        (error) => {
                            this.showError("Cannot search, reason: network error. Try to reload this page");
                        })
                    .then((result) => {
                        this.addNewPassenger(result, tbody);
                    });

                break;
            }
            case 'Firstname': {
                if (this.validateInput(input, (firstname) => {
                    return firstname.length <= 50;
                }, "Firstname length must be not grater than 50")) { return; }
        
                fetch(this.apiUrl + `search/byFirtsname/${searchValue}`, {
                    method: "GET"
                })
                    .then(res => res)
                    .then(
                        (result) => {
                            switch (result.status) {
                                case 200: return result.json();
                                case 500: this.showError('Server error, please, contact administrator'); break;
                            }
                        },
                        (error) => {
                            this.showError("Cannot search, reason: network error. Try to reload this page");
                        })
                    .then(result => {
                        if (result.length == 0) {
                            this.showError(`Cannot find passengers with firstname: ${searchValue}`);
                        }

                        result.map(p => {
                            this.addNewPassenger(p, tbody);
                        });
                    });

                break;
            }
            case 'Lastname': {
                if (this.validateInput(input, (lastname) => {
                    return lastname.length <= 50;
                }, "Lastname length must be not grater than 50")) { return; }

                fetch(this.apiUrl + `search/byLastname/${searchValue}`, {
                    method: "GET"
                })
                    .then(res => res)
                    .then(
                        (result) => {
                            switch (result.status) {
                                case 200: return result.json();
                                case 500: this.showError('Server error, please, contact administrator'); break;
                            }
                        },
                        (error) => {
                            this.showError("Cannot search, reason: network error. Try to reload this page");
                        })
                    .then((result) => {
                        if (result.length == 0) {
                            this.showError(`Cannot find passengers with lastname: ${searchValue}`);
                        }

                        result.map(p => {
                            this.addNewPassenger(p, tbody);
                        });
                    });

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

        let passengersContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];

        passengersContainer.style.display = 'none';
        ticketsContainer.style.display = 'none';
        searchContainer.style.display = '';

        this.isInvokedBySearch = true;
    }

    closeSearch() {
        let containers = document.querySelectorAll('.container');

        let passengersContainer = containers[0];
        let ticketsContainer = containers[1];
        let searchContainer = containers[2];

        passengersContainer.style.display = '';
        ticketsContainer.style.display = 'none';
        searchContainer.style.display = 'none';

        this.isInvokedBySearch = false;

        let tbody = searchContainer.querySelector('tbody');
        tbody.innerText = '';
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

        fetch(`http://api.airportproject.com/tickets/passenger/${passengerId}/`)
            .then(res => res.json())
            .then(res => {
                loadingElement.style.display = 'none';
                res.forEach(t => this.addNewTicket(t, passengerId));
            }, error => {
                console.error(error)
                passengersContainer.style.display = '';
                ticketsContainer.style.display = 'none';
                this.showError("Cannot get tickets, reason: network error. Try to reload this page");
            });
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
        if (e.target.id == 'birthday-input') {
            this.validateInput(e.target, (birthday) => {
                return Date.now() >= Date.parse(birthday);
            }, 'Birthday must be not day in the future');

            return;
        }

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

        setError(this.validateInput(firstnameInput));
        setError(this.validateInput(lastnameInput));

        setError(this.validateInput(passportInput, (passport) => {
            let re = /^[a-z]{2}\d{6}$/;
            return re.test(passport);
        }, 'Passport must be in this format: ab123456'));

        if (modalId == 'addNewPassenger') {
            this.searchPassport(passportInput.value, tr => {
                this.showError("Passport must be unique");
                setError(true);

                tr.classList.remove('is-valid');
                tr.classList.add('is-invalid');
            });
        }

        setError(this.validateInput(nationalityInput));

        if (birthdayInput) {
            setError(this.validateInput(birthdayInput, (birthday) => {
                return Date.now() >= Date.parse(birthday);
            }, 'Birthday must be not day in the future'));
        }

        if (genderId == '' && genderInput) {
            this.showError('Gender must be selected');

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

            this.clearInputs(idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, birthdayInput);
        }

        return returnObj;
    }

    editModalHandler(e) {
        let { id, firstname, lastname, passport, nationality, birthday, gender, error } = this.saveDataFromModal('editPassengerModal');

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
                firstname,
                lastname,
                passport,
                nationality,
                birthday,
                gender
            })
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: {
                            let tr = document.getElementById(`passenger-${id}`);
                            let tds = tr.children;

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

                            break;
                        }
                        case 404: this.showError(`Passenger with id: ${id} cannot be found`); break;
                        case 500: this.showError("Server error, try again"); break;
                    }
                },
                (error) => {
                    this.showError("Cannot edit row, reason: network error. Try to reload this page");
                });
    }
    addNewModalHandler(e) {
        let { id, firstname, lastname, passport, nationality, birthday, gender, ticketId, error } = this.saveDataFromModal('addNewPassengerModal');

        if (error) {
            return;
        }

        this.addNewPassenger({
            id: undefined,
            firstname,
            lastname,
            passport,
            nationality,
            birthday,
            gender,
            ticketId
        });

        fetch(this.apiUrl, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                firstname,
                lastname,
                passport,
                nationality,
                birthday,
                gender,
                ticketId
            })
        })
            .then(res => {
                switch (res.status) {
                    case 200: return res.json();
                    case 500: this.showError("Server error, try again"); break;
                }
            }, error => this.showError('Cannot add row, reason: network error. Try to reload this page'))
            .then(result => {
                this.searchPassport(result.passport, tr => {
                    tr.id = `passenger-${result.id}`;

                    let tdId = tr.children[0];
                    tdId.innerText = result.id;
                });

            });
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

            this.showError('Ticket type must be chosen');

            setError(true);
        }

        setError(this.validateInput(fromInput));
        setError(this.validateInput(toInput));

        if (error) {
            return;
        }

        let from = fromInput.value;
        let to = toInput.value;

        console.log(JSON.stringify({
            from,
            to,
            type: ticketTypeId
        }))

        fetch(this.ticketApiUrl, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                from,
                to,
                type: ticketTypeId
            })
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: return result.json();
                        case 404: {
                            this.showError(`Ticket from: ${from}, to: ${to} cannot be found`);
                            break;
                        }
                        case 500: {
                            this.showError("Server error, try again");
                            break;
                        }
                    }
                },
                (error) => {
                    //tr.style.display = '';
                    this.showError("Cannot delete row, reason: network error. Try to reload this page");
                })
            .then(tickets => {
                let passengerId = this.currentPassengerId;
                tickets.forEach(t => {
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
            });
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
        let ticketIdInput = document.querySelector(`#${modalId} #ticket-input`);

        genderInput.setAttribute('gender-id', '');
        ticketIdInput.setAttribute('ticket-id', '');

        this.clearInputs(idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, birthdayInput);

        genderInput.classList.remove('is-valid');
        genderInput.classList.remove('is-invalid');

        ticketIdInput.classList.add('btn-primary');
        ticketIdInput.classList.remove('btn-dark');
    }
    editModalCloseHandler(e) {
        let modalId = 'addNewPassengerModal';

        let idInput = document.querySelector(`#${modalId} #id-input`);
        let firstnameInput = document.querySelector(`#${modalId} #firstname-input`);
        let lastnameInput = document.querySelector(`#${modalId} #lastname-input`);
        let passportInput = document.querySelector(`#${modalId} #passport-input`);
        let nationalityInput = document.querySelector(`#${modalId} #nationality-input`);
        let ticketIdInput = document.querySelector(`#${modalId} #ticket-input`);

        ticketIdInput.setAttribute('ticket-id', '');

        this.clearInputs(idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, null);

        ticketIdInput.classList.add('btn-primary');
        ticketIdInput.classList.remove('btn-dark');
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
                            this.showError(`Ticket with id: ${ticketId} or passenger with id: ${passengerId} cannot be found`);
                            break;
                        }
                        case 500: {
                            tr.style.display = '';
                            this.showError("Server error, try again");
                            break;
                        }
                    }
                },
                (error) => {
                    tr.style.display = '';
                    this.showError("Cannot add ticket, reason: network error. Try to reload this page");
                });
    }

    deleteTicket(e) {
        let tr = e.target.closest('tr');
        let ticketId = tr.getAttribute('ticket-id');
        let passengerId = tr.getAttribute('passenger-id');

        tr.style.display = 'none';

        fetch(this.apiUrl + `ticket/?passengerId=${passengerId}&ticketId=${ticketId}`, {
            method: "DELETE"
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: {
                            tr.remove();
                            break;
                        }
                        case 404: {
                            tr.style.display = '';
                            this.showError(`Ticket with id: ${ticketId} or passenger with id: ${passengerId} cannot be found`);
                            break;
                        }
                        case 500: {
                            tr.style.display = '';
                            this.showError("Server error, try again");
                            break;
                        }
                    }
                },
                (error) => {
                    tr.style.display = '';
                    this.showError("Cannot remove ticket, reason: network error. Try to reload this page");
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
            body: JSON.stringify(id)
        })
            .then(res => res)
            .then(
                (result) => {
                    switch (result.status) {
                        case 200: tr.remove(); break;
                        case 404: {
                            tr.style.display = '';
                            this.showError(`Passenger with id: ${id} cannot be found`);
                            break;
                        }
                        case 500: {
                            tr.style.display = '';
                            this.showError("Server error, try again");
                            break;
                        }
                    }
                },
                (error) => {
                    tr.style.display = '';
                    this.showError("Cannot delete row, reason: network error. Try to reload this page");
                });
    }

    clearInputs(idInput, firstnameInput, lastnameInput, passportInput, nationalityInput, birthdayInput) {
        firstnameInput.classList.remove('is-valid');
        firstnameInput.classList.remove('is-invalid');
        lastnameInput.classList.remove('is-valid');
        lastnameInput.classList.remove('is-invalid');
        passportInput.classList.remove('is-valid');
        passportInput.classList.remove('is-invalid');
        nationalityInput.classList.remove('is-valid');
        nationalityInput.classList.remove('is-invalid');

        if (birthdayInput) {
            birthdayInput.classList.remove('is-valid');
            birthdayInput.classList.remove('is-invalid');

            birthdayInput.value = '';
        }

        if (idInput) {
            idInput.value = '';
        }

        firstnameInput.value = '';
        lastnameInput.value = '';
        passportInput.value = '';
        nationalityInput.value = '';
    }
}

Object.defineProperty(String.prototype, 'capitalize', {
    value: function () {
        return this.charAt(0).toUpperCase() + this.slice(1);
    },
    enumerable: false
});

export default Passengers;