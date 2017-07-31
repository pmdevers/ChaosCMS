import React, { Component } from 'react';
import PropTypes from 'prop-types';
import Icon from './Icon';

class StateIcon extends Component {
    render() {
        if(this.props.active){
            return (<Icon icon={this.props.activeIcon} />);
        }
        return (<Icon icon={this.props.inactiveIcon} />);
    }
}

StateIcon.propTypes = {
    active: PropTypes.bool,
    activeIcon: PropTypes.string,
    inactiveIcon: PropTypes.string,
};

StateIcon.defaultProps = {
    active: true,
    activeIcon: "minus",
    inactiveIcon: "cross"
}

export default StateIcon;