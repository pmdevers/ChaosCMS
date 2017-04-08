import React, { Component, PropTypes } from 'react';

class Profile extends Component {

    constructor(props){
        super(props);

        this.state = {
            profile: {}
        }
    }

    componentWillMount(){
         this.getProfile();
    }

    getProfile(){
       var main = this;
       fetch(this.props.url)
            .then(res => res.json())
	        .then(json => main.setState({ profile: json}));
    }

    render() {
        const profile = this.state.profile;
        return (
            <div className="profile"> 
                <h2>{profile.name}</h2>
            </div>
        );
    }
}

Profile.propTypes = {
 url: PropTypes.string
};

export default Profile;