import React, { Component } from 'react';
import Divider from 'material-ui/Divider';
import PageTree from '../components/pagetree/pagetree'

import { Grid, Row, Col } from 'react-flexbox-grid';
import {Toolbar, ToolbarGroup, ToolbarSeparator, ToolbarTitle} from 'material-ui/Toolbar';
import TextField from 'material-ui/TextField';
import IconButton from 'material-ui/IconButton';

import Search from 'material-ui/svg-icons/action/search';
import Clear from 'material-ui/svg-icons/content/clear';


class Pages extends Component {

    constructor(props) {
        super(props);

        this.state = {
        value: '',
        };
    }

    handleChange = (event) => {
        this.setState({
        value: event.target.value,
        });
    };

    clearValue = (event) => {
        this.setState({
        value: '',
        });
    }

    render() {
        return (
                <Grid style={{height: "100%"}}>
                    <Row style={{height: "100%"}}>
                        <Col xs={2} md={2} style={{border: "1px solid black", paddingLeft: "25px", height: "100%"}}>
                            <TextField
                                id="text-field-controlled"
                                hintText="Search..."
                                value={this.state.value}
                                onChange={this.handleChange}
                                hasIcon={true}
                                icon={Search}
                                />
                            <PageTree />
                        </Col>
                        <Col xs={10} md={10}>
                            <Toolbar />
                            

                        </Col>
                    </Row>
                </Grid>
            
        );
    }
}

export default Pages;