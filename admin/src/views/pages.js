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
                <div style={{width: "276px", marginLeft: "20px", borderRight: "1px solid black", bottom: 0, top: "65px", position: "fixed" }}>
                            <TextField
                                id="text-field-controlled"
                                hintText="Search..."
                                value={this.state.value}
                                onChange={this.handleChange}
                                hasIcon={true}
                                icon={Search}
                                />
                                <PageTree />
                    </div>

                    
                            

                
            
        );
    }
}

export default Pages;