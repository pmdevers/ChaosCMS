import React, { Component } from 'react';
import PropTypes from 'prop-types';

class Icon extends Component {
    render() {
        var icon = "fa fa-" + this.props.icon;
        return (
            <i className={icon} />
        );
    }
}

Icon.propTypes = {
    icon: PropTypes.string
};

Icon.defaultProps = {
    icon: 'bars'
}


export default Icon;