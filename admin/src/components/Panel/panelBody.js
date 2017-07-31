import React, { Component } from 'react';

class PanelBody extends Component {
    render() {
        return (
            <div className="panel-body">
                {this.props.children}
            </div>
        );
    }
}

export default PanelBody;