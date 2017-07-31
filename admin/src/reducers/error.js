import * as actions from '../actions/types';

const initialState = {
    errors: [],
    show: false
};

export default function update(state = initialState, action){
    switch(action.type){
        case actions.SET_ERRORS: {
            return { errors: action.errors }
        }
        case action.SHOW_ERRORS: {
            return { show: !state.show }
        }
        default: {
            return state;
        }
    }
}