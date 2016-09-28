var STOCKS = require('./symbols');
var WebApi = require('./request')
var TableVolume = require('./tableVolume');
var STOCKS = require('./symbols');
var reqString = "GAS,OIL,GOLD,SNP";
var reqType = reqString.split(',');


module.exports = function(){
  var express = require('express');
  var app = express();
  var util = require('./util');
  function getQuote(symbol, reqType) {

		WebApi(null).quote_data(symbol, function(error, response, body) {
		    if (error) {
		        console.log(error);
		        return;
		    }
		    util.setQuote(body,reqType);
		});

	}

	app.get('/', function(request, response) {
	
		 try {
			reqType.forEach(function(type) {
				Object.keys(STOCKS[type]).forEach(function(key) {
				  getQuote(key,type);
				});

			});
		   	TableVolume.draw(request, response, reqType, STOCKS);
	   }
	   catch(e) {
	   	//	console.log(e);
	   	};
	})

  return app;
}();