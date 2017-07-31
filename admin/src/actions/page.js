import { CREATE_PAGE, UPDATE_PAGE, PATCH_PAGE, FETCH_PAGE, SET_ERRORS, SHOW_ERROR } from './types';
import * as ErrorActions from './errors';
import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

const API_ROOT = "http://localhost:17706";

export function createPage(page) {
    return (dispatch) => {
        return fetch(`${API_ROOT}/api/page`, {
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
        console.log(pageid);
        console.log(page);
        return fetch(`${API_ROOT}/api/page/${pageid}`, {
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
