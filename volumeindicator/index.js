var express = require('express')
var app = express()
var Robinhood = require('./request')
var path = require('path');
var env = process.env.NODE_ENV;
var url = require('url');
app.set('port', (process.env.PORT || 5000))
app.use(express.static(__dirname + '/public'))


app.listen(app.get('port'), function() {
  console.log("Node app is running at localhost:" + app.get('port'))
})
app.use(express.static('public'));

var reqString = "GAS,OIL,GOLD,SNP";
var reqType = reqString.split(',');
var STOCKS = {
	GAS: {
		'UGAZ':{diff:{}, inverse: 'DGAZ'},
		'DGAZ':{diff:{}, inverse: 'UGAZ'}
	},
	OIL: {
		'UWTI':{diff:{}, inverse: 'DWTI'},
		'DWTI':{diff:{}, inverse: 'UWTI'}
		
	},
	GOLD: {
		'NUGT':{diff:{}, inverse: 'DUST'},
		'DUST':{diff:{}, inverse: 'NUGT'}
	},
	SNP: {
		'SVXY':{diff:{}, inverse: 'UVXY'},
		'UVXY':{diff:{}, inverse: 'SVXY'}
	}
	,
	SOCIAL: {
		'AAPL':{diff:{}, inverse: 'GOOGL'},
		'GOOGL':{diff:{}, inverse: 'AAPL'}
	}


};
// "volume": 		"7238120",
// "curtVolume": 	"13519826"
// 186

// "volume": 	  	"22800300",
// "curtVolume": 	"72447247"
// 317

function getQuote(symbol, reqType) {

	Robinhood(null).quote_data(symbol, function(error, response, body) {
	    if (error) {
	        print(error);
	        return;
	    }
	    fetchVolume(body,reqType);
	});

}

function fetchVolume(body,reqType) {
	setQuote(body,reqType);
	newLine();
}
function print(string) {
	if (env !== 'production')
		console.log(string);
}
function newLine(){
	if (env !== 'production')
		console.log('---------------------------------------- ' + new Date() + '-----------------');
}

function averageVolume(symbol1, symbol2) {
	var times = symbol1.times || 1;
	var times2 = symbol2.times || 1;
	 
	 var result = (symbol1.curtVolume / symbol2.volume * 100);
	 symbol1.diff.volume = (symbol1.times) ? result * times  : result;//result * times / 100;

	 var result2 = (symbol2.curtVolume / symbol2.volume * 100);
	 symbol2.diff.volume = (symbol2.times) ? result2 * times2 : result2;

	// result = --result;
	//symbol1.diff.volume = (symbol1.curtVolume * times);//result.toFixed(5);
	
}

function bidDiff (symbol1, symbol2) {
	var result1 = symbol1.ask - symbol1.bid;
	var result2 = symbol2.ask - symbol2.bid;
	
	symbol1.diff.bid = -result2.toFixed(5);

	symbol2.diff.bid = -result1.toFixed(5);
} 

function objToString(obj) {
	return JSON.stringify(obj, null, 4);
}

function bullishSymbol(symbol1, symbol2) {
	// if (symbol1.diff.volume.indexOf('-') === -1)
	// {
	// 	symbol1.diff.mode = 'BULL';
	// }
	// else {
	// 	symbol1.diff.mode = 'BEAR';	
	// }

}

function setQuote(body,reqType) {
	var quote = body.query.results.quote;
	var symbol = quote.Symbol;
	var STOCKTYPE = STOCKS[reqType][symbol];
 	STOCKTYPE.ask = (quote.Ask) ? quote.Ask : STOCKTYPE.ask;
 	STOCKTYPE.bid = (quote.Bid) ? quote.Bid : STOCKTYPE.bid; 
 	STOCKTYPE.percentChange = quote.Change_PercentChange; 
 	STOCKTYPE.Percent = quote.PercentChange; 
 	STOCKTYPE.volume = (quote.AverageDailyVolume !== '0') ? quote.AverageDailyVolume : STOCKTYPE.volume; 
 	STOCKTYPE.curtVolume = (quote.Volume !== '0') ? quote.Volume : STOCKTYPE.curtVolume; 
	
	Object.keys(STOCKS[reqType]).forEach(function(key) {

		newLine();
		var inveseSymbol = STOCKS[reqType][key].inverse;
		averageVolume(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);
	 	bidDiff(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
	 	bullishSymbol(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
		print(objToString(STOCKS))
	});
 	
}
app.get('/getQuote', function(request, response) {
	try {

	reqType.forEach(function(type) {

		Object.keys(STOCKS[type]).forEach(function(key) {
		  getQuote(key,type);
		});

	});
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
		
		htmlVolume = html;
   		reqType.forEach(function(type) {
	   		html += '<tr>';	
	   		htmlVolume += '<tr>';

	   		var loop = true;
	   		var keys = '';
	   		Object.keys(STOCKS[type]).forEach(function(key) {
	   			if (loop === true) {
	   				var inveseSymbol = STOCKS[type][key].inverse;
		   		


		   			html += '<td>'+type + ' - ' +key + '/' + inveseSymbol +'</td>';	
		   			html += '<td>'+STOCKS[type][key].diff.volume+'</td>';
		   			html += '<td>'+STOCKS[type][inveseSymbol].diff.volume+'</td>';	
				

					htmlVolume += '<td>'+type + ' - ' +key + '/' + inveseSymbol +'</td>';	
		   			htmlVolume += '<td>'+STOCKS[type][key].volume+'</td>';
		   			htmlVolume += '<td>'+STOCKS[type][inveseSymbol].Volume+'</td>';	
					
					loop = false;
				}
			});
			html += '</tr>';	
			htmlVolume += '</tr>';	
			
		});
   		
   		var pieDataSeries = [];
   		reqType.forEach(function(type) {

   			var pieParentSeries = [];
   			Object.keys(STOCKS[type]).forEach(function(key) {
	   			var pieSeries = [key, STOCKS[type][key].diff.bid];
	   			pieParentSeries.push(pieSeries);
	   		});
   			pieDataSeries.push({
   				name: type,
   				series: pieParentSeries
   			})			
		});


   		var resData = {
   			html: html,
   			data: STOCKS,
   			pie: pieDataSeries,
   			htmlVolume: htmlVolume
   		};
        response.send(resData)
    }, 2000);//force delay of 20 seconds.
   }
   catch(e) {
   		print(e);
   };
})

app.get('/', function(request, response){
	var url_parts = url.parse(request.url, true);
	var query = url_parts.query;
	if (query.type)
	{
		if (query.type !== 'ALL') {
			reqType = query.type.split(',');
		}
		else {
			reqType = reqString.split(',');	
		}
	}
	response.sendFile(path.join(__dirname + '/view/index.html'));
})
