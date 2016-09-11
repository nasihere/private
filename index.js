var Robinhood = require('robinhood')

var reqType = ['GAS','GOLD','SNP'];
var STOCKS = {
	GAS: {
		'UGAZ':{diff:{}, inverse: 'DGAZ'},
		'DGAZ':{diff:{}, inverse: 'UGAZ'}
	},

	GOLD: {
		'NUGT':{diff:{}, inverse: 'DUST'},
		'DUST':{diff:{}, inverse: 'NUGT'}
	},
	SNP: {
		'SVXY':{diff:{}, inverse: 'UVXY'},
		'UVXY':{diff:{}, inverse: 'SVXY'}
	}
	// ,
	// SOCIAL: {
	// 	'FB':{diff:{}, inverse: 'TWTR'},
	// 	'TWTR':{diff:{}, inverse: 'FB'}
	// }


};



function getQuote(symbol, reqType) {

	Robinhood(null).quote_data(symbol, function(error, response, body) {
	    if (error) {
	        console.error(error);
	        process.exit(1);
	    }
	    fetchVolume(body,reqType);
	});

}

function fetchVolume(body,reqType) {
	setQuote(body,reqType);
	newLine();
}
function print(string) {
	console.log(string);
}
function newLine(){
	console.log('---------------------------------------- ' + new Date() + '-----------------');
}

function averageVolume(symbol1, symbol2) {
	var result = (symbol1.volume / symbol2.volume * 100);
	if (symbol1.volume < symbol2.volume) result = -result;
	symbol1.diff.volume = result.toFixed(5);
	
}

function bidDiff (symbol1, symbol2) {
	var result1 = symbol1.ask - symbol1.bid;
	symbol1.diff.bid = -result1.toFixed(5);

	var result2 = symbol2.ask - symbol2.bid;
	symbol2.diff.bid = -result2.toFixed(5);
} 

function objToString(obj) {
	return JSON.stringify(obj, null, 4);
}

function bullishSymbol(symbol1, symbol2) {
	if (symbol1.diff.volume.indexOf('-') === -1)
	{
		symbol1.diff.mode = 'BULL';
	}
	else {
		symbol1.diff.mode = 'BEAR';	
	}

}

function setQuote(body,reqType) {
	var quote = body.query.results.quote;
	var symbol = quote.Symbol;
	var STOCKTYPE = STOCKS[reqType][symbol];
 	STOCKTYPE.ask = quote.Ask;
 	STOCKTYPE.bid = quote.Bid; 
 	STOCKTYPE.percentChange = quote.Change_PercentChange; 
 	STOCKTYPE.Percent = quote.PercentChange; 
 	STOCKTYPE.volume = quote.Volume; 
	
	Object.keys(STOCKS[reqType]).forEach(function(key) {

		newLine();
		var inveseSymbol = STOCKS[reqType][key].inverse;
		console.log(inveseSymbol)
		averageVolume(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);
	 	bidDiff(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
	 	bullishSymbol(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
		print(objToString(STOCKS))
	});
 	
}

reqType.forEach(function(type) {

	Object.keys(STOCKS[type]).forEach(function(key) {
	  getQuote(key,type);
	});

});