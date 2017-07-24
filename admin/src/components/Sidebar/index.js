import React, { Component } from 'react';
import { NavLink } from 'react-router-dom'
import { NavItem, Nav } from 'reactstrap';
import { NavTitle } from '../';

class Sidebar extends Component {

  handleClick(e) {
    e.preventDefault();
    e.target.parentElement.classList.toggle('open');
  }

  activeRoute(routeName) {
    return this.props.location.pathname.indexOf(routeName) > -1 ? 'nav-item nav-dropdown open' : 'nav-item nav-dropdown';
  }

  render() {
    return (
      <div className="sidebar">
        <nav className="sidebar-nav">
          <Nav>
            <NavItem>
              <NavLink to={'/dashboard'} className="nav-link" activeClassName="active"><i className="icon-speedometer"></i> Dashboard </NavLink>
            </NavItem>
            <NavTitle>
              Content Test
            </NavTitle>
            <NavItem>
              <NavLink to={'/pages'} className="nav-link" activeClassName="active"><i className="icon-directions"></i> Pages</NavLink>
            </NavItem>
          </Nav>
        </nav>
      </div>
    )
  }
}

export default Sidebar;
