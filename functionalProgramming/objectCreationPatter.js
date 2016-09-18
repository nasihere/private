//contsruc pattern
//factory pattern
//prototype pattern
//dynamic pattern

'use strict'

//Factory Pattern;

//Let's create main object called people factory
let peopleFactory = function(name, age, state) {

	//temp Object

	let temp = {};

	temp.age = age;
	temp.name = name;
	temp.state = state;

	temp.printPerson = function() {
		console.log(this.name, this.age, this.state);
	}
	return temp; // now this will behave like a factory, 

};
// now we call this factory through
var person1 = peopleFactory('nasir', 27, 'CA');
var person2 = peopleFactory('saba', 23, 'NY');
person1.printPerson();
person2.printPerson();

/*
output
Nasirs-MacBook-Pro:functionalProgramming Nasz$ node objectCreationPatter.js 
nasir 27 CA
saba 23 NY
*/

//constructor Pattern
// this will beahve like a class = > more like java => 
var peopleConstructor = function(name, age, state) {
	
	this.name = name;
	this.age = age;
	this.state = state;

	this.printPerson = function() {
		console.log(this.name, this.age, this.state);
	}
}

var person2 = new peopleConstructor('nasir', 27, 'CA');
var person3 = new peopleConstructor('Saba', 23, 'NY');

// in constructor patter we have to deinfed new keyword 

person2.printPerson();
person3.printPerson();

/*

Nasirs-MacBook-Pro:functionalProgramming Nasz$ node objectCreationPatter.js 
nasir 27 CA
saba 23 NY
nasir 27 CA
Saba 23 NY

*/

// whenever we create constructor patter or service pattern it will add printPerson() all the time in the list

//hence the solution of to resolve the error is PROTOTYPE patter




//Example
//PIZZA.getCrust = function() {
//	return this.crust;
//}

//PIZZA.prototype.getCrust = function() {
//	return this.crust;
//}

// var peopleProto = function() {

// };

// peopleProto.prototype.age = 0;
// peopleProto.prototype.name = 'no name';
// peopleProto.prototype.state = 'no state';

// peopleProto.prototype.printPerson = function() {
// 	console.log(this.name, this.age, this.state);
// }
// // we have to use NEW as consturctor pattern
// let person4 = new peopleProto();
// person4.age = 27;
// person4.name = 'Nasir'
// person4.state = 'CA';
// person4.printPerson()



// //Comparison
// let a = {};
// let b = {};
// var compare1 = (a == b);
// console.log(compare1)



//dynamic prototype pattern

var dynamicProto = function(name, age, state) {

	this.name = name;
	this.age = age;
	this.state = state;

	if (typeof this.printPerson !== 'function') {
		dynamicProto.prototype.printPerson = function() {
			console.log(this.name, this.age, this.state);

		}
	}

};

let person5 = new dynamicProto('nasir', 20, 'CA');
person5.printPerson();


let person6 = new dynamicProto('saba', 23, 'SA');
person6.printPerson();


// avoid making printperson now become one time defined whenever we create an object
