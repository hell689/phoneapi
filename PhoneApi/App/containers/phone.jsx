import React from 'react';

export default class Phone extends React.Component {
    state = {
        phones: []//{ id: 1, phoneNumber: "22-22-22" }, { id: 2, phoneNumber: "33-33-33" }]
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
            </div>
        );
    }
};