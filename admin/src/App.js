import React, { Component } from 'react';

import { BrowserRouter  as Router, Route, Link } from 'react-router-dom';

import AppBar from 'material-ui/AppBar';
import Drawer from 'material-ui/Drawer';
import Profile from './components/profile/profile';
import Menu from './components/menu/menu';
import Main from './components/main/main';
import './App.css';
import Logo from './assets/logo.png';


class App extends Component {

 constructor(props){
   super(props);

   this.state = {
     isOpen: true
   };
  
   this.toggleDrawer = this.toggleDrawer.bind(this)
 }

toggleDrawer(){
  this.setState((prevState, props) => ({
      isOpen: !prevState.isOpen
  }));
}

 menuStyle(isOpen) {
   return {
     heigth: "100%",
     marginLeft: isOpen ? "256px": "0px",
     transition: "transform 450ms cubic-bezier(0.23, 1, 0.32, 1) 0ms",
   };
 }

 render() {
    return (
      <Router basename="/admin">
        <div>
            <div style={this.menuStyle(this.state.isOpen)}>
            <AppBar title="Chaos Admin"
              iconClassNameRight="muidocs-icon-navigation-expand-more"
              onLeftIconButtonTouchTap={this.toggleDrawer} />
              <Main />
            </div>
            <Drawer docked={this.state.isOpen}>
                <Profile username="Patrick" logo={Logo} />
                <Menu />
              </Drawer>
          </div>
      </Router>
    );
  }
}

export default App;
