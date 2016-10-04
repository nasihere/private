import React from 'react';
import {Header} from './Header';
import {Navbar} from './Navbar';

export class Layout extends React.Component{
    render() {
        return (
            <div className='container'>
                <div className='row'>
                    <Header />
                </div>
                <div className='row'>
                    <Navbar />
                </div>
            </div>
        );
    }
}

