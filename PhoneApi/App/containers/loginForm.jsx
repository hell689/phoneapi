import React from 'react';
import AuthHelper from './authHelper.jsx';

export default class LoginForm extends React.Component {
    constructor() {
        super();
        this.state = {
            login: "",
            password: ""
        }
    }

    handleChangeLogin(event) {
        let login = event.target.value;
        this.setState({ login: login });
    }

    handleChangePassword(event) {
        let password = event.target.value;
        this.setState({ password: password });
    }

    sendForm(event) {
        event.preventDefault();
        fetch(constants.token + '/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: JSON.stringify({
                login: this.state.login,
                password: this.state.password
            })
        }).then((response) => {
            return response.json();
        }).then((data) => {
            AuthHelper.saveAuth(data.login, data.access_token);
        }).catch((ex) => {
            alert(ex);
        })

        this.forceUpdate();
    }
    

    render() {
        return (
            <form onSubmit={this.sendForm.bind(this)}>
                <div className="form-group">
                    <label>Логин</label>
                    <input type="text" className="form-control" id="loginInput" value={this.state.login} required
                        onChange={this.handleChangeLogin.bind(this)} />
                    <small className="form-text text-muted">Напр. admin</small>
                </div>
                <div className="form-group">
                    <label>Пароль</label>
                <input type="password" className="form-control" id="passwordInput" value={this.state.password} required
                        onChange={this.handleChangePassword.bind(this)} />
                    <small className="form-text text-muted">Напр. 123</small>
                </div>
                <button type="submit" className="btn btn-primary">Отправить</button>
            </form>
            );
    }
}