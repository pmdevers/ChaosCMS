import * as actions from '../actions/types';

const initialState = {
    isOpen: true,
};

export default function update(state = initialState, action){
    switch(action.type){
        case actions.TOGGLE_PANEL: {
            console.log("toggle duck");
            return { isOpen: !state.isOpen }
        }

        default:{
            return state;
        }
    }
}