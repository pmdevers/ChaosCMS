import React, { Component } from 'react';
import { Row, Col, InputGroup, InputGroupAddon, Input } from 'reactstrap';

import { PageTree, Card } from '../../components';

import './style.css';

class Pages extends Component {
    constructor(props){
        super(props);
    }
    
    componentWillMount() {         
    };

    headerSearchBox(){
        return (
            <InputGroup>
                <Input placeholder="Search..." />
                <InputGroupAddon><i className="fa fa-search" /></InputGroupAddon>
            </InputGroup>
        );
    };

    render(){
        return (
            <div className="pages animated fadeIn">
                <Row className="leftTree">
                    <Col xs="2">
                        <Card showIcon={false} header={this.headerSearchBox()} style={{height: "100%"}}>
                            <PageTree from="/api/page/5970a0751fc20703e823eff5"  />
                        </Card>
                    </Col>
                    <Col xs="10">
                        <h1>Pages</h1>
                    </Col>
                </Row>
            </div>
        );
    }
}

export default Pages;
