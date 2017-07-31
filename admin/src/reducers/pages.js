import * as actions from '../actions/types';

const initialState = {
    page: {},
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
        default:{
            
            return state;
        }
    }
}