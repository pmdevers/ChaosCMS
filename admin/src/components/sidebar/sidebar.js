import React, { Component } from 'react';

class Sidebar extends Component {

  constructor(props){
    super(props);
    this.state = { isOpen: false };
    this.toggleMenu = this.toggleMenu.bind(this)
  }

  toggleMenu(e){
    this.setState(function(prevState, props) {
      return {
        isOpen: !prevState.isOpen
      };
    });
  }

  applyWrapperStyles(){
    const html = document.querySelector('html');
    const body = document.querySelector('body');
    
    const wrapper = document.getElementById(this.props.outerContainerId);
    
    var builtStyles = this.outerContainer(this.props.right, this.state.isOpen);

    for (const prop in builtStyles) {
      if (builtStyles.hasOwnProperty(prop)) {
        wrapper.style[prop] = builtStyles[prop];
      }
    }
    // Prevent any horizontal scroll.
      [html, body].forEach((element) => {
        element.style['overflow-x'] = 'hidden';
      });
  }

  listenForClose(e){
    e = e || window.event;

    if(this.state.isOpen && (e.key === 'Escape' || e.keyCode === 27)){
      this.toggleMenu();
    }
  }

  componentWillMount() {
     if(this.props.isOpen) {
       this.toggleMenu();
     }
  }

  componentWillUnmount() {
    window.onkeydown = null;
  }

  componentWillReceiveProps(nextProps) {
    if(typeof nextProps.isOpen !== 'undefined' && nextProps.isOpen !== this.state.isOpen) {
      this.toggleMenu();
    }
  }

  outerContainer(right, isOpen) {
    return {
        position: 'fixed',
        zIndex: 2,
        'margin-left': right ? 0: '300px',
        'margin-right': right ? '300px' : 0,
        height: '100%',
        transform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
        MozTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
        MsTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
        OTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
        WebkitTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
        transition: 'all 0.5s'
      };
  }

  menu() {
    return {
      height: '100%',
      boxSizing: 'border-box',
      overflow: 'auto'
    };
  }

  itemList() {
    return {
      height: '100%'
    };
  }

  item() {
    return {
      display: 'block',
      outline: 'none'
    };
  }

  menuWrap(isOpen, width, right) {
    return {
      position: 'fixed',
      right: right ? 0 : 'inherit',
      zIndex: 2,
      width,
      height: '100%',
      transform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
      MozTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
      MsTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
      OTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
      WebkitTransform: isOpen ? '' : right ? 'translate3d(100%, 0, 0)' : 'translate3d(-100%, 0, 0)',
      transition: 'all 0.5s'
    };
  }

  render() {

    return (
      <div>
        <div id={this.props.id} className={`sidebar ${this.props.className}`} style={this.menuWrap(this.state.isOpen, this.props.width)}> 
          <div className="sidebar-inner"  style={this.menu()}>
            <nav className="sidebar-items" style={this.itemList()}>
              {React.Children.map(this.props.children, (item, index) => {
                    if (item) {
                      const extraProps = {
                        key: index,
                        style: this.item()
                      };
                      return React.cloneElement(item, extraProps);
                    }
                  })}
            </nav>
          </div>
        </div>
      </div>
    );
  }
}

Sidebar.propTypes = {
   className: React.PropTypes.string,
   burgerIcon: React.PropTypes.oneOfType([React.PropTypes.element, React.PropTypes.oneOf([false])]),
   crossIcon:  React.PropTypes.oneOfType([React.PropTypes.element, React.PropTypes.oneOf([false])]),
   id: React.PropTypes.string,
   outerContainerId: React.PropTypes.string,
   isOpen: React.PropTypes.bool,
   onStateChange: React.PropTypes.func,
   right: React.PropTypes.bool,
   styles: React.PropTypes.object,
   width: React.PropTypes.number
};

Sidebar.defaultProps = {
  id: '',
  //noOverlay: false,
  onStateChange: () => {},
  outerContainerId: '',
  //pageWrapId: '',
  styles: {},
  width: 300
}

export default Sidebar;