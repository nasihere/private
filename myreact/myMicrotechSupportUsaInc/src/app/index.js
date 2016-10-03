import React from 'react';
import { render } from 'react-dom';
import { Router, Route, IndexRoute, browserHistory } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';
import { AboutUs } from './components/AboutUs/AboutUs';
import { ContactUs } from './components/ContactUs/ContactUs';
import { Root } from './components/Root';
import { Services } from './components/Services/Services';


class App extends React.Component{
    render() {
        return (
            <Router history={browserHistory}>
                <Route path={'/'} component={Root}>
                    <IndexRoute component={Home} />
                    <Route path={'aboutUs'} component={AboutUs} />
                    <Route path={'services'} component={Services} />
                    <Route path={'home'} component={Home} />
                    <Route path={'contactUs'} component={ContactUs} />
                </Route>
            </Router>
        );
    }
}
render(<App/>, window.document.getElementById('app'));