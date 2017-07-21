import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Table } from 'reactstrap';


class PageTable extends Component {

    constructor(props){
        super(props);

    }

    render() {
        var title = "Show Page " + this.props.pages.Page + " of " + this.props.pages.TotalPages 
        var items = this.props.pages._embedded.pages.map(
            function(item, i) {
                return (<tr key={item.id}>
                        <th scope="row">{i+1}</th>
                        <td>{item.name}</td>
                    </tr>)
            });

        return (
            <Table>
                <thead>
                    <th>
                        #
                    </th>
                    <th>
                        Name
                    </th>
                </thead>
                <tbody>
                    {items}       
                </tbody>
            </Table>
        );
    
    }
}

PageTable.propTypes = {
    pages: PropTypes.object
};

export default PageTable;