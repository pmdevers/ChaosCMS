import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

import chaosTheme from './theme/chaosTheme';

import App from './App';
import './index.css';

injectTapEventPlugin();

const muiTheme = getMuiTheme(chaosTheme);

ReactDOM.render(
  <MuiThemeProvider muiTheme={muiTheme}>
    <App />
  </MuiThemeProvider>
  ,
  document.getElementById('root')
);
