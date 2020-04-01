import React from 'react';

function EmployeePhoneList(props) {
    if (props.employeePhones == null) {
        return null;
    }

    const employeePhoneList = props.employeePhones.map((phone) => {
        return <button type="button" className="btn btn-outline-primary m-1"
            key={phone.id} onClick={(e) => props.parent.removePhonefromEmployee(props.editedEmployee, phone.id, props.parent)}>{phone.phoneNumber}</button>;
    });

    return (
        <div className="btn-group-vertical btn-group-lg mx-auto" role="group" aria-label="Basic example">
            {employeePhoneList}
        </div>
    );
}

export default class employeePhonesTable extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            phones: [],
            employeePhones: [],
            isLoading: false,
        };
        this.getEmployeePhones = this.getEmployeePhones.bind(this);
        this.removePhonefromEmployee = this.removePhonefromEmployee.bind(this);
    }

    componentDidMount() {
        this.getEmployeePhones(this.props.editedEmployee.id);
    }

    componentDidUpdate(prevProps) {
        if (this.props.editedEmployee !== prevProps.editedEmployee) {
            this.getEmployeePhones(this.props.editedEmployee.id);
        }
    }

    getPhones() {
        fetch(window.constants.phones)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    phones: data.filter(x => this.state.employeePhones.filter(y => y.id == x.id).length == 0)
                });
            }
            )
    }

    getEmployeePhones(employeeId) {
        fetch(window.constants.employees + "/" + employeeId)
            .then((response) => {
                return response.json();
            }).then((data) => {
                this.setState({
                    employeePhones: data.phones
                });
                this.getPhones();
            }
            )
    }

    addPhoneToEmployee(employee, phoneId) {
        fetch(window.constants.employees + "/" + employee.id + "/" + phoneId, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                phoneId: phoneId,
                employeeId: employee.id
            })
        })
            .then(function (response) {
                return {x: 1};
            }).then((data) => {
                this.getEmployeePhones(employee.id);
            }
            );
        event.preventDefault();
        
    }

    removePhonefromEmployee(employee, phoneId) {
        this.setState({ isLoading: true });
        fetch(window.constants.employees + "/" + employee.id + "/" + phoneId, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            }).then((data) => {
                this.getEmployeePhones(employee.id);
            }
            );
    }

    render() {

        const phoneList = this.state.phones.map((phone) => {
            return <button type="button" className="btn btn-primary ml-1"
                key={phone.id} onClick={(e) => this.addPhoneToEmployee(this.props.editedEmployee, phone.id)}>{phone.phoneNumber}</button>;
        });

        return (
            <div>
                <table className="table table-bordered mt-3">
                    <thead>
                        <tr>
                            <th scope="col">Номера телефона сотрудника {this.props.editedEmployee.surname + "  " + this.props.editedEmployee.name + "  " + this.props.editedEmployee.patronymic}</th>
                            <th scope="col">Добавить номер телефона</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <EmployeePhoneList removePhonefromEmployee={this.removePhonefromEmployee}
                                    employeePhones={this.state.employeePhones}
                                    editedEmployee={this.props.editedEmployee} getEmployeePhones={this.getEmployeePhones}
                                    parent={this} />
                            </td>
                            <td>
                                <div className="btn-group-vertical btn-group-lg mx-auto" role="group" aria-label="Basic example">
                                    {phoneList}
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
