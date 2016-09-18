'use strict'
let deferred = Promise.defer();
console.log(deferred)

console.log('-----')
var promise  = deferred.promise;
console.log(promise);

console.log(deferred)

promise.then((data) => {
	console.log(data);
})

promise.catch((err)=>{
	cnsole.log(err);
});
//deferred.resolve(1);
deferred.reject(new Error('something went wrong'));
console.log(deferred);
console.log(promise)
deferred.resolve(1);


class APIService {
	getData(endPoint) {
		let deferred = Promise.defer();
		fetch(endPoint, fetchConfig)
			.then((data) => {
				deferred.resolve(data);
			})
		return deferred.promise;
	}
	fetch(endPoint, fetchConfig) {
		//Call HTTP $http://nasidn
	}
}

APIService.getData('http://www.google.com').then((data) => {
	console.log(data);
})

Promise.all(APIService.getData(ENDPOINT1), APIService.getData(ENDPOINT2), APIService.getData(ENDPOINT3)).then((data)=>{
	console.log(data);
})

//# Good eample for pagniation when user hit quickly to page 1 to page 2,3,4 so with the promises we can reject promoises..