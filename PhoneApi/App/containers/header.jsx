import React from 'react';
import { Link } from 'react-router-dom';

export default class Header extends React.Component {
    render() {
        return (
            <header>
                <menu>
                    <ul>
                        <li>
                            <Link to="/">Главная</Link>
                        </li>
                        <li>
                            <Link to="/phone">Телефоны</Link>
                        </li>
                        <li>
                            <Link to="/about">info</Link>
                        </li>
                    </ul>
                </menu>
            </header>
        );
    }
};