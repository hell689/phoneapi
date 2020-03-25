﻿import React from 'react';
import Spinner from './spinner.jsx';

function EmployeePhones(props) {

    const employeePhoneList = props.employeePhones.map((phone, index) => {
        return <li className="list-group-item" key={index}>{phone.phoneNumber + "  "} 
            (каб. № <PhoneCabinets cabinets={phone.cabinets} />)</li>;

    });

    return (
    <ul className="list-group">
        {employeePhoneList}
    </ul>);
}

function PhoneCabinets(props) {

    const phoneCabinetsList = props.cabinets.map((cabinet, index) => {
        return <span key={index}> "{cabinet.cabinetNumber}"</span>;

    });

    return (
        <span>
            {phoneCabinetsList}
        </span>);
}

export default class Home extends React.Component {

    constructor() {
        super();
        this.state = {
            employees: [],
            isLoading: false,
        };
        this.getEmployees();
    }

    componentDidMount() {
        this.setState({ isLoading: true });
        this.getEmployees();
    }

    getEmployees() {
        fetch(window.constants.employees)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    employees: data,
                    isLoading: false
                });
            }
            )
    }

    render() {

        const catalogRow = this.state.employees.map((employee, index) => {
            return <tr key={index}>
                <td>
                    {employee.surname + "  " + employee.name + "  " + employee.patronymic}
                </td>
                <td>
                    <EmployeePhones employeePhones={employee.phones} />
                </td>
            </tr>;
        });

        return (
            <div>
                <h2>Телефонный справочник</h2>

                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">ФИО сотрудника</th>
                            <th scope="col">Телефон</th>
                        </tr>
                    </thead>
                    <Spinner loading={this.state.isLoading} />
                    <tbody>
                        {catalogRow}
                    </tbody>
                </table>

            </div>
        );
    }
};