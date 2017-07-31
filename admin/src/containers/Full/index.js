import React, { Component } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom'

import { Header, Sidebar, Breadcrumb, Aside, Footer } from '../../components';
import { Dashboard, Pages } from '../../views'

class Full extends Component {

  render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props}/>
          <main className="main">
            <Breadcrumb />
            <Switch>
              <Route path="/dashboard" name="Dashboard" component={Dashboard}/>
              <Route path="/pages" name="Pages" component={Pages}/>
              <Redirect from="/" to="/dashboard"/>
            </Switch>
          </main>
          <Aside>
          </Aside>
        </div>
        <Footer />
      </div>
    );
  }
}

export default Full;
