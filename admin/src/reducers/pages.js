import * as actions from '../actions/types';

const initialState = {
    page: {},
    selKey: ""
};

export default function update(state = initialState, action){
    switch(action.type){
        case actions.CREATE_PAGE: {
            return { page: action.page }
        }
        case actions.UPDATE_PAGE: {
            return { page: action.page }
        }
        case actions.PATCH_PAGE: {
            return state;
        }
        case action.FETCH_PAGE: {
            return { selKey: action.page }
        }
        default:{
            
            return state;
        }
    }
}