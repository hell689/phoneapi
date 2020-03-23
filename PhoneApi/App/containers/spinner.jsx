import React from 'react';

export default class Spinner extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        if (!this.props.loading) {
            return null;
        }
        return (
            <div className="d-flex justify-content-center">
                <div className="spinner-border text-primary" role="status">
                    <span className="sr-only">Loading...</span>
                </div>
            </div>

            );
    }
}
