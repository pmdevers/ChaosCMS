import { CREATE_PAGE, UPDATE_PAGE, PATCH_PAGE, FETCH_PAGE, SET_ERRORS, SHOW_ERROR } from './types';
import * as ErrorActions from './errors';
import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

import * as Constants from './constants';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

export function selectPage(page){
    return (dispatch) => {
        return dispatch({type: FETCH_PAGE, page: page });
    }
}

export function createPage(page) {
    return (dispatch) => {
        return fetch(`${Constants.API_ROOT}page`, {
                method: 'post',
                headers: {'content-type': 'application/json'},
                body: JSON.stringify(page)
            }).then(
                response => handleError(dispatch, response)
            ).catch((e) => {
                dispatch(ErrorActions.ShowErrors());
            })
            .then(
                json => dispatch({ type: UPDATE_PAGE, page: json.result})
            );
    }
}

export function updatePage(pageid, page) {
    return (dispatch) => {
        return fetch(`${Constants.API_ROOT}/api/page/${pageid}`, {
                    method: 'patch',
                    headers: {'content-type': 'application/json'},
                    body: JSON.stringify(page)
                }).then(
                    response => handleError(dispatch, response)
                ).catch((e) => {
                    dispatch(ErrorActions.ShowErrors());
                })
                .then(
                    json => dispatch({ type: UPDATE_PAGE, page: json.result})
                );
    }
}

export function getChildren(href){
    return fetch(`${Constants.API_ROOT}${href}`, {
                    method: 'get',
                    headers: {'content-type': 'application/json'},
                }).then( response => response.json());
}

function handleError(dispatch, response){
    const { status, statusText } = response;
       
    if(status < 200 || status > 399){
        response
        .json()
        .then(json => {
            console.log(json);
            dispatch(ErrorActions.SetErrors(json));
            dispatch(ErrorActions.ShowErrors());
        });
        throw Error({
                status,
                statusText,
            });
    }
    return response.json();
}



// export function fetch(path, func){
//     return traverson
//         .from('http://localhost:17706/api')
//         .jsonHal()
//         .follow(path)
//         .getResource(function(error, document, traversal) {
//         if (error) {
//             console.error('No luck :-)', error)
//         } 
//         func(document, traversal);
//     });
// }

// export function fetchFrom(location){
//     return traverson.from('http://localhost:17706' + location)
// }
