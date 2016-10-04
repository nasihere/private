import React from 'react';
import { Link } from 'react-router';
export class Header extends React.Component{
    render() {
        return (
            <div className="jumbotron">
                <h1>CredoMobile</h1>
                <p>About the application - Todo's -  Code Test with Nasir Sayed</p>
                <h4>Below is a simple ToDo application that we would like to you build, Following are the user stories</h4>
                <ul>
                    <li>As a user, I should be able to view a page with a task list on it.</li>
                    <li>As a user, I should be able to add a new task to the task list.</li>
                    <li>As a user, I should be able to remove a task from the task list.</li>
                    <li>As a user, I should be able to reorder a task in the task list.</li>
                    <li>As a user, I should be able to edit the name of a task in the task list.</li>
                    <li>As a user, I should be able to mark a task in the list as completed.</li>
                    <p>This is single user application and do not worry about cross-user scenarios or security in that respect.
                    A task is simply defined as a small piece of work with a description and can exist in one of two simple states: completed or not completed. As for the list, consider it empty any time someone first visits your index page; you don't have to worry about saving the state of your application between uses.</p>
                </ul>
                <h4>Architecture</h4>
                    <p>Implement front end with HTML, CSS, and JavaScript required. For this role, we prefer that you use a Templating engine you are familiar with (Example: Handlebars or EJS).</p>
                    <p><s>Implement back end with NodeJs, Express. Data layer can be flat JSON files or MongoDb.</s> Please refer my github repository for Express/LoopBack/DB  </p>
                    <p><s>Use Gulp or Grunt to orchestrate your front end engineering process.</s> Not Required with webpack</p>
                    <p><s>Make sure you have implemented unit tests with your choice of framework (Example: Jasmine or Mocha).</s> Please refer my github repository for UnitTest </p>
                    
                    <p><Link to={"https://bitbucket.org/credomobile/codetest-e1d0be9e/overview"} target={"_blank"} className="btn btn-primary btn-lg" href="#" role="button">Learn more</Link></p>
            </div>
        );
    }
}

