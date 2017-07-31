import { createDevTools } from 'redux-devtools';
import LogMonitor from 'redux-devtools-log-monitor';
import DockMonitor from 'redux-devtools-dock-monitor';

import React from 'react';
import ReactDOM from 'react-dom';
import createBrowserHistory from 'history/createBrowserHistory';
import { Router, Route, Switch } from 'react-router-dom';
import { createStore, applyMiddleware, combineReducers } from 'redux';
import thunk from 'redux-thunk'
import { reducer as formReducer } from 'redux-form';
import { Provider } from 'react-redux';
import { createLogger } from 'redux-logger'
import { syncHistoryWithStore, routerReducer, routerMiddleware  } from 'react-router-redux';

// Containers
import { Full } from './containers';
import { pages, sidebar, error } from './reducers';

const browserHistory = createBrowserHistory();
const logger = createLogger();
const reducer = combineReducers({
  error,
  pages, 
  sidebar,
  routing: routerReducer,
  form: formReducer
});

const DevTools = createDevTools(
  <DockMonitor toggleVisibilityKey="ctrl-h" changePositionKey="ctrl-q">
    <LogMonitor theme="tomorrow" preserveScrollTop={false} />
  </DockMonitor>
);

const store = createStore(
  reducer,
  applyMiddleware(thunk, logger)
)
const history = syncHistoryWithStore(browserHistory, store)

ReactDOM.render((
  <Provider store={store}>
    <div>
      <Router history={history}>
        <Switch>
          <Route path="/" name="Home" component={Full}/>
        </Switch>
      </Router>
      <DevTools />
    </div>
  </Provider>
), document.getElementById('root'))
