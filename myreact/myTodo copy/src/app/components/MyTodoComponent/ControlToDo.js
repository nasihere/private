import React from 'react';
export class ControlToDo extends React.Component{
    constructor() {
        super();
       
    }
    onReOrder() {
        this.props.ReOrder();
    }
    onRemove() {
        this.props.Remove();
    }
    onCompleted() {
        this.props.Completed();
    }
    render() {
        return (
            <div className="panel-footer">
                <div className="btn-group">
                    <button type="button" onClick={this.onRemove.bind(this)} className="btn btn-default">Remove</button>
                    <button type="button" onClick={this.onReOrder.bind(this)} className="btn btn-default">Re-Order</button>
                    <button type="button" onClick={this.onCompleted.bind(this)} className="btn btn-default">Completed</button>
                </div>
            </div>
        );
    }
}

