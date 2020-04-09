import React from 'react';
import Spinner from './spinner.jsx';

function EmployeePhones(props) {

    const employeePhoneList = props.employeePhones.map((cabinetPhone, index) => {
        return <li className="list-group-item" key={index}>{cabinetPhone.phone.phoneNumber + "  "} 
            (каб. № {cabinetPhone.cabinet.cabinetNumber})</li>;

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
                    <EmployeePhones employeePhones={employee.cabinetPhones} />
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
                    <tbody>
                        {catalogRow}
                    </tbody>
                </table>
                <Spinner loading={this.state.isLoading} />
            </div>
        );
    }
};