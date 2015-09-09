
<head>
<script src="https://code.jquery.com/jquery-1.10.2.js"></script>
<script>
var RemediesName = "<?php echo $_REQUEST['paramremedies']; ?>";
if (!String.prototype.trim)
{
    String.prototype.trim = function()
    {
        return this.replace(/^\s+|\s+$/g,'');
    };
}

$( document ).ready(function() {
	
	var normalArray = [];
	var italicArray = [];
	var boldArray = [];
	var boldUnderLineArray = [];
	var categoy = "";
	if (RemediesName == ""){
		alert("remedies name is empty cannot process");
		return;
	}
	$("table").each(function(){
		$(this).remove();
	});
	$("p").each(function() {
		var flag = false;
	
	var bold = $("b span", this) .text();
	bold = bold.replace(/\n|\r/g, "");
	if(bold != "" )
	{
		var replaceStr = bold.split('-');

		categoy = bold.replace(/ - /g,"|");
		categoy = bold.replace(/ -/g ,"|");
		categoy = bold.replace(/- /g ,"|");
		var subcategory = "";
		 $.each(replaceStr, function (i, val) {
			 if (subcategory == "")
				 subcategory = val;
				 else
				 {
	 			
 			 		subcategory += "|" + val;
	 				append(val,subcategory,"",3);
					flag = true;
				}
			
			 
				replaceStr[i] = val.replace(val,'<b>' + val + '<b>');
			});
		boldArray.push(replaceStr+"\n");
	}
		
	var italic = $("i span", this) .text();
	italic = italic.replace(/\n|\r/g, "");
	if(italic != "" && !flag)
	{
		var replaceStr = italic.split('-');

		categoy = italic.replace(/ - /g,"|");
		categoy = italic.replace(/ -/g ,"|");
		categoy = italic.replace(/- /g ,"|");
		var subcategory = "";
		
		 $.each(replaceStr, function (i, val) {
			 if (subcategory == "")
				 subcategory = val;
			 else
			 {
 			
		 		subcategory += "|" + val;
 				append(val,subcategory,"",2);
				flag = true;
			}
		
			 
				replaceStr[i] = val.replace(val,'<i>' + val + '<i>');
			});
		italicArray.push(replaceStr+"\n");
	}
		
	var normal = $("span", this) .text();
	normal = normal.replace(/\n|\r/g, "");
	if(normal != "" && normal!="©Copyright 2000, Archibel S.A." && !flag)
	{
		var replaceStr = normal.split('-');
		categoy = normal.replace(/ - /g,"|");
		categoy = normal.replace(/ -/g ,"|");
		categoy = normal.replace(/- /g ,"|");
		var subcategory = "";
		 $.each(replaceStr, function (i, val) {
			 if (subcategory == "")
				 subcategory = val;
				 else
				 {
	 			
 			 		subcategory += "|" + val;
	 				append(val,subcategory,"",1);
					flag = true;
				}
				replaceStr[i] = val.replace(val,'<i>' + val + '<i>');
			});
		normalArray.push(replaceStr+"\n");
	}
/*	
	var boldUnderLine = $("b u span", this) .text();
	boldUnderLine = boldUnderLine.replace(/\n|\r/g, "");
	if(boldUnderLine != "")
	{
		var replaceStr = boldUnderLine.split('-');
		categoy = boldUnderLine.replace(/ - /g ,"|");
		categoy = boldUnderLine.replace(/ -/g ,"|");
		categoy = boldUnderLine.replace(/- /g ,"|");
		var subcategory = "";
		 $.each(replaceStr, function (i, val) {
			 if (subcategory == "")
				 subcategory = val;
			 else
			 {
 			
		 		subcategory += "|" + val;
 			//	append(val,subcategory,"",3);

			}
		
				 append(val,subcategory,categoy,4);
				replaceStr[i] = val.replace(val,'<b><u>' + val + '<b><u>');
			});
		boldUnderLineArray.push(replaceStr+"\n");
	}	*/
	
	
	});
	var finalArray=$.merge( $.merge( normalArray, boldArray  ), italicArray );
	
	//$.each(finalArray,function(i,j){
	//	$("#text").append(j + ":\n" );
		
//	})
	//$("#text").html(finalArray.toString());
	//console.log( normalArray );
	//console.log( boldArray );
	//console.log( italicArray );
	//console.log( boldUnderLineArray );
});
function append(val,subcategory,categoy,intensity){
	categoy = categoy.replace(/ - /g ,"|");
	categoy = categoy.replace(/ -/g ,"|");
	categoy = categoy.replace(/- /g ,"|");
	categoy = categoy.replace(/ \| /g ,"|");
	categoy = categoy.replace(/ \|/g ,"|");
	categoy = categoy.replace(/\| /g ,"|");

	subcategory = subcategory.replace(/ \| /g ,"|");
	subcategory = subcategory.replace(/ \|/g ,"|");
	subcategory = subcategory.replace(/\| /g ,"|");
	
	
	val = val.trim();
	categoy = subcategory.trim();
	subcategory = subcategory.trim();
	categoy = categoy.trim();
	
	subcategorySPLIT = subcategory.split("|");
	delete subcategorySPLIT[subcategorySPLIT.length - 1];
	subcategory = subcategorySPLIT.toString();
	subcategory = subcategory.toString().substring(0,subcategory.toString().length - 1).replace(",","|");
	
	//if (subcategory == "") return;
 $("#text").append("insert into homrep (_id,book,Name,categoy,Remedies,maincategoy,title) values (NULL,'newrep','"+val+"','"+subcategory+"','"+RemediesName+":"+intensity+"','Mind','"+categoy+"')");
 $("#text").append(";\n");
}
</script
</head>

<body>
<div><textarea id="text" cols="150" rows="30"></textarea></div>

<?php echo $_REQUEST['paramtext']; ?>
</body>