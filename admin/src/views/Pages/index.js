import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import jiff from 'jiff';
import { Input, InputGroup, InputGroupAddon } from 'reactstrap';

import { PageTree, Panel,
         PanelBody, PanelHeader, Icon } 
from '../../components';

import PageForm from '../../components/Forms/pageForm';
import * as PageActions from '../../actions/page';
import * as SidebarActions from '../../actions/sidebar';
import update from '../../utils/update';


class Pages extends Component {
    constructor(props){
        super(props);

         this.onSubmit = this.onSubmit.bind(this);
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
                        <PageTree from="/api/page/5970a0751fc20703e823eff5" />
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
        dispatch(SidebarActions.toggleSidebar())
    } ,
    onSubmit: (page) => {
        dispatch(PageActions.createPage(page));
    } ,
    actions: bindActionCreators(Object.assign({}, PageActions, SidebarActions), dispatch) };
  
};

const mapStateToProps = state => ({
  page: state.pages.page,
  isOpen: state.sidebar.isOpen
});

export default connect(mapStateToProps, mapDispatchToProps)(Pages);