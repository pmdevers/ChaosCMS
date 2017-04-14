import React, { Component, PropTypes } from 'react';
import {List, ListItem} from 'material-ui/List';

import Home from 'material-ui/svg-icons/action/home';
import Description from 'material-ui/svg-icons/action/description';

class PageTree extends Component {
    static propTypes = {
        url: PropTypes.string
    }

    static defaultProps = {
        url: '/api/pages'
    }

    constructor(props){
        super(props);
    }



    render() {
        return (
                            
                <List style={{height: "100%"}}>
                    <ListItem key={1}
                    primaryText="Home"
                    leftIcon={<Home />}
                    initiallyOpen={true} 
                    primaryTogglesNestedList={true}
                    nestedItems={[
                        <ListItem key={2} 
                            primaryText="SubPage-1"
                            leftIcon={<Description />}
                            initialyOpen={true}
                        />,
                        <ListItem key={3} 
                            primaryText="SubPage-2"
                            leftIcon={<Description />}
                            initialyOpen={true}
                        />,
                        <ListItem key={4} 
                            primaryText="SubPage-3"
                            leftIcon={<Description />}
                            initialyOpen={true}
                        />,
                        <ListItem key={5} 
                            primaryText="SubPage-4"
                            leftIcon={<Description />}
                            initialyOpen={true}
                             nestedItems={[
                                <ListItem key={2} 
                                    primaryText="SubPage-1"
                                    leftIcon={<Description />}
                                    initialyOpen={true}
                                />,
                                <ListItem key={3} 
                                    primaryText="SubPage-2"
                                    leftIcon={<Description />}
                                    initialyOpen={true}
                                />,
                                <ListItem key={4} 
                                    primaryText="SubPage-3"
                                    leftIcon={<Description />}
                                    initialyOpen={true}
                                />,
                                <ListItem key={5} 
                                    primaryText="SubPage-4"
                                    leftIcon={<Description />}
                                    initialyOpen={true}
                                />
                            ]}
                        />
                    ]}
                    />
                </List>
        );
    }
}

export default PageTree;
