

var currentData = $('#currentData').val();
var currentGranularity = $('#currentGranularity').val();
var update = 0;
var updateData = "";
var currentType;
var title = "Voltage 1";

var secondDate = (new Date()).getTime();
var firstDate = secondDate - 900000;
var flag = 0;

function timeRangeFinder(timeOne, timeTwo) {
    return (timeTwo - timeOne) / 1000;
}

$(document).ready(function () {
    currentType = "v1";
    getNewData();
});

setInterval(function () {
    getNewData();
}, 8000);

function getNewData() {
    $.ajax({
        type: "GET",
        data: { dataType: currentType },
        url: $('#UpdateChartUrl').val(),
        async: false,
        success: function (result) {
            updateData = JSON.parse("[" + result + "]")
        }
    });
}

function graphMaker() {
    $(function () {

        Highcharts.setOptions({
            global: {
                useUTC: false
            }
        });

        // Create the graph
        $('#graph').highcharts('StockChart', {
            chart: {
                events: {
                    load: function () {
                        if (flag == 0) {
                            var series = this.series[0];
                            setInterval(function () {
                                if (update == 8) {
                                    update = 0;
                                }

                                var x = (new Date()).getTime(),
                                y = Math.max(-1000000, updateData[update]);
                                series.addPoint([x, y], true, true);
                                update++;
                            }, 1000);
                        }
                    }
                }
            },

            rangeSelector: {
                buttons: [{
                    count: (flag == 0) ? currentGranularity : timeRangeFinder(firstDate, secondDate),
                    type: 'minute',
                    text: ''

                }],
                inputEnabled: false,
                selected: 0
            },
            navigator: {

            },

            title: {
                text: title
            },

            exporting: {
                enabled: false
            },

            series: [{
                data: (function () {
                    var data = [], time = (new Date()).getTime(), i,
                     ourData = JSON.parse("[" + currentData + "]"), j = 0;

                    for (i = (ourData.length * -1) ; i < 0; i += 1) {
                        data.push([
                            time + i * 1000,
                            ourData[j]
                        ]);
                        j++;
                    }
                    return data;
                }())
            }]
        });
    });
}

graphMaker(JSON.parse("[" + currentData + "]"));

$(".granularity").click(function () {
    $("button").removeClass("granularity-selected");
    $(this).addClass("granularity-selected");
    currentGranularity = this.id;
    graphMaker();
});

$(".data").click(function () {
    var graph = $('#graph').highcharts();
    $("button").removeClass("data-selected");
    $(this).addClass("data-selected");
    currentType = this.id;
    title = (currentType == "v1") ? "Voltage 1" :
        (currentType == "v2") ? "Voltage 2" :
        (currentType == "v3") ? "Voltage 3" : "Temperature";
    $.ajax({
        type: "GET",
        data: { dataType: this.id },
        url: $('#DataChangeUrl').val(),
        success: function (result) {
            currentData = result;
            update = 0;
            getNewData();
            graphMaker(JSON.parse("[" + currentData + "]"));
        }
    });

});

$("#clear").click(function () {
    document.getElementById("from-date").value = "";
    document.getElementById("to-date").value = "";
    document.getElementById("from-time").value = "";
    document.getElementById("to-time").value = "";
});

function toTimestamp(str) {
    var s = str.split("/");
    if (s.length > 1) return (new Date(Date.UTC(s[2], (s[0] * 1 - 1), s[1], 0, 0, 0)).getTime());
}

function minutesToMilliseconds(minutes) {
    return minutes * 60000
}

$("#apply").click(function () {
    var fromTime = toTimestamp(document.getElementById("from-date").value)
        + minutesToMilliseconds(document.getElementById("from-time").value);
    var toTime = toTimestamp(document.getElementById("to-date").value)
    + minutesToMilliseconds(document.getElementById("to-time").value);
    var currentTime = (new Date()).getTime();
    if (fromTime > toTime) {
        alert("From time cannot be later than To time.");
    }
    else if (fromTime > currentTime || toTime > currentTime) {
        alert("Selected time cannot be in the future.");
    }
    else if (document.getElementById("from-date").value == "" ||
    document.getElementById("to-date").value == "" ||
    document.getElementById("from-time").value == "" ||
    document.getElementById("to-time").value == "") {
        alert("Field cannot be empty.");
    }
    else {
        firstDate = (fromTime / 1000) + 14400;
        secondDate = toTime / 1000 + 14400;
        flag = 1;
        $.ajax({
            type: "POST",
            data: {
                fromTime: firstDate,
                toTime: secondDate,
                dataType: currentType
            },
            url: $('#DataRangeUrl').val(),
            async: false,
            success: function (result) {
                currentData = result;
                graphMaker();
            }
        });
    }
});

$(function () {
    $(".datepicker").datepicker();
});