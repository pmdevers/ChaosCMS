import React, { Component, PropTypes } from 'react';
import {Card, CardActions, CardHeader, CardMedia, CardTitle, CardText} from 'material-ui/Card';
import ReactDOM from 'react-dom';
import shallowEqual from 'recompose/shallowEqual';



function getStyles(props, context){
    const { 
        desktop,
        maxHeight,
        width,
    } = props;

    const {muiTheme} = context;

    const styles = {
        root: {
            zIndex: muiTheme.zIndex.menu,
            maxHeight: maxHeight,
            overflowY: maxHeight ? 'auto' : null,
        }
    }
}

class Profile extends Component {
    static propTypes = {
        /**
        * If true, the width of the menu will be set automatically
        * according to the widths of its children,
        * using proper keyline increments (64px for desktop,
        * 56px otherwise).
        */
        autoWidth: PropTypes.bool,
        /**
        * The content of the profile. 
        */
        children: PropTypes.node,
        /**
        * If true, the menu item will render with compact desktop styles.
        */
        desktop: PropTypes.bool,

        logo: PropTypes.string,

        avatar: PropTypes.string,

        displayName: PropTypes.string,

        username: PropTypes.string
    };

    static defaultProps = {
        autoWidth: true,
        desktop: false,
        logo: 'http://placehold.it/350x150',
        avatar: 'http://placehold.it/128x128',
        displayName: 'John Doe',
        username: 'john_doe@chaos.com'
    };

    static contextTypes = {
        muiTheme: PropTypes.object.isRequired,
    };

    constructor(props, context){
        super(props, context);
        const filteredChildren = this.getFilteredChildren(props.children);
    }

    componentDidMount() {
        if(this.props.autoWidth) {
            this.setWidth();
        }
    }

    componentWillReceiveProps(nextProps) {
        const filteredChildren = this.getFilteredChildren(nextProps.Children);

        // this.setState({
            
        // });
    }

    shouldComponentUpdate(nextProps, nextState, nextContext){
        return (
            !shallowEqual(this.props, nextProps) ||
            !shallowEqual(this.state, nextState) ||
            !shallowEqual(this.context, nextContext)
        );
    }

    componentDidUpdate(){
        if(this.props.autoWidth) this.setWidth();
    }

    getFilteredChildren(children) {
        const filteredChildren = [];
        React.Children.forEach(children, (child) => {
        if (child) {
            filteredChildren.push(child);
        }
        });
        return filteredChildren;
    }

    setWidth() {
        const el = React.findDOMNode(this);
    }

    render() {

        // const {prepareStyles} = this.context.muiTheme;
        // const styles = getStyles(this.props, this.context);
        // const mergedRootStyles = Object.assign(styles.root, style);

        return (
            <Card>
                <CardMedia>
                    <img src={this.props.logo} />
                </CardMedia>
                <CardHeader
                    title={this.props.displayName}
                    subtitle={this.props.username}
                    avatar={this.props.avatar}
                />
            </Card>
        );
    }
}

export default Profile;