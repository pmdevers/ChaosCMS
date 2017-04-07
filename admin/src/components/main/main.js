import React, { Component } from 'react';

class Main extends Component {
    
    render() {
        return (
            <div id={this.props.id} className={this.props.className} style={this.props.styles}>
                {this.props.children}
            </div>
        );
    }
}

Main.propTypes = {
    id: React.PropTypes.string,
    className: React.PropTypes.string,
    styles: React.PropTypes.object
};

Main.defaultProps = {
  id: '',
  onStateChange: () => {},
  styles: {}
}

export default Main;