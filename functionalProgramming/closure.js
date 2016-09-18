'use strict'


// any varibale defined outside the function it can be accessable inside the function


// let passed = 3;

// var addTo = ()  => {
// 	let inner = 2;
// 	return passed - inner;
// }

// //var passed = 4;
// console.log(addTo(3))
// //var passed = 4;

// //console.dir(JSON.stringify(addTo.toString(), null, 2))




let addTo = (passed) => {
	var add = function(inner){
		return passed + inner;
	}
	return add;
}

var val = addTo(3);
console.log(val(2)); //'5'
//console.dir(val.toString());


var val = addTo(3)(2);
//console.log(val(2));
console.log(val) // '5'


// it preseve the value and when you need to use for calculation it can use those closure values.
