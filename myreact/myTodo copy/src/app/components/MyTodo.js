import React from 'react';
import { ListTodo } from './MyTodoComponent/ListTodo';
import { NewToDo } from './MyTodoComponent/NewToDo';
import { ControlToDo } from './MyTodoComponent/ControlToDo';


// 
const TASK_UNSELECTED = false;
const TASK_SELECTED = true;

const TASK_NOTCOMPLETED = false;
const TASK_COMPLETED = true;
let _ListToDo = [];
_ListToDo.push({
    task: "Buy Beer for Cat",
    flag: TASK_SELECTED,
    completed: TASK_NOTCOMPLETED
});
_ListToDo.push({
    task: "Buy Laptop From BlueMix",
    flag: TASK_UNSELECTED,
    completed: TASK_NOTCOMPLETED
});
_ListToDo.push({
    task: "Code Deploy!",
    flag: TASK_UNSELECTED,
    completed: TASK_NOTCOMPLETED
});

export class MyTodo extends React.Component{
    
    constructor() {
        super();
        this.state = {
            Default: 'write new todo',
            ListToDo: _ListToDo
        };
    }
    onChangeTodo(Default) {
        this.setState({Default});
    }
    onFlagTodo(index, item, newValue) {
        let newListTodo = this.state.ListToDo.concat();
        newListTodo[index].flag = newValue;
        this.setState({ListToDo: newListTodo});
    }
    onReOrder() {
        let newListToDo = this.state.ListToDo.sort((a, b) => {
            if (a.flag > b.flag) {
                return 1;
            }
            if (a.flag < b.flag) {
                return -1;
            }
            // a must be equal to b
            return 0;
        });
        this.setState({ListToDo: newListToDo});
    }
    onRemove() {
        let newListTodo = this.state.ListToDo.filter(x=>{
           return x.flag === TASK_UNSELECTED 
        });
        console.log(newListTodo)
        this.setState({ListToDo: newListTodo});
    }
    onCompleted() {
        let newListTodo = this.state.ListToDo.map(x=>{
           if (x.flag === TASK_SELECTED) {
               x.completed = !x.completed;
           } 
           return x;
        });
        this.setState({ListToDo: newListTodo});
    }
    onEditToDo(index, item, newValue) {
        let newListTodo = this.state.ListToDo.concat();
        newListTodo[index].task = newValue;
        this.setState({ListToDo: newListTodo});
    }
    onAddTodo() {
        let newElement = {
            task: this.state.Default,
            flag: TASK_UNSELECTED,
            completed: TASK_NOTCOMPLETED
        };
        this.setState({
                ListToDo: this.state.ListToDo.concat([newElement])
        });
    }
    render() {
        var itemsToDo = this.state.ListToDo.map((item,i) => {
            return <ListTodo 
                key={i}
                Index={i}
                FlagTodo={this.onFlagTodo.bind(this)} 
                Source={item} 
                EditToDo={this.onEditToDo.bind(this)} 
                Completed={item.completed}
                Flag={item.flag} Text={item.task} />;
        })
        return (
            <div>
                <h1>ToDo Component</h1>
                <div className="row">
                    <div className="col-lg-6">
                        <NewToDo
                                AddTodo={this.onAddTodo.bind(this)} 
                                ChangeToDo={this.onChangeTodo.bind(this)} 
                                value={this.state.Default} />
                    </div>
                   
                   <div className="col-lg-6">
                        <div className="panel panel-primary">
                            <div className="panel-heading"><span>List of Todo</span></div>
                            <div className="panel-body">
                               
                               <ul className="list-group">
                                    {itemsToDo}
                                </ul>

                            </div>
                            <ControlToDo 
                                ReOrder={this.onReOrder.bind(this)}
                                Remove={this.onRemove.bind(this)}
                                Completed={this.onCompleted.bind(this)}
                            />
                        </div>
                    </div>

                </div>

            </div>
        );
    }
}


