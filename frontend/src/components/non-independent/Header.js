export default function Header(props) {
    let classes = {
        airports: "nav-link",
        flights: "nav-link",
        passengers: "nav-link"
    }

    switch (props.activeLink) {
        case "airports":
            classes.airports += ' active';
            break;
        case "flights":
            classes.flights += ' active';
            break;
        case "passengers":
            classes.passengers += ' active';
            break;
    }

    return (
        <header className="navbar navbar-expand-lg bg-light">
            <div className="container-fluid d-flex flex-row-reverse justify-content-between">
                <a className="navbar-brand" href="/">SkyUp Airline Panel</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <a className={classes.airports} aria-current="page" href="/">Airports</a>
                        </li>
                        <li className="nav-item">
                            <a className={classes.flights} href="/flights">Flights</a>
                        </li>
                        <li className="nav-item">
                            <a className={classes.passengers} href="/passengers">Passengers</a>
                        </li>
                    </ul>
                </div>
            </div>
        </header>
    );
}