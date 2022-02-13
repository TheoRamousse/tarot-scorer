function computeChart(titleParam, subtitle, xAxisName, yAxisName, listOfValues, rangeX) {
    Highcharts.chart('container', {

        title: {
            text: titleParam
        },

        subtitle: {
            text: subtitle
        },

        yAxis: {
            title: {
                text: yAxisName
            }
        },

        xAxis: {
            title: {
                text: xAxisName
            },
            categories: rangeX
        },

        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },

        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false,
                },
                cursor: 'pointer',
                point: {
                    events: {
                        click: function (event) {
                            DotNet.invokeMethodAsync("APIGateway","OnPointSelected", event.point.index)
                        }
                    }
                }
            }
        },

        series: [{
            name: 'Parties',
            data: listOfValues
        }],

        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom'
                    }
                }
            }]
        }

    });


}
