import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Field, reduxForm } from 'redux-form'
import { Button } from 'reactstrap';

import RenderField from './RenderField';

class PageForm extends Component {
        
    render() {
        const { handleSubmit, errors } = this.props
        return (
            <form onSubmit={ handleSubmit }>
                <Field name="Name" label="Page Name" component={RenderField} type="text" description="" />
                <Field name="Url" label="Url" component={RenderField} type="text"  description="" />                
                <Field name="Template" label="Template" component={RenderField} type="text"  description="" /> 
                <Field name="PageType" label="Type" component={RenderField} type="text"  description="" /> 
                <Field name="StatusCode" label="StatusCode" component={RenderField} type="text"  description="" /> 
                <Button type="submit" >Save</Button>
            </form>
        );
    }
}

PageForm = reduxForm({
  // a unique name for the form
  form: 'contact'
})(PageForm)

function mapDispatchToProps(dispatch){
    return { actions: bindActionCreators(Object.assign({
    }), dispatch) };
}

const mapStateToProps = state => ({
    initialValues: state.pages.page,
    errors: state.errors
});

export default connect(mapStateToProps, mapDispatchToProps)(PageForm);