import React from 'react';
import { Link } from 'react-router';
export class Navbar extends React.Component{
    render() {
        return (
           <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <div className="navbar-header">
                        <ul className="nav navbar-nav">
                            <li className="active" >
                                <Link activeClassName={'active'} to={'/home'}>Home</Link>
                            </li>
                            <li>
                                <Link activeClassName={'active'} to={'/contact'}>Contact</Link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}

