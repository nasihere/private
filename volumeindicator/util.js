'use strict';
var STOCKS = require('./symbols');
var env = process.env.NODE_ENV;


function averageVolume(symbol1, symbol2) {
	var times = symbol1.times || 1;
	var times2 = symbol2.times || 1;
	 
	 var result = (symbol1.curtVolume / symbol1.volume * 100);
	 symbol1.diff.volume = (symbol1.times) ? result * times  : result;//result * times / 100;

	 var result2 = (symbol2.curtVolume / symbol2.volume * 100);
	 symbol2.diff.volume = (symbol2.times) ? result2 * times2 : result2;
	
}

function bidDiff (symbol1, symbol2) {
	var result1 = symbol1.ask - symbol1.bid;
	var result2 = symbol2.ask - symbol2.bid;
	
	symbol1.diff.bid = -result2.toFixed(5);
	symbol2.diff.bid = -result1.toFixed(5);

} 

function bullishSymbol(symbol1, symbol2) {

	var VolumeAlert = require('./volumeAlert');
	var symbol = VolumeAlert.alert(symbol1, symbol2);
	symbol1 = symbol.symbol1;
	symbol2 = symbol.symbol2;
}


function print(string) {
	if (env !== 'production')
		console.log(string);
}

module.exports = {
	setQuote: function(body,reqType) {
		if (body.query === undefined) return;
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

			var inveseSymbol = STOCKS[reqType][key].inverse;
			averageVolume(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);
		 	bidDiff(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
		 	bullishSymbol(STOCKS[reqType][key],STOCKS[reqType][inveseSymbol]);	
			print(JSON.stringify(STOCKS, null, 4))
		});
	 	
	}

}
