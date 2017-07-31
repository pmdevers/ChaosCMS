import React, { Component } from 'react';
import CardHeader from './header';
import CardBody from './'


class Card extends Component {
    render() {        
        return (
            <div className="card" style={this.props.style}>
                {this.props.children}
            </div>
        );
    }
}

export { Card, CardHeader, CardBody };