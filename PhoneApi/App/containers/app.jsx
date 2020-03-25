import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Header from './header.jsx';
import About from './about.jsx';
import Phone from './phone.jsx';
import Cabinet from './cabinet.jsx';
import Home from './home.jsx';
import Employee from './employee.jsx';

export default class App extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <Header />
                    <div className="container">
                        <Switch>
                            <Route path="/about" component={About} />
                            <Route path="/phone" component={Phone} />
                            <Route path="/cabinet" component={Cabinet} />
                            <Route path="/employee" component={Employee} />
                            <Route path="/" component={Home} />
                        </Switch>
                    </div>
                </div>
            </Router>
        );
    }
};