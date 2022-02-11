function computeChart(titleParam, subtitle, xAxisName, yAxisName, listOfValues, pointStart) {
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
            accessibility: {
                rangeDescription: 'Range generated automatically with a start value of ' + pointStart
            }
        },

        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },

        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                pointStart: pointStart,
                point: {
                    events: {
                        click: function () {
                            DotNet.invokeMethodAsync('DynamicGraph', 'OnPointSelected', this.index);
                        }
                    }
                }
            }
        },

        series: [{
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
