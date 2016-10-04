import React from 'react';
export class NewToDo extends React.Component{
    constructor(props) {
        super();
    }
    onChangeToDo(e) {
        const value = e.target.value;
        this.props.ChangeToDo(value);
    }
    onAddToDo() {
        this.props.AddTodo();
    }
    render() {
        return (
             <div className="panel panel-primary">
                <div className="panel-heading"><span>What need to be done?</span></div>
                <div className="panel-body">
                    <div className="input-group">
                        <input value={this.props.value} onChange={(e) => this.onChangeToDo(e) }  type="text" className="form-control" placeholder="Todo" />
                        <span className="input-group-btn">
                            <button className="btn btn-default" onClick={this.onAddToDo.bind(this)} type="button">Add</button>
                        </span>
                    </div>
                </div>
                <div className="panel-footer">
                    Create your new todo item.
                </div>
            </div>
        );
    }
}

