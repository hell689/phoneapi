import React from 'react';

export default class Phone extends React.Component {
    state = {
        phones: [],//{ id: 1, phoneNumber: "22-22-22" }, { id: 2, phoneNumber: "33-33-33" }]
        newPhone: ""
    };

	componentDidMount() {
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
        this.setState({
            newPhone: event.target.value
        });
    }

    addPhone(event) {
        fetch(window.constants.phones, {
            method: "POST",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                phoneNumber: this.state.newPhone,
            }),
        })
            .then(function (response) {
                //return response.json();
            })
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
                <form onSubmit={this.addPhone}>
                    <label>
                        Новый телефон
                        <input type="text" value={this.state.newPhone} onChange={this.handleChange} />
                    </label>
                    <input type="submit" value="Добавить" />
                </form>

            </div>
        );
    }
};