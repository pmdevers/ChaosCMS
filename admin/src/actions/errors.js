import * as types from './types';

export function SetErrors(errors){
    return {
        type: types.SET_ERRORS,
        errors: errors
    }
}

export function ShowErrors(){
    return {
        type: types.SHOW_ERROR
    }
}