var dataSpeedoMeter = [80,100];
function drawVolumeDemand (container, dataParam) {
   // var series1 = [data[0], ];
    if (dataParam === undefined) return;
    var data = dataParam.series;
    var max = dataParam.max;
    var type = dataParam.name;
    var title = data[0][0];
    var mode = dataParam.mode;
    var series = data[0][1];
    var percent = dataParam.percent;
            
    var min = dataParam.min;
    drawSpeedDemand(container, type, series, max, min, title,mode,percent);


}
var bullish = ['#00b200','#004c00'];
var bearish = ['#093k20','#990000'];
function drawSpeedDemand(container,type,series, _max,_min, _title,_mode, _percent ) {

    var color = ['#2a2a2b','#3e2e40'];
    if (_mode) {
        color = (_mode === 'UP') ? bullish : bearish;        
    }
    $('#'+container).highcharts({

        chart: {
            type: 'gauge',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 4,
            plotShadow: true,
            
        },

        title: {
            text: _title + ' ' + _percent
        },

        pane: {
            startAngle: -150,
            endAngle: 150,
            background: [{
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#000'],
                        [1, '#333']
                    ]
                },
                borderWidth:23,
                borderColor:color[1],
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background
            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }]
        },

        // the value axis
        yAxis: {
            min: 0,
            max: _max,

            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10,
            minorTickPosition: 'inside',
            minorTickColor: '#000',

            tickPixelInterval: 30,
            tickWidth: 2,
            tickPosition: 'inside',
            tickLength: 10,
            tickColor: '#000',
            labels: {
                step: 4,
                rotation: 'auto',
                color:'#000'
            },
            title: {
                text: parseInt(series),
                color:'#383992'
            },
            plotBands: [{
                from: 0,
                to: (_min / 2),
                color: '#55BF3B' // green
            }, {
                from: (_min / 2),
                to: _min,
                color: '#DDDF0D' // yellow
            }, {
                from: _min,
                to: _max,
                color: '#DF5353' // red
            }]
        },

        series: [{
            name: _mode,
            data: [parseInt(series)],
            tooltip: {
                valueSuffix: ' M/l'
            }
        }]

    });
};