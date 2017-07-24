import React, { Component } from 'react';
import PropTypes from 'prop-types';

class MainPanel extends Component {

    constructor(props){
        super(props);
    }

    render() {

        var open = "main-panel " + (this.props.isOpen ? "main-panel-open" : "");

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

export default MainPanel;