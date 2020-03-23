import React from 'react';
import Spinner from './spinner.jsx';

export default class Cabinet extends React.Component {
    constructor() {
        super();
        this.state = {
            cabinets: [],
            newCabinet: "",
            isLoading: false,
        };
    }

    componentDidMount() {
        this.setState({ isLoading: true });
        this.getCabinets(); 
        
    }

    getCabinets() {
        fetch(window.constants.cabinets)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    cabinets: data,
                    isLoading: false
                });
            }
            )  
    }

    handleChange(event) {
        let cabinet = event.target.value;
        this.setState({
            newCabinet: cabinet
        });
    }

    addCabinet(event) {
        this.setState({ isLoading: true });
        if (this.state.newCabinet.length > 0) {
            fetch(window.constants.cabinets, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    CabinetNumber: this.state.newCabinet,
                })
            })
                .then(function (response) {
                    return response.json();
                }).then((data) => {
                    this.getCabinets();
                    this.setState({ newCabinet: "" })
                }
                );
        }
        event.preventDefault(); 
    }

    deleteCabinet(idForDelete) {
        this.setState({ isLoading: true });
        fetch(window.constants.cabinets + "/" + idForDelete, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {                
                return response.json();
            }).then((data) => {
                this.getCabinets();
            }
        );
    }

    render() {
        const list = this.state.cabinets.map((cabinet, index) => {
            return <li className="list-group-item d-flex justify-content-between align-items-center" key={index}>
                {cabinet.cabinetNumber}
                <button className="btn badge badge-primary badge-pill" onClick={(e) => this.deleteCabinet(cabinet.id, this)}>X</button>
                </li>;
        });
        return (
            <div>
                <h2>Список кабинетов</h2>

                <Spinner loading={this.state.isLoading} />

                <ul className="list-group">
                    {list}
                </ul>
                <form onSubmit={this.addCabinet.bind(this)}>
                    <div className="form-group">
                        <label>Новый кабинет</label>
                        <input type="text" className="form-control" value={this.state.newCabinet} aria-describedby="helper"
                            onChange={this.handleChange.bind(this)} />
                        <small id="helper" className="form-text text-muted">Введите номер кабинета</small>
                    </div>

                    <button type="submit" className="btn btn-primary">Добавить</button>
                </form>

            </div>
        );
    }
};