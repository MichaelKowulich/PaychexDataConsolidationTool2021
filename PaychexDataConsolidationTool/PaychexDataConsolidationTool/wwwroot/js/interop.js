//--ClientsPerStatus, ClientsPerType, UsersPerType, ClientsPerBrand Line Graphs
function generateLineGraph(graphdata) {

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
    graphdata = JSON.parse(graphdata); // Parsing our serialized json object from our FetchCPS/CPT/UPT etc.razor file

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
    graphdata.Entitys.forEach(Entity => {
        var randomBetween = (min, max) => min + Math.floor(Math.random() * (max - min + 1));
        var r = randomBetween(0, 255);
        var g = randomBetween(0, 255);
        var b = randomBetween(0, 255);
        var rgb = `rgb(${r},${g},${b})`;
        var obj = {
            label: Entity,
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
            data: graphdata.EntityCounts[i],
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
        },
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

//--HOME PAGE PI CHART GENERATION
function generatePieChart(graphdata) {
    graphdata = JSON.parse(graphdata);
    console.dir(graphdata);
    var canvas = document.getElementById(graphdata.EntityName); // Referencing the ID of the <canvas> tag in our razor file
    var ctx = canvas.getContext('2d'); // This is a 2d graph
    //Default font options
    Chart.defaults.global.defaultFontColor = 'black';
    var i = 0;
    var colors = [];
    graphdata.AllEntityNames.forEach(() => {
        var randomBetween = (min, max) => min + Math.floor(Math.random() * (max - min + 1));
        var r = randomBetween(0, 255);
        var g = randomBetween(0, 255);
        var b = randomBetween(0, 255);
        var rgb = `rgb(${r},${g},${b})`;
        colors[i] = rgb;
        i++
    });

    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: graphdata.AllEntityNames,
            datasets: [{
                label: 'Count',
                data: graphdata.AllEntityCountsPerName,
                backgroundColor: colors,
                borderWidth: 1,
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
        }
    })
        return true;
}

//--HOME PAGE PI CHART GENERATION
function generateBrandPieChart(graphdata) {
    graphdata = JSON.parse(graphdata);
    console.dir(graphdata);
    var canvas = document.getElementById(graphdata.EntityName); // Referencing the ID of the <canvas> tag in our razor file
    var ctx = canvas.getContext('2d'); // This is a 2d graph
    //Default font options
    Chart.defaults.global.defaultFontColor = 'black';

    var data = {
        labels: graphdata.AllBrands,
        datasets: []
    };
    var colors = [];
    for (let i = 0; i < graphdata.AllBrands.length; i++) {
        let randomBetween = (min, max) => min + Math.floor(Math.random() * (max - min + 1));
        let r = randomBetween(0, 255);
        let g = randomBetween(0, 255);
        let b = randomBetween(0, 255);
        let rgb = `rgb(${r},${g},${b})`;
        colors[i] = rgb;
    }
    var j = 0;
    graphdata.AllCountTypes.forEach(CountType => {
        console.log(CountType);
        let obj = {
            label: CountType,
            data: graphdata.BrandData[j],
            backgroundColor: colors,
        }
        data.datasets.push(obj);
        j++;
    });

    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                callbacks: {
                    label: function (item, data) {

                        return data.datasets[item.datasetIndex].label
                            + ": " + data.datasets[item.datasetIndex].data[item.index];
                    }
                }
            }
        }
    })
    return true;
}
