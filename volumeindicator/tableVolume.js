module.exports =  {
	draw: function (request, response, reqType, STOCKS) {
		setTimeout(function() {
			var data = {
	   			xAxis: [],
	   			series: []
	   		};
			var html = '';
			var htmlVolume = '';
	   		html += '<thead><tr><th></th>';		
	   		html += '<th>Share 1</th>';
	   		html += '<th>Share 2</th>';
			html += '<tbody><tbody>';
			
			var movingStatus = '';

			htmlVolume = html;
	   		reqType.forEach(function(type) {
		   		html += '<tr>';	
		   		htmlVolume += '<tr>';

		   		var loop = true;
		   		var keys = '';
		   		Object.keys(STOCKS[type]).forEach(function(key) {
		   			if (loop === true) {
		   				var inveseSymbol = STOCKS[type][key].inverse;
			   			
			   			var mode1 = STOCKS[type][key].log.mode;
			   			var mode2 = STOCKS[type][inveseSymbol].log.mode;

			   			if (mode1) 
			   			{
			   				movingStatus +=  STOCKS[type][key].log.status + ', <br />';
			   			}
			   			if (mode2) 
			   			{
			   				movingStatus +=  STOCKS[type][inveseSymbol].log.status + ', <br />';
			   			}
			   			html += '<td>'+type + ' - ' +key + '(' + mode1 + ')' + '/' + inveseSymbol + '(' + mode2 + ')' +'</td>';	
			   			html += '<td>'+STOCKS[type][key].diff.volume+'</td>';
			   			html += '<td>'+STOCKS[type][inveseSymbol].diff.volume+'</td>';	
					

						htmlVolume += '<td>'+type + ' - ' +key + '(' + mode1 + ')' + + '/' + inveseSymbol + '(' + mode2 + ')' +'</td>';	
			   			htmlVolume += '<td>'+STOCKS[type][key].volume+'</td>';
			   			htmlVolume += '<td>'+STOCKS[type][inveseSymbol].Volume+'</td>';	
						
						loop = false;
					}
				});
				html += '</tr>';	
				htmlVolume += '</tr>';	
				
			});
	   		
	   		var pieDataSeries = [];
	   		var pieMode, pieStatus = '';
	   		reqType.forEach(function(type) {
	   			var pieParentSeries = [];
	   			Object.keys(STOCKS[type]).forEach(function(key) {
		   			var pieSeries = [key, STOCKS[type][key].diff.bid];
		   			pieParentSeries.push(pieSeries);


		   		});

	   			pieDataSeries.push({
	   				name: type,
	   				series: pieParentSeries,
	   				mode: pieMode,
	   				status: pieStatus
	   			})			




			});



	   		var speedDataSeries = [];
	   		reqType.forEach(function(type) {
	   			var speedParentSeries = [];
	   			Object.keys(STOCKS[type]).forEach(function(key) {

		   			var speedSeries = [key, STOCKS[type][key].log.oldVolume];
		   			var speedMode 	= 	STOCKS[type][key].log.mode;
		   			var speedMax	=	STOCKS[type][key].alarmLimit;
		   			var speedMin 	= 	STOCKS[type][key].alarm;
					var speedPer 	= 	STOCKS[type][key].log.oldPercent;
//		   			speedParentSeries.push(speedSeries);

	   				speedDataSeries.push({
		   				name: type,
		   				max: speedMax,
		   				min: speedMin,
		   				series: [speedSeries],
		   				mode: speedMode,
						percent: speedPer
		   			})			

		   		});

	   		



			});



	   		var resData = {
	   			html: html,
	   			data: STOCKS,
	   			pie: pieDataSeries,
	   			htmlVolume: htmlVolume,
	   			speed: speedDataSeries
	   		};
	        response.send(resData)
	    }, 2000);//force delay of 20 seconds.
	}
}
