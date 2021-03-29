function generateCPSGraph(graphdata){
    var canvas = document.getElementById("lineGraph");
    var ctx = canvas.getContext('2d');
    Chart.defaults.global.defaultFontColor = 'black';
    Chart.defaults.global.defaultFontSize = 16;
    Chart.scaleService.updateScaleDefaults('logarithmic', {
        ticks: {
            callback: function (tick, index, ticks) {
                return tick.toLocaleString()
            }
        }
    });
    graphdata = JSON.parse(graphdata);

    var data = {
        labels: graphdata.Dates,
        datasets: []
    };
    var i = 0;
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
            // notice the gap in the data and the spanGaps: true
            data: graphdata.StatusCounts[i],
            spanGaps: true,
        }
        i++;
        data.datasets.push(obj);
    });
    // Notice the scaleLabel at the same level as Ticks
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

    // Chart declaration:
    var myBarChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: options
    });
    return true; 
}
