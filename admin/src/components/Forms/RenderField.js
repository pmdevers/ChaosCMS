import React, { Component } from 'react';
import { FormGroup, Label, Input, FormFeedback, FormText } from 'reactstrap';

class componentName extends Component {
    render() {
        return(
            <FormGroup color="">
                <Label for="pageName">{this.props.label}</Label>
                <Input {...this.props.input} type="text" />
                {this.props.meta.touched && this.props.meta.error && <FormFeedback>{this.props.meta.error}</FormFeedback>}
                <FormText color="muted"> {this.props.de} </FormText>
            </FormGroup>
        );
    }
}

export default componentName;