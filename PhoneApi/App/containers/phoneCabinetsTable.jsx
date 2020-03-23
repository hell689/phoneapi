import React from 'react';

export default class PhoneCabinetsTable extends React.Component {
    constructor(props) {
        super(props)
        this.state = {            
            cabinets: null,
            isLoading: false,
        };
    }

    componentDidMount() {
        this.getCabinets();
    }

    getCabinets() {
        fetch(window.constants.cabinets)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    cabinets: data                    
                });
            }
            )
    }

    addCabinetToPhone(phone, cabinetId) {
        fetch(window.constants.phones + "/" + phone.id + "/" + cabinetId, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                cabinetId: cabinetId,
                phoneId: phone.id
            })
        })
            .then(function (response) {
                return response.json();
            }).then((data) => {                

            }
            );
        event.preventDefault();
    }

    render() {
        if (!this.props.showTable) {
            return null;
        }

        const cabinetList = this.state.cabinets.map((cabinet) => {
            return <button type="button" className="btn btn-primary ml-1"
                key={cabinet.id} onClick={(e) => this.addCabinetToPhone(this.props.editedPhone, cabinet.id)}>{cabinet.cabinetNumber}</button>;
        });

        return (
            <table className="table table-bordered mt-3">
                <thead>
                    <tr>
                        <th scope="col">Кабинеты с номером телефона {this.props.editedPhone.phoneNumber}</th>
                        <th scope="col">Добавить кабинет</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                               
                        </td>
                        <td>
                            <div className="btn-group btn-group-lg mx-auto" role="group" aria-label="Basic example">
                                {cabinetList}
                            </div>
                        </td>
                    </tr>

                </tbody>
            </table>
        );
    }
}
