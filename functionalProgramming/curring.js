'use strict'

let dragon = 
	animal => 
		name => 
			type => {
				console.log('animal', animal, 'name', name, 'type', type)
			}


console.log(dragon('animal')('name')('type'))
