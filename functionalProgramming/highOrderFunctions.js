'use strict'

let p = x => console.log(x);

//concatAll
let out = null;

//out = [[1],[2],[],[3,4,5]].concatAll()
//p(out);

//reduce
out = [1,2,3,4,5].reduce((prev,cur)=>prev+cur);
p(out); //output 15

//removing duplicate array values
var unique = [1,2,3,4,5,6,2,3,1,3,4,3,4].filter((ele,index,self) => {
    return index === self.indexOf(ele);
})
p(unique);

// let unique = [1,2,3,4,5,6,2,3,1,3,4,3,4].map((ele) => {
//     return ele === this.indexOf[ele];
// })
// p(unique);
let uniBucket = [];
let reduceUnique = [1,2,3,4,5,6,1,2,3,4,9].reduce((prev, curr)=>{
    if (uniBucket.indexOf(curr) === -1) {
        uniBucket.push(curr);
    }
});
p(uniBucket)

var a = [  null,  null, null, 1],
b = a.some(function(k, v, arr){
        p(arr)
    });
console.log(b);//false
