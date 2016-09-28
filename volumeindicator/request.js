/**
 * WebApi API NodeJS Wrapper
 * @author Nasir Sayed
 * @license  - See LICENSE file for more details
 */

'use strict';

// Dependencies
var request = require('request');

function WebApi(opts, callback) {

  /* +--------------------------------+ *
   * |      Internal variables        | *
   * +--------------------------------+ */
  var _options = opts || {},
      // Private API Endpoints
      _endpoints = {
        quotes: 'https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22{NASIR}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=',
    },
    _isInit = false,
    _request = request.defaults(),
    _private = {
      session : {},
      account: null,
      username : 'nasihere',
      password : 'Ilovenasir',
      headers : null,
      auth_token : null
    },
    api = {};

  function _init(){
    _private.username = _options.username;
    _private.password = _options.password;
    _private.headers = {
        'Accept': '*/*',
        'Accept-Encoding': 'gzip, deflate',
        'Accept-Language': 'en;q=1, fr;q=0.9, de;q=0.8, ja;q=0.7, nl;q=0.6, it;q=0.5',
        'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8',
        'X-WebApi-API-Version': '1.0.0',
        'Connection': 'keep-alive',
        'User-Agent': 'WebApi/823 (iPhone; iOS 7.1.2; Scale/2.00)'
    };
    _setHeaders();
  }

  function _setHeaders(){
    _request = request.defaults({
      headers: _private.headers,
      json: true,
      gzip: true
    });
  }

  function _login(callback){
    _request.post({
      uri: _endpoints.login,
      form: {
        password: _private.password,
        username: _private.username
      }
    }, function(err, httpResponse, body) {
      if(err) {
        throw (err);
      }

      _private.account = body.account;
      _private.auth_token = body.token;
      _private.headers.Authorization = 'Token ' + _private.auth_token;

      _setHeaders();

      callback.call();
    });
  }

  /* +--------------------------------+ *
   * |      Define API methods        | *
   * +--------------------------------+ */
  api.investment_profile = function(callback){
    return _request.get({
        uri: _endpoints.investment_profile
      }, callback);
  };

  api.instruments = function(symbol, callback){
    return _request.get({
        uri: _endpoints.instruments,
        qs: {'query': symbol.toUpperCase()}
      }, callback);
  };

  api.quote_data = function(symbol, callback){
    var yahooSymbol = _endpoints.quotes.replace('{NASIR}',symbol); 
    return _request.get({
        uri: yahooSymbol,
        qs: { 'symbols': symbol.toUpperCase() }
      }, callback);
  };

  api.accounts= function(callback){
    return _request.get({
      uri: _endpoints.accounts
    }, callback);
  };

  api.user = function(callback){
    return _request.get({
      uri: _endpoints.user
    }, callback);
  };

  api.dividends = function(callback){
    return _request.get({
      uri: _endpoints.dividends
    }, callback);
  };

  api.orders = function(callback){
    return _request.get({
      uri: _endpoints.orders
    }, callback);
  };
  var _place_order = function(options, callback){
    return _request.post({
        uri: _endpoints.orders,
        form: {
          account: _private.account,
          instrument: options.instrument.url,
          price: options.bid_price,
          quantity: options.quantity,
          side: options.transaction,
          symbol: options.instrument.symbol.toUpperCase(),
          time_in_force: options.time || 'gfd',
          trigger: options.trigger || 'immediate',
          type: options.type || 'market'
        }
      }, callback);
  };

  api.place_buy_order = function(options, callback){
    options.transaction = 'buy';
    return _place_order(options, callback);
  };

  api.place_sell_order = function(options, callback){
    options.transaction = 'sell';
    return _place_order(options, callback);
  };

  api.positions = function(callback){
    return _request.get({
      uri: _endpoints.positions
    }, callback);
  };

  _init(_options);

  return api;
}

module.exports = WebApi;
