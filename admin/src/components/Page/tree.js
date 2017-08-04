import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { TreeNode } from 'rc-tree';
import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

import * as Constants from '../../actions/constants';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

class PageTree extends Component {
    constructor(props){
        super(props);

        this.state = {
            page: { id: "", name: ""},
            children: []
        };
    }

    componentDidMount() {
        let _this = this;

        console.log(`${Constants.API_ROOT}${this.props.href}`);

        traverson
            .from(`${Constants.API_ROOT}${this.props.href}`)
            .jsonHal()
            .getResource((error, document, traversal) => {
                const page = document
                console.log(document);

                _this.setState({page: page});
                //traversal.continue().follow('children').getResource()
            });
    }

    render() {

        //const nodes = this.state.children_link.children.map((item) => {
        //    <PageTree href={item.href} title={item.title} />
        //})

        return (
            <TreeNode title={this.state.page.name}></TreeNode>
        );
    }
}
 
PageTree.propTypes = {
    href: PropTypes.string,
    title: PropTypes.string,
    filterTreeNode: PropTypes.func
};

export default PageTree;