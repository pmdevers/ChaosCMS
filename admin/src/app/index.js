//imports external
import React, { Component } from 'react';
import { Container } from 'reactstrap';

import { Header } from '../components';
import './app.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.state={}
  }

  render() {
    return (
        <div className="app">
          <Header />
          <Container fluid={true}>
          {this.props.children}
          </Container>
        </div>
    );
  }
}

export default App;