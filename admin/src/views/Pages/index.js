import React, { Component } from 'react';
import { Table, Row, Col } from 'reactstrap';

import { Card, PagesTable, PageTree } from '../../components';
import { fetch, fetchFrom } from '../../actions/pages';

class Pages extends Component {
    constructor(props){
        super(props);
    }
    
    componentWillMount() {         
    };

    render(){
        return (
            <div className="pages animated fadeIn">
                <Row>
                    <Col xs="2">
                        <PageTree from="/api/page/5970a0751fc20703e823eff5" />
                    </Col>
                    <Col xs="10">
                        
                    </Col>
                </Row>
            </div>
        );
    }
}

export default Pages;
