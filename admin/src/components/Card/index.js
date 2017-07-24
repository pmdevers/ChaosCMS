import React, { Component } from 'react';
import PropTypes from 'prop-types';

class Card extends Component {
    constructor(props){ 
        super(props);

    }

    render() {
        let icon = null;
        
        if(this.props.showIcon){
            var iconclass = "fa fa-" + this.props.icon;
            icon = <i className={iconclass}></i>
        }
        
        return (
            <div className="card" style={this.props.style}>
                <div className="card-header">
                    {icon} {this.props.header} 
                </div>
                <div className="card-block">
                    {this.props.children}
                </div>
            </div>
        );
    }
}

Card.propTypes = {
    showIcon: PropTypes.bool,
    icon: PropTypes.string,
    header: PropTypes.string
}

Card.defaultProps = {
    showIcon: true,
    icon: "align-justify",
    header: ""
}

export default Card;