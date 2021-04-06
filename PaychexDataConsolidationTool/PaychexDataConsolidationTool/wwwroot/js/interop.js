// -- STATUS
function generateCPSGraph(graphdata) {
    
    var canvas = document.getElementById("lineGraph"); // Referencing the ID of the <canvas> tag in our razor file
    var ctx = canvas.getContext('2d'); // This is a 2d graph
    //Default font options
    Chart.defaults.global.defaultFontColor = 'black'; 
    Chart.defaults.global.defaultFontSize = 16;

    ////////////////////////////////////////////////////////
    //
    //  Ensuring the Y axist scale does not show up in 
    //  'Scientific format' i.e (2E5), uses human readable
    //  decimal values instead
    //
    Chart.scaleService.updateScaleDefaults('logarithmic', {
        ticks: {
            callback: function (tick, index, ticks) {
                return tick.toLocaleString()
            }
        }
    });
    graphdata = JSON.parse(graphdata); // Parsing our serialized json object from our FetchCPS.razor file

    ////////////////////////////////////////////////////////
    //
    //  Data object to encapsulate x axis, and y axis data
    //
    var data = {
        labels: graphdata.Dates,
        datasets: []
    };

    ///////////////////////////////////////////////////////////////
    //
    //  Creating an object per status, each with randomized color
    //  also associates corrensponding data with its status
    //
    var i = 0; // to reference index of two dimensional array
    graphdata.Statuses.forEach(status => {
        var randomBetween = (min, max) => min + Math.floor(Math.random() * (max - min + 1));
        var r = randomBetween(0, 255);
        var g = randomBetween(0, 255);
        var b = randomBetween(0, 255);
        var rgb = `rgb(${r},${g},${b})`;
        var obj = {
            label: status,
            fill: false,
            lineTension: 0.1,
            backgroundColor: "rgba(225,0,0,0.4)",
            borderColor: rgb, // The main line color
            borderCapStyle: 'square',
            borderDash: [], // try [5, 15] for instance
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: "black",
            pointBackgroundColor: "white",
            pointBorderWidth: 1,
            pointHoverRadius: 8,
            pointHoverBackgroundColor: "yellow",
            pointHoverBorderColor: "brown",
            pointHoverBorderWidth: 2,
            pointRadius: 4,
            pointHitRadius: 10,
            data: graphdata.StatusCounts[i],
            spanGaps: true,
        }
        i++;
        data.datasets.push(obj);
    });

    //////////////////////////////////////////////////////
    //
    //  Options for customizing our scales
    //
    var options = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                },
                type: 'logarithmic',
                scaleLabel: {
                    display: true,
                    labelString: 'Count',
                    fontSize: 20
                }
            }]
        }
    };

    // Chart declaration (creating the chart with our data and options objects)
    if (window.myLineChart != undefined) {
        window.myLineChart.destroy();
        window.myLineChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        })
    } else {
        window.myLineChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        });
    }
    return true; 
}
//--TYPE
function generateCPTGraph(graphdata) {

    var canvas = document.getElementById("lineGraph"); // Referencing the ID of the <canvas> tag in our razor file
    var ctx = canvas.getContext('2d'); // This is a 2d graph
    //Default font options
    Chart.defaults.global.defaultFontColor = 'black';
    Chart.defaults.global.defaultFontSize = 16;

    ////////////////////////////////////////////////////////
    //
    //  Ensuring the Y axist scale does not show up in 
    //  'Scientific format' i.e (2E5), uses human readable
    //  decimal values instead
    //
    Chart.scaleService.updateScaleDefaults('logarithmic', {
        ticks: {
            callback: function (tick, index, ticks) {
                return tick.toLocaleString()
            }
        }
    });
    graphdata = JSON.parse(graphdata); // Parsing our serialized json object from our FetchCPS.razor file

    ////////////////////////////////////////////////////////
    //
    //  Data object to encapsulate x axis, and y axis data
    //
    var data = {
        labels: graphdata.Dates,
        datasets: []
    };

    ///////////////////////////////////////////////////////////////
    //
    //  Creating an object per status, each with randomized color
    //  also associates corrensponding data with its type
    //
    var i = 0; // to reference index of two dimensional array
    graphdata.Types.forEach(type => {
        var randomBetween = (min, max) => min + Math.floor(Math.random() * (max - min + 1));
        var r = randomBetween(0, 255);
        var g = randomBetween(0, 255);
        var b = randomBetween(0, 255);
        var rgb = `rgb(${r},${g},${b})`;
        var obj = {
            label: type,
            fill: false,
            lineTension: 0.1,
            backgroundColor: "rgba(225,0,0,0.4)",
            borderColor: rgb, // The main line color
            borderCapStyle: 'square',
            borderDash: [], // try [5, 15] for instance
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: "black",
            pointBackgroundColor: "white",
            pointBorderWidth: 1,
            pointHoverRadius: 8,
            pointHoverBackgroundColor: "yellow",
            pointHoverBorderColor: "brown",
            pointHoverBorderWidth: 2,
            pointRadius: 4,
            pointHitRadius: 10,
            data: graphdata.TypeCounts[i],
            spanGaps: true,
        }
        i++;
        data.datasets.push(obj);
    });

    //////////////////////////////////////////////////////
    //
    //  Options for customizing our scales
    //
    var options = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                },
                type: 'logarithmic',
                scaleLabel: {
                    display: true,
                    labelString: 'Count',
                    fontSize: 20
                }
            }]
        }
    };

    // Chart declaration (creating the chart with our data and options objects)
    if (window.myLineChart != undefined) {
        window.myLineChart.destroy();
        window.myLineChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        })
    } else {
        window.myLineChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        });
    }
    return true;
}
