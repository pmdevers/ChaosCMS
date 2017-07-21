import React, { Component } from 'react';

class NavTitle extends Component {
    render() {
        return (
            <li className="nav-title">
                {this.props.children}
            </li>
        );
    }
}

export default NavTitle;