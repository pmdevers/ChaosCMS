import React, { Component } from 'react';
import PropTypes from 'prop-types';

import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

import {fetchFrom} from '../../actions/pages';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

class PageTree extends Component {
    constructor(props){
        super(props);

        this.state = { page : {
                "id": {},
                "name": "Home",
                "statusCode": 200,
                "url": "/",
                "pageType": "Default",
                "template": "Index",
                "createdBy": null,
                "modifiedBy": null,
                "modifiedDate": "2017-07-20T14:22:13.902+02:00",
                "Children" : [],
                "_links": {
                    "self": {
                        "href": "/api/page/5970a0751fc20703e823eff5"
                    },
                    "content": {
                        "href": "/api/page/5970a0751fc20703e823eff5/content"
                    },
                    "ac:copy": {
                        "href": "/api/page/5970a0751fc20703e823eff5/copy"
                    },
                    "ac:publish": {
                        "href": "/api/page/5970a0751fc20703e823eff5/publish"
                    }
                }
            }
        }
    }
    componentWillMount(){
        var me = this;

        fetchFrom(me.props.from, "", (err, document, traverson) => {
            console.log(document);
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