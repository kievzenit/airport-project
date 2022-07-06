export default function Modal(props) {
    if (props.id && props.title && props.body && props.saveOnClick && props.closeOnClick) {
        return (
            <div className="modal fade" id={props.id} tabIndex="-1" aria-labelledby={`${props.id}Label`} aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id={`${props.id}Label`}>{props.title}</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            {props.body}
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-success" onClick={props.saveOnClick}>Save</button>
                            <button type="button" className="btn btn-danger" onClick={props.closeOnClick} data-bs-dismiss="modal">Discard changes</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    return null;
}