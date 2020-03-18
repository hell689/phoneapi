import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Header from './header.jsx';
import About from './about.jsx';
import Phone from './phone.jsx';

export default class App extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <Header />
                    <main>
                        <Switch>
                            <Route path="/about" component={About} />
                            <Route path="/phone" component={Phone} />
                        </Switch>
                    </main>
                </div>
            </Router>
        );
    }
};