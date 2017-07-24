import React, { Component } from 'react';
import PropTypes from 'prop-types';

import PanelHeader from './Header';
import MainPanel from './mainpanel';

class Panel extends Component {
    constructor(props){
        super(props);
    }

    render() {
        var open = "panel " + (this.props.isOpen ? "panel-open" : "");
        return (
            <div className={open}>
                {this.props.children}
            </div>
        );
    }
}

MainPanel.propTypes = {
    isOpen: PropTypes.bool,
};

export { Panel, PanelHeader, MainPanel };