'use strict';

	var milionZero = 1000000;

	function pushLog(curtVolume, curtBid, curtMode, curtStatus, symbol) {
		var position = {
			volume: curtVolume,
			bid: curtBid,
			mode: curtMode,
			status:curtStatus
		};	
		symbol.log = position;
	}
	function milion(val) { //Convert Milion
		return (val / milionZero).toFixed(2);
	}
	function curtStatus(curtMode, curtVolume, oldVolume, symbol) {
		var mode = '';
		var movedVolume = '';
		if (curtMode === 'Green') {
			if (curtVolume > symbol.alarm) {
				mode = 'UP';
				var limitVolume = symbol.alarm + ' / Max: ' + symbol.alarmLimit;
				movedVolume = 'VOL: ' + milion(curtVolume) + ' / LIMIT: ' + milion(limitVolume); 
			}
			else {
				mode = ''
			}
		}
		else {
			if (curtVolume > symbol.alarm) {
				mode = 'DOWN';
				var limitVolume = symbol.alarmLimit;
				movedVolume = 'VOL: ' + milion(curtVolume) + ' / MAX: ' + milion(limitVolume); 
			}
			else {
				mode = ''
			}
		}

		return {
			mode: mode,
			status:	symbol.id +' ' +  mode + ' '  + movedVolume,
		}
	}

module.exports = {

	alert: function(symbol1, symbol2) {
			var curtVolume1 = symbol1.curtVolume || 1;
			var curtVolume2 = symbol2.curtVolume || 1;
			
			var curtPercent1 = symbol1.bid || 1;
			var curtPercent2 = symbol2.bid || 1;

			var curtMode1 = (curtPercent1 > symbol1.log.oldPercent) ? 'Green' : 'Red';
			var curtMode2 = (curtPercent2 > symbol2.log.oldPercent) ? 'Green' : 'Red';

			var curtStatus1 = curtStatus(curtMode1, curtVolume1, symbol1.log.oldVolume, symbol1);
			var curtStatus2 = curtStatus(curtMode2, curtVolume2, symbol2.log.oldVolume, symbol2);


			pushLog(symbol1.log.oldVolume, symbol1.log.oldPercent, curtStatus1.mode, curtStatus1.status, symbol1);
			pushLog(symbol2.log.oldVolume, symbol2.log.oldPercent, curtStatus2.mode, curtStatus2.status, symbol2);

			symbol1.log.oldPercent 		=	curtPercent1; 
			symbol2.log.oldPercent 		=	curtPercent2; 
			
			symbol1.log.oldVolume 		= 	curtVolume1;
			symbol2.log.oldVolume 		= 	curtVolume2;
			

			return {
				s1: symbol1,
				s2: symbol2
			}
		}
	
}