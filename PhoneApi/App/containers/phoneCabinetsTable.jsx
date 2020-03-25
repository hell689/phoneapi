import React from 'react';

function PhoneCabinetList(props) {
    //props.getPhoneCabinets(props.editedPhone.id);
    if (props.phoneCabinets == null) {
        return null;
    }

    const phoneCabinetList = props.phoneCabinets.map((cabinet) => {
        return <button type="button" className="btn btn-primary ml-1"
            key={cabinet.id} onClick={(e) => props.parent.removeCabinetfromPhone(props.editedPhone, cabinet.id, props.parent)}>{cabinet.cabinetNumber}</button>;
    });

    return (
        <div className="btn-group btn-group-lg mx-auto" role="group" aria-label="Basic example">
            {phoneCabinetList}
        </div>
    );
}

export default class PhoneCabinetsTable extends React.Component {
    constructor(props) {
        super(props)
        this.state = {            
            cabinets: [],
            phoneCabinets: [],
            isLoading: false,
        };
        this.getPhoneCabinets = this.getPhoneCabinets.bind(this);
        this.removeCabinetfromPhone = this.removeCabinetfromPhone.bind(this);
    }

    componentDidMount() {
        this.getCabinets(); 
        this.getPhoneCabinets(this.props.editedPhone.id);
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

    getPhoneCabinets(phoneId) {
        fetch(window.constants.phones + "/" + phoneId)
            .then((response) => {
                return response.json();
            }).then((data) => {                
                this.setState({
                    phoneCabinets: data.cabinets
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
                return { x: 1 };
            }).then((data) => {
                this.getPhoneCabinets(phone.id);
            }
            );
        event.preventDefault();
    }

    removeCabinetfromPhone(phone, cabinetId) {
        this.setState({ isLoading: true });
        fetch(window.constants.phones + "/" + phone.id + "/" + cabinetId, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            }).then((data) => {
                this.getPhoneCabinets(phone.id);
            }
            );
    }

    render() {
        /*if (!this.props.showTable) {
            return null;
        }*/
        
        const cabinetList = this.state.cabinets.map((cabinet) => {
            return <button type="button" className="btn btn-primary ml-1"
                key={cabinet.id} onClick={(e) => this.addCabinetToPhone(this.props.editedPhone, cabinet.id)}>{cabinet.cabinetNumber}</button>;
        });

        return (
            <div>
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
                                <PhoneCabinetList removeCabinetfromPhone={this.removeCabinetfromPhone}
                                    phoneCabinets={this.state.phoneCabinets}
                                    editedPhone={this.props.editedPhone} getPhoneCabinets={this.getPhoneCabinets}
                                    parent={this}/>
                            </td>
                            <td>
                                <div className="btn-group btn-group-lg mx-auto" role="group" aria-label="Basic example">
                                    {cabinetList}
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>
                <button className="btn btn-primary" onClick={this.props.clickCloseTable}>Закрыть</button>
                </div>
        );
    }
}
