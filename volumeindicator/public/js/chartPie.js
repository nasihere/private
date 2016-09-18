function jsonPrint(data) {
    $('#jsonData').html(JSON.stringify(data,null,4));
}
function drawBidDiff (container,typeParam, dataParam) {
   // var series1 = [data[0], ];
    if (typeParam === undefined) return;
    var data = dataParam.series;
    var type = typeParam.name;
    var series = [
                [data[0][0],-data[0][1]],
                [data[0][1],-data[1][1]]
            ];
    drawBidDemand(container, type, series);
}
function drawBidDemand(container,type, series) {


 $('#'+container).highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: 0,
            plotShadow: false
        },
        title: {
            text: type,
            align: 'center',
            verticalAlign: 'middle',
            y: 40
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                dataLabels: {
                    enabled: true,
                    distance: -50,
                    style: {
                        fontWeight: 'bold',
                        color: 'white',
                        textShadow: '0px 1px 2px black'
                    }
                },
                startAngle: -90,
                endAngle: 90,
                center: ['50%', '75%']
            }
        },
        series: [{
            type: 'pie',
            name: 'Bidder',
            innerSize: '50%',
            data: series

        }]
    });
}
function drawChartVolume(data) {
  $('#datatableVolume').html(data.htmlVolume);  
  $('#containerVolume').highcharts({
        legend: {
          enabled: false
        },
        line: {
            animation: false
        },
        data: {
            table: 'datatableVolume'
        },
        chart: {
            type: 'column',
            animation: false
        },
        title: {
            text: 'Volume Average'
        },
        series: {
            animation: false
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Number'
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
            }
        }
    });
}
function drawChart(data) {
  $('#datatable').html(data.html);
  $('#container').highcharts({
        legend: {
          enabled: false
        },
        line: {
            animation: false
        },
        data: {
            table: 'datatable'
        },
        chart: {
            type: 'column',
            animation: false
        },
        title: {
            text: 'Volume Current'
        },
        series: {
            animation: false
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Number'
            },
            min: 0, max: 400
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
            }
        }
    });
}