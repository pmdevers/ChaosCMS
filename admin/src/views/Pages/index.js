import React, { Component } from 'react';
import { Row, Col, InputGroup, InputGroupAddon, Button } from 'reactstrap';

import { PageTree, Card, Panel, MainPanel, PanelHeader } from '../../components';

import './style.css';

class Pages extends Component {
    constructor(props){
        super(props);

        this.state = {
            isOpen : true
        }

        this.onToggle = this.onToggle.bind(this)
    }
    
    componentWillMount() {         
    };

    getHeader(){
        return (<span>Test</span>);
    }

    onToggle(){
        this.setState(prevState => ({
            isOpen: !prevState.isOpen
        }));
    }

    render(){
        return (
            <div className="pages animated fadeIn">
                <Panel isOpen={this.state.isOpen}>
                    <PanelHeader>
                        <Button onClick={this.onToggle} />
                    </PanelHeader>
                </Panel>
                <MainPanel isOpen={this.state.isOpen}>
                    <PanelHeader>
                        <Button onClick={this.onToggle} >open</Button>
                        <span>Pages</span>
                    </PanelHeader>
                </MainPanel>
            </div>
        );
    }
}

export default Pages;
