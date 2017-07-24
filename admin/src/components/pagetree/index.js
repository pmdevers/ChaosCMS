import React, { Component } from 'react';
import PropTypes from 'prop-types';

import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

import {fetchFrom} from '../../actions/pages';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

class PageTree extends Component {
    constructor(props){
        super(props);

        this.state = {};
    }
    componentWillMount(){
        var me = this;

        fetchFrom(me.props.from)
        .jsonHal()
        .getResource(function(error, document, traversal) {
            if (error) {
                console.error('No luck :-)', error)
            } 
            me.setState({ page : document });
        });
    }

    render() {
        var result = ""; 
        var me = this.state;

        if(me.page === undefined)
            return (<span></span>);

        return (
            <li>
                <span>{me.page.Name}</span>
                <ul>
                {
                    Object.keys(me.page.Children).map(function(key, index) {
                        console.log(key);

                        var from = "/api/page/" + me.page.Children[key];
                        return (<PageTree key={key} from={from} />);
                    })
                }
                </ul>
            </li>
        );
    }
}

PageTree.propTypes = {
    from:  PropTypes.string
};

export default PageTree;