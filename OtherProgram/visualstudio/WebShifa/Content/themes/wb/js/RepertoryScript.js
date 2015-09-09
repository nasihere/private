var result = document.getElementById("result");
var area = document.getElementById("area");
var button = document.getElementById("formate");
var select = document.getElementById("sort");
function toArray(string) {
	var object = [];
	var lines = string.split('$$');
	var tl = lines;
	if(tl.pop() == undefined){
		lines.pop();
	}
	var symptoms = [];
	for(i=0;i<lines.length;i++){
		var test = lines[i].split('^');
		symptoms[i] = test[1];
		var r = test[0].split(':');
		var testr = r;
		if(testr.pop() == undefined){
			r.pop();
		}
		var rems = [];
		var remsCount = [];
		for(j=0;j<r.length;j++){
			rems[j] = r[j].split(",")[0];
			remsCount[j] = r[j].split(",")[1];
		}
		object[i] = {symptoms:symptoms[i],remedies:rems,remediesCount:remsCount};
	}
	return object;
}
Array.prototype.unique = function (){  
    var r = new Array();  
    o:for(var i = 0, n = this.length; i < n; i++){  
        for(var x = 0, y = r.length; x < y; x++){  
            if(r[x]==this[i]) continue o;}  
        r[r.length] = this[i];}  
    return r;  
} 
function listRemedies(object){
	var remediesList = [];
	for(i=0;i<object.length;i++){
		for(j=0;j<object[i].remedies.length;j++){
			remediesList.push(object[i].remedies[j]);
		}
	}
	var text = "";
	for(i=0;i<remediesList.length;i++){
		remediesList[i] = remediesList[i].replace(/(\r\n|\n|\r)/gm,"");
	}
	return remediesList;
}
function show(object) {
	var remedies = listRemedies(object).unique();
	if(select.value == "Name") remedies = remedies.sort();
	var noRemedies = listRemedies(object);
	var table = document.createElement("table");
	var table2 = document.createElement("table");
	var title = document.createElement("tr");
	var title2 = document.createElement("tr");
	var symptitle = document.createElement("th");
	var remtitle = document.createElement("th");
	symptitle.innerHTML = "Symptoms";
	remtitle.innerHTML = "Remedies";
	title.appendChild(symptitle);
	title2.appendChild(remtitle);
	var th = [];
	var remGrading = [];
	var remCount = [];
	function total() {
		for(i=0;i<object.length;i++){
			for(j=0;j<remedies.length;j++){
				for(x=0;x<object[i].remedies.length;x++){
					object[i].remedies[x] = object[i].remedies[x].replace(/(\r\n|\n|\r)/gm,"");
					object[i].remediesCount[x] = parseInt(object[i].remediesCount[x]);
					if(remedies[j] == object[i].remedies[x]){
						if(remedies[j].indexOf(object[i].remedies[x]) == -1){
							remCount.push(object[i].remediesCount[x]);
						}else{
							if(remCount[remedies.indexOf(object[i].remedies[x])] == undefined){
								remCount[remedies.indexOf(object[i].remedies[x])] = object[i].remediesCount[x];
							}else{
								remCount[remedies.indexOf(object[i].remedies[x])] += object[i].remediesCount[x];
							}
						}
					}
				}
			}
		}
	}
	function grading(){
		for(i=0;i<remedies.length;i++){
			remGrading[i] = 0;
			for(j=0;j<noRemedies.length;j++){
    			if(noRemedies[j] == remedies[i]){
    				remGrading[i]++;
    			}
			}
		}
	}
	//Arrays
	total();
	if(select.value == "Intensity"){
		var testArray = [];
		for(i=0;i<remCount.length;i++){
			testArray[i] = remCount[i] + remedies[i];
		}
		testArray = testArray.sort().reverse();
		alert(testArray);
		for(i=0;i<remCount.length;i++){
			testArray[i] = testArray[i].replace(/\d{0,}/,"");
		}
		alert(testArray);
		remedies = testArray;
	}
	remCount =[];
	total();
	grading();
	if(select.value == "Grading"){
		var testArray = [];
		for(i=0;i<remCount.length;i++){
			testArray[i] = remGrading[i] + remedies[i];
		}
		testArray = testArray.sort().reverse();
		for(i=0;i<remCount.length;i++){
			testArray[i] = testArray[i].slice(1);
		}
		remedies = testArray;
	}
	remGrading = [];
	grading();
	//DOM
	for(i=0;i<remedies.length;i++){
		th[i] = document.createElement("th");
		th[i].innerHTML = remedies[i];
		title.appendChild(th[i]);
	}
	for(i=0;i<remedies.length;i++){
		th[i] = document.createElement("th");
		th[i].innerHTML = remedies[i];
		title2.appendChild(th[i]);
	}
	table2.appendChild(title2);
	table.appendChild(title);
	for(i=0;i<object.length;i++){
		var line = document.createElement("tr");
		var std = document.createElement("td");
		var td = [];
		std.innerHTML = object[i].symptoms.split("|").join(", ");
		line.appendChild(std);
		for(j=0;j<remedies.length;j++){
			td[j] = document.createElement("td");
			for(e=0;e<object[i].remedies.length;e++){
				object[i].remedies[e] = object[i].remedies[e].replace(/(\r\n|\n|\r)/gm,"");
				object[i].remediesCount[e] = parseInt(object[i].remediesCount[e]);
				if(remedies[j] == object[i].remedies[e]){
					td[j].innerHTML = object[i].remediesCount[e];
				}
			}
			line.appendChild(td[j]);
		}
		table.appendChild(line);
	}
	var line2 = document.createElement("tr");
	var std2 = document.createElement("td");
	var td2 = [];
	var line3 = document.createElement("tr");
	var std3 = document.createElement("td");
	var td3 = [];
	std3.innerHTML = "Grading";
	std2.innerHTML = "Total";
	line3.appendChild(std3);
	line2.appendChild(std2);
	for(i=0;i<remCount.length;i++){
		td2[i] = document.createElement("td");
		td2[i].innerHTML = remCount[i];
		line2.appendChild(td2[i]);
	}
	for(i=0;i<remGrading.length;i++){
		td3[i] = document.createElement("td");
		td3[i].innerHTML = remGrading[i];
		line3.appendChild(td3[i]);
	}
	table2.appendChild(line2);
	table2.appendChild(line3);
	table.appendChild(line);
	result.innerHTML = "";
	result.appendChild(table);
	result.appendChild(table2);
}
button.addEventListener("click",function(){
    show(toArray(sessionStorage.Report));
},false);