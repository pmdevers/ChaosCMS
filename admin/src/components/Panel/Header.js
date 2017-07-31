import React, { Component } from 'react';

class PanelHeader extends Component {
    render() {
        return (
            <div className="panel-header">
               {this.props.children}   
            </div>
        );
    }
}

export default PanelHeader;