'use strict'

/*
	Google/ MSFT/ FF/  - JAVASCRIPT 
	brendan - author jS
	skim herritage,
	higher order 
*/

//Basic Object



// let dog = {
// 	sound: 'woof',
// 	talk:function()  {
// 		console.log(this.sound);
// 	}
// }

// dog.talk(); // 'woof'



// let dog = {
// 	sound: 'woof',
// 	talk: () =>  {
// 		console.log(this.sound);
// 	}
// }

// dog.talk(); // 'undefined'





let dog = {
	sound: 'woof',
	talk: function() {
		console.log(this.sound);
	}
}
dog.talk(); // 'undefined'
let talkFunction = dog.talk;
let boundFunction = talkFunction.bind(dog);
boundFunction(); // 'woof'
