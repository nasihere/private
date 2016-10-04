import React from 'react';
export class ListTodo extends React.Component{
    onEditToDo(event) {
        const newValue = event.target.value;
        this.props.EditToDo(this.props.Index, this.props.Source, newValue);
    }
    onChangeCheckbox(event) {
        const newCheck = !this.props.Flag;
        this.props.FlagTodo(this.props.Index, this.props.Source, newCheck);
    }

    render() {
        let inputToDo = null;
        if (this.props.Completed) {
            inputToDo = <span className="input-group-addon"><s>{this.props.Text}</s></span>
        }  
        else {
            inputToDo = <input className='form-control' type="text" 
                        value={this.props.Text} onChange={(e) => this.onEditToDo(e)} />
        }
                        
        return (
             <li className="list-group-item">
                <div className="input-group">
                    <span className="input-group-addon">
                       
                        <input  
                            onChange={(e) => this.onChangeCheckbox(e)} 
                            checked={this.props.Flag} type="checkbox" />
                    </span>
                    {inputToDo}
                </div>
            </li>
        );
    }
}

