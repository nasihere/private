'use strict'


//Call apply and bind


//Object 1 { properties1, methods1}
//Object 2{ properties1, methods2}

//OR
//Object 1 { properties1, methods = method1}
//Object 2 { properties1, methods = method1}



// var obj = {num:2};

// var addToThis = function(a) {
// 	console.log(this)
// 	return this.num + a;
// }

// let val = addToThis.call(obj, 3); // functionName.call(object, functionArgument);

// console.log(val);




// var obj = {num:2};

// var addToThis = function(a,b,c) {
// 	return this.num + a + b + c;node 
// }

// let val = addToThis.call(obj, 1, 2, 3); // functionName.call(object, functionArgument);

// console.log(val);


// var arr = [1,2,3];
// let apply = addToThis.apply(obj, arr);
// console.log(apply);




// var obj = {num:2};
// var obj2 = {num:5};

// var addToThis = function(a,b,c) {
// 	return this.num + a + b + c;node 
// }

// let val = addToThis.call(obj, 1, 2, 3); // functionName.call(object, functionArgument);

// console.log(val);


// var arr = [1,2,3];
// let apply1 = addToThis.apply(obj, arr);
// let apply2 = addToThis.apply(obj2, arr);
// console.log(apply2);



var obj = {num:2};

var addToThis = function(a,b,c) {
	return this.num + a + b + c;node 
}



var arr = [1,2,3];
let bound = addToThis.bind(obj);
var valBound = bound(1,2,3);
console.log(valBound);

