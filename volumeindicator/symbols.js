module.exports = {
	GAS: {
		'UGAZ':{id:'UGAZ', diff:{}, inverse: 'DGAZ', log: {}, alarm:2000000, alarmLimit: 3500000},
		'DGAZ':{id:'DGAZ', diff:{}, inverse: 'UGAZ', log: {}, alarm:1550000, alarmLimit: 14050000}
	},																
	OIL: {
		'UWTI':{id:'UWTI', diff:{}, inverse: 'DWTI', log: {}, alarm:6850000, alarmLimit: 35850000},
		'DWTI':{id:'DWTI', diff:{}, inverse: 'UWTI', log: {}, alarm:685000, alarmLimit: 2850000}
		
	},
	GOLD: {														   
		'NUGT':{id:'NUGT', diff:{}, inverse: 'DUST',log: {}, alarm:5850000, alarmLimit: 30850000},
		'DUST':{id:'DUST', diff:{}, inverse: 'NUGT',log: {}, alarm:6850000, alarmLimit: 33850000}
	},
	SNP: {
		'SVXY':{id:'SVXY', diff:{}, inverse: 'UVXY',log: {}, alarm:1350000, alarmLimit: 4350000},
		'UVXY':{id:'UVXY', diff:{}, inverse: 'SVXY',log: {}, alarm:6850000, alarmLimit: 26250000}
	}

};
