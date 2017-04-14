import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import Paper from 'material-ui/Paper';

import Dashboard from '../../views/dashboard';
import Pages from '../../views/pages';

const style = {
    margin: 20
}

class componentName extends Component {
    render() {
        return (
            <div style={{bottom: 0, top: 65, position: "fixed" }}>
                <Route exact path="/" component={Dashboard}/>
                <Route exact path="/pages" component={Pages}/>
            </div>
        );
    }
}

export default componentName;