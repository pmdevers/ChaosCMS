import React, { Component, PropTypes } from 'react';
import ReactDOM from 'react-dom';
import shallowEqual from 'recompose/shallowEqual';

import { Link } from 'react-router-dom';

import Menu from 'material-ui/Menu';
import MenuItem from 'material-ui/MenuItem';
import Divider from 'material-ui/Divider';


import LibraryBooks from 'material-ui/svg-icons/av/library-books';
import Home from 'material-ui/svg-icons/action/home';


class componentName extends Component {
    static propTypes = {

    };

    static defaultProps = {

    };

    static contextTypes = {
        muiTheme: PropTypes.object.isRequired,
    };

    constructor(props, context) {
        super(props, context);
    }

    shouldComponentUpdate(nextProps, nextState, nextContext) {
        return (
            !shallowEqual(this.props, nextProps) ||
            !shallowEqual(this.state, nextState) ||
            !shallowEqual(this.context, nextContext)
        );
    }

    render() {
        return (
            <div >
                <MenuItem primaryText="Dashboard" leftIcon={<Home />} containerElement={<Link to="/" />} />
                <MenuItem primaryText="Pages" leftIcon={<LibraryBooks />} containerElement={<Link to="/pages" />} />
                <MenuItem primaryText="Templates" leftIcon={<LibraryBooks />} containerElement={<Link to="/templates" />} />
                <Divider />
            </div>
        );
    }
}

export default componentName;