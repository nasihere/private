import React from 'react';
import { render } from 'react-dom';
import { Router, Route, IndexRoute, browserHistory } from 'react-router';
import { Layout } from './components/Layout';
import { MyTodo } from './components/MyTodo';
import { Contact } from './components/Contact';
import { Root } from './components/Root';


class App extends React.Component{
    render() {
        return (
            <Router history={browserHistory}>
                <Route path={'/'} component={Root}>
                    <IndexRoute component={MyTodo} />
                    <Route path={'mytodo'} component={MyTodo} />
                    <Route path={'contact'} component={Contact} />
                </Route>
            </Router>
        );
    }
}
render(<App/>, window.document.getElementById('app'));