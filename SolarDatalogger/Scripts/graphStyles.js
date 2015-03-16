Highcharts.createElement('link', {
    href: 'http://fonts.googleapis.com/css?family=Unica+One',
    rel: 'stylesheet',
    type: 'text/css'
}, null, document.getElementsByTagName('head')[0]);

Highcharts.theme = {
    colors: ["#52CC29", "#90ee7e", "#f45b5b", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee",
  "#55BF3B", "#DF5353", "#7798BF", "#aaeeee"],
    chart: {
        backgroundColor: 'black',
        style: {
            fontFamily: "'Unica One', sans-serif"
        },
        plotBorderColor: 'white'
    },
    title: {
        style: {
            color: 'white',
            textTransform: 'uppercase',
            fontSize: '20px'
        }
    },
    subtitle: {
        style: {
            color: '#E0E0E3',
            textTransform: 'uppercase'
        }
    },
    xAxis: {
        gridLineColor: 'white',
        labels: {
            style: {
                color: 'white'
            }
        },
        lineColor: 'white',
        minorGridLineColor: '#505053',
        tickColor: 'white',
        title: {
            style: {
                color: 'white'

            }
        }
    },
    yAxis: {
        gridLineColor: 'white',
        labels: {
            style: {
                color: 'white'
            }
        },
        lineColor: 'white',
        minorGridLineColor: '#505053',
        tickColor: 'white',
        tickWidth: 1,
        title: {
            style: {
                color: 'white'
            }
        }
    },
    tooltip: {
        backgroundColor: 'rgba(0, 0, 0, 0.85)',
        style: {
            color: '#F0F0F0'
        }
    },
    plotOptions: {
        series: {
            dataLabels: {
                color: '#B0B0B3'
            },
            marker: {
                lineColor: '#333'
            }
        },
        boxplot: {
            fillColor: '#505053'
        },
        candlestick: {
            lineColor: 'white'
        },
        errorbar: {
            color: 'white'
        }
    },
    legend: {
        itemStyle: {
            color: 'white'
        },
        itemHoverStyle: {
            color: '#FFF'
        },
        itemHiddenStyle: {
            color: '#606063'
        }
    },
    credits: {
        style: {
            color: 'black'
        }
    },
    labels: {
        style: {
            color: '#707073'
        }
    },

    drilldown: {
        activeAxisLabelStyle: {
            color: '#F0F0F3'
        },
        activeDataLabelStyle: {
            color: '#F0F0F3'
        }
    },

    navigation: {
        buttonOptions: {
            symbolStroke: '#DDDDDD',
            theme: {
                fill: '#505053'
            }
        }
    },

    // scroll charts
    rangeSelector: {
        buttonTheme: {
            fill: 'black',
            stroke: 'black',
            style: {
                color: 'black'
            },
            states: {
                hover: {
                    fill: 'black',
                    stroke: 'black',
                    style: {
                        color: 'black',
                        cursor: 'none'
                    },

                },
                select: {
                    fill: 'black',
                    stroke: 'black',
                    style: {
                        color: 'black'
                    }
                }
            }
        },
        inputBoxBorderColor: '#505053',
        inputStyle: {
            backgroundColor: '#333',
            color: 'silver'
        },
        labelStyle: {
            color: 'black'
        }
    },

    navigator: {
        handles: {
            backgroundColor: '#666',
            borderColor: '#AAA'
        },
        outlineColor: 'black',
        maskFill: 'rgba(255,255,255,0.1)',
        series: {
            color: '#7798BF',
            lineColor: '#A6C7ED'
        },
        xAxis: {
            gridLineColor: '#505053'
        }
    },

    scrollbar: {
        barBackgroundColor: '#808083',
        barBorderColor: '#808083',
        buttonArrowColor: 'black',
        buttonBackgroundColor: 'black',
        buttonBorderColor: 'black',
        rifleColor: '#FFF',
        trackBackgroundColor: '#404043',
        trackBorderColor: '#404043'
    },

    // special colors for some of the
    legendBackgroundColor: 'rgba(0, 0, 0, 0.5)',
    background2: '#505053',
    dataLabelsColor: '#B0B0B3',
    textColor: '#C0C0C0',
    contrastTextColor: '#F0F0F3',
    maskColor: 'rgba(255,255,255,0.3)'
};

// Apply the theme
Highcharts.setOptions(Highcharts.theme);