
$(document).ready(function(){
  //  alert("HI")
  //  $.getScript("../../Global/Scripts/Lib/svp/masked.js", function (data, textStatus, jqxhr) {
       // console.log(textStatus); // Success
      //  console.log(jqxhr.status); // 200
      //  console.log("Load was performed.");
//    });
	// resist putting functions here!
	// put them in svp_app.js > svpApp.init() !!!!

  // show/hide for id=showmoreNAME will show/hide hiddenNAME and switch showmoreNAME image
	/*$('[id^="showmore"]').click(function(){
		var myNAME = $(this).attr('id').substring(8);
		$("[id='hidden"+myNAME+"']").toggle();
		if ($("[id='showmore"+myNAME+"']").attr('src') == 'img/minus-active.png') {
		   $("[id='showmore"+myNAME+"']").attr("src", "img/plus-active.png");
			}
		else {
		   $("[id='showmore"+myNAME+"']").attr("src", "img/minus-active.png");
			}
	});*/


	// init the app
	svpApp.init();

});  // END DOCUMENT.READY









// ============================================================================================================================================== FUNCTIONS =================================================================================




