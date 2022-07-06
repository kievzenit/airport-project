export default function Table(props) {
    if (props.columns && props.rows) {
        return (
            <table className="table table-striped">
                <thead>
                    <tr>{props.columns}</tr>
                </thead>
                <tbody>{props.rows}</tbody>
            </table>
        );
    }

    return null;
}