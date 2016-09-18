'use strict'

//#recurrsion
let abc = 'nasir';

let callme = (num) => {
	if (num === 0) return;
	console.log(num);
	callme(num - 1);
}

callme(10);

//#recurrsion


let categories = [
	{id:'animal', 'parent': null},
	{id:'mamals', 'parent': 'animal'},
	{id:'cats', 'parent': 'mamals'},
	{id:'dogs', 'parent': 'mamals'},
	{id:'chilhamua', 'parent': 'dogs'},
	{id:'labrador', 'parent': 'dogs'},
	{id:'persian', 'parent': 'cats'},
	{id:'siamese', 'parent': 'cats'},
]

let makeTree = (obj, parent) => {
	let node = {}
	categories
		.filter(c => c.parent === parent)
		.forEach(c => node[c.id] = makeTree(categories, c.id))
	return node;
}
console.log(
	JSON.stringify(
		makeTree(categories, null)
	,null,2)
	
)

// {

// 	animals: {
// 		mamals : {
// 			dogs: {
// 				chilhamua : null,
// 				labrador: null
// 			},
// 			cats: {
// 				persian: null,
// 				siamese: null
// 			}
// 		}
// 	}
// }


