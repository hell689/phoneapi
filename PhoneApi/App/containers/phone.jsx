import React from 'react';

export default class Phone extends React.Component {
    constructor() {
        super();
        this.state = {
            phones: [],//{ id: 1, phoneNumber: "22-22-22" }, { id: 2, phoneNumber: "33-33-33" }]
            newPhone: ""
        };
    }

    componentDidMount() {
        this.getPhones();  
    }

    getPhones() {
        fetch(window.constants.phones)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    phones: data
                });
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
                this.getPhones();
                this.setState({ newPhone: "" })
            }
            );
        event.preventDefault(); 
    }

    deletePhone(idForDelete) {
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

    render() {
        const list = this.state.phones.map((phone, index) => {
            return <li className="list-group-item d-flex justify-content-between align-items-center" key={index}>
                    {phone.phoneNumber}
                <button className="btn badge badge-primary badge-pill" onClick={(e) => this.deletePhone(phone.id, this)}>X</button>
                </li>;
        });
        return (
            <div>
                <h2>Телефоны</h2>
                <ul className="list-group">
                    {list}
                </ul>
                <form onSubmit={this.addPhone.bind(this)}>
                    <div className="form-group">
                        <label>Новый телефон</label>
                        <input type="tel" className="form-control" value={this.state.newPhone} aria-describedby="emailHelp"
                            pattern="[0-9]{2}-[0-9]{2}-[0-9]{2}" onChange={this.handleChange.bind(this)} />
                        <small id="emailHelp" className="form-text text-muted">Введите номер телефона в формате ХХ-ХХ-ХХ</small>
                    </div>

                    <button type="submit" className="btn btn-primary">Добавить</button>
                </form>

            </div>
        );
    }
};