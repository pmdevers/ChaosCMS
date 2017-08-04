import React from 'react';
import ReactDOM from 'react-dom';

import { createStore, combineReducers, applyMiddleware } from 'redux';
import { Provider  } from 'react-redux';

import createHistory from 'history/createBrowserHistory';
import { Router, Route, Switch } from 'react-router';

import { createLogger } from 'redux-logger'

import { routerReducer, routerMiddleware } from 'react-router-redux';

import reducers from './reducers';
import { Full } from './containers';

const history = createHistory();
const middleware = routerMiddleware(history);
const logger = createLogger();

const store = createStore(
  combineReducers({
    ...reducers,
    router: routerReducer
  }),
  applyMiddleware(logger, middleware)
)

ReactDOM.render(( 
  <Provider store={store}> 
    <div> 
      <Router history={history}>
        <Switch> 
          <Route path="/" name="Home" component={Full}/> 
        </Switch> 
      </Router> 
    </div> 
  </Provider> 
), document.getElementById('root')) 