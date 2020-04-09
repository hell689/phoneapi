import React from 'react';
import Spinner from './spinner.jsx';
import PhoneCabinetsTable from './phoneCabinetsTable.jsx';
import AuthHelper from './authHelper.jsx';
import { Redirect } from 'react-router-dom';

function ShowTable(props) {
    const isShow = props.showTable;
    if (isShow) {
        return <PhoneCabinetsTable showTable={props.showTable}
                cabinets={props.cabinets} editedPhone={props.editedPhone}
                clickCloseTable={props.clickCloseTable} />;
    }
    return <div></div>;
}

export default class Phone extends React.Component {
    constructor() {
        super();
        this.state = {
            phones: [],
            newPhone: "",
            cabinets: [],
            showAddPhoneToCabinets: false,
            editedPhone: {},
            isLoading: false,
        };
        this._isMounted = false;
        this.clickCloseTable = this.clickCloseTable.bind(this);
    }

    componentDidMount() {
        this._isMounted = true;
        this.setState({ isLoading: true });
        this.getPhones();  
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    getPhones() {
        
        fetch(window.constants.phones)
            .then((response) => {
                return response.json();
            }).then((data) => {
                if (this._isMounted) {
                    this.setState({
                        phones: data,
                        isLoading: false
                    });
                }
            }
            )  
    }

    handleChange(event) {
        let phone = event.target.value;
        this.setState({
            newPhone: phone
        });
    }

    addPhone(event) {
        this.setState({ isLoading: true });
        fetch(window.constants.phones, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                phoneNumber: this.state.newPhone,
            })
        })
            .then(function (response) {
                return response.json();
            }).then((data) => {
                this.getPhones(),
                this.setState({ newPhone: "" });
            }
        );        
        event.preventDefault(); 
    }

    deletePhone(idForDelete) {
        this.setState({ isLoading: true });
        fetch(window.constants.phones + "/" + idForDelete, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {                
                return response.json();
            }).then((data) => {
                this.getPhones();
            }
        );
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

    clickPhone(phone) {
        this.clickCloseTable();
        this.setState({
            editedPhone: phone,
            showAddPhoneToCabinets: true            
        });        
    }

    clickCloseTable() {
        this.setState({
            editedPhone: {},
            showAddPhoneToCabinets: false
        });   
    }


    render() {

        if (!AuthHelper.isLogged()) {
            return <Redirect to="/login" />
        }

        const phoneList = this.state.phones.map((phone, index) => {
            return <li className="list-group-item d-flex justify-content-between align-items-center" key={index}
                onClick={(e) => this.clickPhone(phone, this)} >
                {phone.phoneNumber}
                <button className="btn badge badge-primary badge-pill" onClick={(e) => this.deletePhone(phone.id, this)}>X</button>
                </li>;
        });

        return (
            <div>
                <h2>Телефоны</h2>

                <Spinner loading={this.state.isLoading} />

                <ul className="list-group">
                    {phoneList}
                </ul>

                <form onSubmit={this.addPhone.bind(this)}>
                    <div className="form-group">
                        <label>Новый телефон</label>
                        <input type="tel" className="form-control" value={this.state.newPhone} aria-describedby="helper"
                            pattern="[0-9]{2}-[0-9]{2}-[0-9]{2}" onChange={this.handleChange.bind(this)} />
                        <small id="helper" className="form-text text-muted">Введите номер телефона в формате ХХ-ХХ-ХХ</small>
                    </div>

                    <button type="submit" className="btn btn-primary">Добавить</button>
                </form>

                <ShowTable showTable={this.state.showAddPhoneToCabinets}
                    cabinets={this.state.cabinets} editedPhone={this.state.editedPhone}
                    clickCloseTable={this.clickCloseTable} />

            </div>
        );
    }
};