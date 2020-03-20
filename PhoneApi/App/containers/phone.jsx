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

    render() {
        const list = this.state.phones.map((phone, index) => {
            return <li key={index}>{phone.phoneNumber}</li>;
        });
        return (
            <div>
                <h2>Телефоны</h2>
                <ul>
                    {list}
                </ul>
                <form onSubmit={this.addPhone.bind(this)}>
                    <label>
                        Новый телефон
                        <input type="tel" value={this.state.newPhone} pattern="[0-9]{2}-[0-9]{2}-[0-9]{2}"
                            onChange={this.handleChange.bind(this)} />
                    </label>
                    <input type="submit" value="Добавить" />
                </form>

            </div>
        );
    }
};