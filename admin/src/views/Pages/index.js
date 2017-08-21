import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import jiff from 'jiff';
import { Input, InputGroup, InputGroupAddon } from 'reactstrap';
import { push } from 'react-router-redux';

import Tree, { TreeNode } from 'rc-tree';

import 
    { Panel, PanelBody, PanelHeader, Icon } 
from '../../components';

import PageForm from '../../components/Forms/pageForm';
import * as PageActions from '../../actions/page';
import * as SidebarActions from '../../actions/sidebar';
import update from '../../utils/update';

class Pages extends Component {
    
    constructor(props){
        super(props);

         this.onSubmit = this.onSubmit.bind(this);
         this.onEdit = this.onEdit.bind(this);
         
    }

    onSubmit(e){
        var patch = jiff.diff(this.props.page, e);
        if(this.props.page.id === undefined){
            let result = jiff.patch(patch, this.props.page);
            console.log(result);
            this.props.actions.createPage(result);
        } else {
            this.props.actions.updatePage(this.props.page.id, patch);
        }
        //
    }

    render(){
        const { actions } = this.props;

        
        //  const customLabel = (<span className="cus-label">
        //     <span>operations: </span>
        //     <span style={{ color: 'blue' }} onClick={this.onEdit}>Edit</span>&nbsp;
        //     <label onClick={(e) => e.stopPropagation()}><input type="checkbox" /> checked</label> &nbsp;
        //     <span style={{ color: 'red' }} onClick={this.onDel}>Delete</span>
        //     </span>);

        return (
            <div className="pages animated fadeIn">
                <Panel name="tree" isOpen={this.props.isOpen}>
                    <PanelHeader>
                        <InputGroup>
                            <Input type="text"/>
                            <InputGroupAddon>
                                <Icon icon="search"/>
                            </InputGroupAddon>
                        </InputGroup>
                    </PanelHeader>
                    <PanelBody>
                          
                    </PanelBody>
                </Panel>
                <Panel name="mainPanel" isOpen={this.props.isOpen}>
                    <PanelHeader>
                        <a onClick={actions.toggleSidebar}>
                            <Icon icon="bars animated" /> {this.props.page.Name}
                        </a>                      
                    </PanelHeader>
                    <PanelBody>
                        <PageForm onSubmit={this.onSubmit} />
                    </PanelBody>
                </Panel>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
  return { 
    onToggle: () => {
        dispatch(SidebarActions.toggleSidebar());
    } ,
    onSubmit: (page) => {
        dispatch(PageActions.createPage(page));
    } ,
    onSelect: (selectedKeys, info) => {
        console.log('selected', selectedKeys, info);
        return PageActions.selectPage(info.node.props.eventKey);
        //dispatch(push(`/page/${info.node.props.eventKey}`));  
    },
    actions: bindActionCreators(Object.assign({}, PageActions, SidebarActions), dispatch) };
  
};

const mapStateToProps = state => ({
  page: state.pages.page,
  selKey: state.pages.selKey,
  isOpen: state.sidebar.isOpen,
});

export default connect(mapStateToProps, mapDispatchToProps)(Pages);