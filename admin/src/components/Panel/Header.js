import React, { Component } from 'react';
import PropTypes from 'prop-types';

class PanelHeader extends Component {

    getIcon(){
        var icon = "fa fa-" + this.props.Icon;

        if(!this.props.ShowIcon) {
            return "";
        }

        return (<i className={icon} />);
    }

    render() {
        var icon = this.getIcon();
        return (
            <div className="panel-header">
                 {icon} {this.props.children}   
            </div>
        );
    }
}

PanelHeader.propTypes = {
    Icon: PropTypes.string,
    ShowIcon: PropTypes.bool,
};

export default PanelHeader;