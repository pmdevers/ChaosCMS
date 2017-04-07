import React, { Component } from 'react';
import pkg from '../package.json';
import { BrowserRouter as Router, Route,  Link } from 'react-router-dom';
import Sidebar from './components/sidebar/sidebar';
import Profile from './components/profile/profile';
import Main from './components/main/main';
import Home from './components/home/home';
import Page from './components/page/page';
import './App.scss';

class App extends Component {

  constructor()
  {
    super();
    this.state = {
      sidebarOpen: true,
      sidebarDocked: true,
      profile: "http://localhost:17706/api/profile"
    };

    this.toggleSidebar = this.toggleSidebar.bind(this);
  }

  onSetSidebarOpen(open){
    this.setState({sidebarOpen: open});
  }

  toggleSidebar() {
    this.setState(function(prevState, props) {
      return {
        sidebarOpen: !prevState.sidebarOpen
      };
    });
  }

  componentWillMount(){
    var mql = window.matchMedia(`(min-width: 800px)`);
    mql.addListener(this.mediaQueryChanged);
    this.setState({mql: mql, sidebarDocked: mql.matches});
  }

  componentWillUnmount(){
    this.state.mql.removeListener(this.mediaQueryChanged);
  }

  mediaQueryChanged(){
    this.setState({sidebarDocked: this.state.mql.matches});
  }

  mainStyle(isOpen, right){
    return {
        position: 'fixed',
        zIndex: 2,
        marginLeft: right ? 0: '300px',
        marginRight: right ? '300px' : 0,
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
      <Router>
        <div>
          <Sidebar id="sideBar" isOpen={this.state.sidebarOpen} className="mainmenu" outerContainerId="mainPage">
            <Profile url={this.state.profile} />
            <div className="menuItems">
              <ul>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/page">Page</Link></li>
              </ul>
            </div>
          </Sidebar>
          <Main id="mainPage" className="container-fluid main-content" styles={this.mainStyle(this.state.sidebarOpen, false)}>
            
            <div className="row">
                <button onClick={this.toggleSidebar}>toggle</button>
                <span>{pkg.version}</span>
            </div>
            <Route exact path="/" component={Home}/>
            <Route exact path="/page" component={Page}/>
          </Main>
      </div>
    </Router>
    );
  }
}

export default App;
