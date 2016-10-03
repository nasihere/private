

var ButtonToolbar = ReactBootstrap.ButtonToolbar;
var Button = ReactBootstrap.Button;
var Nav = ReactBootstrap.Nav;
var NavItem = ReactBootstrap.NavItem;
var NavDropdown = ReactBootstrap.NavDropdown;
var MenuItem = ReactBootstrap.MenuItem
const NavDropdownExample = React.createClass({
  handleSelect(eventKey) {
    event.preventDefault();
    alert(`selected ${eventKey}`);
  },

  render() {
    return (
     <Nav bsStyle="tabs" activeKey="1" onSelect={this.handleSelect}>
        <NavItem eventKey="1" href="/home">NavItem 1 content</NavItem>
        <NavItem eventKey="2" title="Item">NavItem 2 content</NavItem>
        <NavItem eventKey="3" disabled>NavItem 3 content</NavItem>
        <NavDropdown eventKey="4" title="Dropdown" id="nav-dropdown">
          <MenuItem eventKey="4.1">Action</MenuItem>
          <MenuItem eventKey="4.2">Another action</MenuItem>
          <MenuItem eventKey="4.3">Something else here</MenuItem>
          <MenuItem divider />
          <MenuItem eventKey="4.4">Separated link</MenuItem>
        </NavDropdown>
      </Nav>
    );
  }
});

ReactDOM.render(<NavDropdownExample />, document.getElementById('hellomessage'));





import React, { Component } from 'react';
class IndexComponent extends Component {
  render() {
    if (this.props.items.length === 0) {
      return (
        <p ref="empty">Index is empty.</p>
      );
    }

    return (
      <section>
        <h2>react-webpack-boilerplate</h2>
        <ul ref="indexList" className="index-list">
          {this.props.items.map((item, index) => {
            return (<li key={index}>item {item}</li>);
          })}
        </ul>
        
        <button className="btn btn-primary">Nasir The great</button>
      </section>
    );
  }
}

IndexComponent.defaultProps = {
  items: []
};

export default IndexComponent;
