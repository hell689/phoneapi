import React from 'react';
import Spinner from './spinner.jsx';
import EmployeePhonesTable from './employeePhonesTable.jsx';

function ShowTable(props) {
    const isShow = props.showTable;

    if (isShow) {
        alert(props.editedEmployee.surname);
        return <EmployeePhonesTable showTable={props.showTable}
            phones={props.phones} editedEmployee={props.editedEmployee}
            clickCloseTable={props.clickCloseTable} />;
    }
    return <div></div>;
}

export default class Employee extends React.Component {
    constructor() {
        super();
        this.state = {
            employees: [],
            newName: "",
            newSurname: "",
            newPatronymic: "",
            phones: [],
            showAddPhoneToEmployee: false,
            editedEmployee: {},
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

    handleChangeName(event) {
        let name = event.target.value;
        this.setState({
            newName: name
        });
    }



    handleChangeSurname(event) {
        let surname = event.target.value;
        this.setState({
            newSurname: surname
        });
    }

    handleChangePatronymic(event) {
        let patronymic = event.target.value;
        this.setState({
            newPatronymic: patronymic
        });
    }

    addEmployee(event) {
        this.setState({ isLoading: true });
        if (this.state.newName.length > 0 && this.state.newSurname.length > 0 && this.state.newPatronymic.length > 0) {
            let employee = { Name: this.state.newName, Surname: this.state.newSurname, Patronymic: this.state.newPatronymic };
            fetch(window.constants.employees, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Name: employee.Name,
                    Surname: employee.Surname,
                    Patronymic: employee.Patronymic
                })
            })
                .then(function (response) {
                    return response.json();
                }).then((data) => {
                    this.getEmployees();
                    this.setState({ newEmployee: "" })
                }
                );
        }
        event.preventDefault();
    }

    deleteEmployee(idForDelete) {
        this.setState({ isLoading: true });
        fetch(window.constants.employees + "/" + idForDelete, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            }).then((data) => {
                this.getEmployees();
            }
            );
    }

    clickEmployee(employee) {
        this.setState({
            editedEmployee: employee,
            showAddPhoneToEmployee: true
        });
    }

    clickCloseTable() {
        this.setState({
            editedEmployee: {},
            showAddPhoneToEmployee: false
        });
    }

    render() {
        const list = this.state.employees.map((employee, index) => {
            return <li className="list-group-item d-flex justify-content-between align-items-center" key={index}
                onClick={(e) => this.clickEmployee(employee, this)}>
                {employee.surname + "  " + employee.name + "  " + employee.patronymic}
                <button className="btn badge badge-primary badge-pill" onClick={(e) => this.deleteEmployee(employee.id, this)}>X</button>
            </li>;
        });
        return (
            <div>
                <h2>Список сотрудников</h2>

                <Spinner loading={this.state.isLoading} />

                <ul className="list-group">
                    {list}
                </ul>
                <form onSubmit={this.addEmployee.bind(this)} className="mt-2">
                    <h3>Новый сотрудник:</h3>
                    <div className="form-group">
                        <label>Фамилия</label>
                        <input type="text" className="form-control" value={this.state.newSurname}
                            onChange={this.handleChangeSurname.bind(this)} required />
                        <label>Имя</label>
                        <input type="text" className="form-control" value={this.state.newName}
                            onChange={this.handleChangeName.bind(this)} required />
                        <label>Отчество</label>
                        <input type="text" className="form-control" value={this.state.newPatronymic}
                            onChange={this.handleChangePatronymic.bind(this)} required />
                    </div>

                    <button type="submit" className="btn btn-primary">Добавить</button>
                </form>

                <ShowTable showTable={this.state.showAddPhoneToEmployee}
                    phones={this.state.phones} editedEmployee={this.state.editedEmployee}
                    clickCloseTable={this.clickCloseTable} />

            </div>
        );
    }
};