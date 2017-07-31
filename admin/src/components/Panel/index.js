import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PanelHeader from './Header';
import PanelBody from './panelBody';

class Panel extends Component {
    render() {
        var open = this.props.name + " panel " + (this.props.isOpen ? "panel-open" : "");
        return (
            <div className={open}>
                {this.props.children}
            </div>
        );
    }
}

Panel.propTypes = {
    name: PropTypes.string,
    isOpen: PropTypes.bool,
};

export { Panel, PanelHeader, PanelBody };