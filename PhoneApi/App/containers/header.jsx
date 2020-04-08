import React from 'react';
import { Link } from 'react-router-dom';
import AuthHelper from './authHelper.jsx';

function LoginUnlogin(props) {
    if (AuthHelper.isLogged()) {
        return AuthHelper.getLogin() + " Выйти";
    } else {
        return <Link className="nav-link active my-2 my-sm-0" to="/login">Войти</Link>;
    }
}

export default class Header extends React.Component {
    render() {
        return (
            <header>
                <nav className="navbar navbar-expand-lg navbar-light bg-light">
                    <a className="navbar-brand" href="/">Телефонный справочник</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav mr-auto">
                            <li className="nav-item active">
                                <Link className="nav-link" to="/" >Главная</Link>
                            </li>
                            <li className="nav-item active">
                                <Link className="nav-link" to="/phone" >Телефоны</Link>
                            </li>
                            <li className="nav-item active">
                                <Link className="nav-link" to="/cabinet" >Кабинеты</Link>
                            </li>
                            <li className="nav-item active">
                                <Link className="nav-link" to="/employee" >Сотрудники</Link>
                            </li>
                            <li className="nav-item active">
                                <Link className="nav-link" to="/about" >info</Link>
                            </li>
                        </ul>
                        
                        <LoginUnlogin />
                    </div>                                      
                </nav>
            </header>
        );
    }
};