var STOCKS = require('./symbols');
var WebApi = require('./request')
var TableVolume = require('./tableVolume');
var STOCKS = require('./symbols');


var reqString = "GAS,OIL,GOLD,SNP";
var reqType = reqString.split(',');


module.exports = function(){
  var express = require('express');
  var app = express();
  var url = require('url');
  var path = require('path');	
	var util = require('./util');


	app.get('/', function(request, response){
		
		response.sendFile(path.join(__dirname + '/view/index.html'));
	})

  return app;
}();