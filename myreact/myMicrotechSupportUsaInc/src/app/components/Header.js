import React from 'react';

export class Header extends React.Component{
    render() {
        return (
            <div>
                <div clasNames="page-header">
                    <h1>Microtech Support USA <small>We culminate your business</small><i className="fa fa-phone"><small className='pull-right'>+ 858-231-0115</small></i></h1>
                </div>
                <div className="jumbotron">
                    <h1>Microtech Support USA</h1>
                    <p>...</p>
                    <p><a className="btn btn-primary btn-lg" href="#" role="button">Learn more</a></p>
                </div>
            </div>
        );
    }
}

