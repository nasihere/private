import React from 'react';
import { Link } from 'react-router';
export class Navbar extends React.Component{
    render() {
        return (
           <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <div className="navbar-header">
                        <ul className="nav navbar-nav">
                            <li><Link activeClassName={'active'} to={'/aboutUs'}>About Us</Link></li>
                             <li><Link activeClassName={'active'} to={'/services'}>Services</Link></li>
                            <li><Link activeClassName={'active'} to={'/home'}>Home</Link></li>
                            <li><Link activeClassName={'active'} to={'/contactUs'}>Contact Us</Link></li>
                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}

