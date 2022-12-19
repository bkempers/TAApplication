/**
 * Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        12/6/2022
 * Course:      CS 4540, University of Utah, School of Computing
 * Copyright:   CS 4540 and [Ben Kempers and Trevor Koenig] - This work may not be copied for use in Academic Coursework.
 *
 * I, Trevor Koenig and Ben Kempers, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *    Contains JS functionality for HighCharts GUI and AdminController endpoint
 *    
 */

const enrollmentChart = Highcharts.chart('linegraph_container', {
    title: {
        text: 'Enrollments Over Time',
        align: 'left'
    },

    subtitle: {
        text: 'Enrollments Over Time',
        align: 'center'
    },

    yAxis: {
        title: {
            text: 'Students'
        }
    },

    xAxis: {
        title: {
            text: 'Dates'
        },
        type: 'datetime',
        accessibility: {
            rangeDescription: 'Range: November 1 to Present Day'
        },
        labels: {
            formatter: function () {
                return Highcharts.dateFormat('%d %b', +this.value);
            }
        },
        tickInterval: 24 * 3600 * 1000
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
            pointStart: Date.UTC(2022, 10, 12),
            pointInterval: 24 * 3600 * 1000 // one day
        }
    },

    series: [{
        name: 'CS 1400',
        data: [0, 0, 0, 0, 0, 1, 1, 1, 43, 44, 57, 65, 84, 85, 90, 91, 94, 96, 98, 99, 99, 101, 102, 105, 109, 110, 110, 110, 113]
    }, {
        name: 'CS 1410',
        data: [0, 0, 0, 0, 3, 3, 3, 3, 127, 141, 167, 173, 189, 193, 195, 201, 207, 213, 217, 223, 223, 222, 224, 229, 231, 233, 235, 235, 235]
    }, {
        name: 'CS 1420',
        data: [0, 0, 0, 0, 0, 0, 0, 1, 44, 52, 61, 66, 72, 75, 76, 76, 81, 80, 80, 85, 85, 88, 89, 91, 94, 94, 94, 95, 95]
    }, {
        name: 'CS 2100',
        data: [0, 0, 0, 0, 4, 5, 5, 5, 45, 51, 83, 91, 108, 110, 114, 118, 125, 128, 129, 130, 131, 132, 134, 138, 143, 143, 144, 146, 147]
    }, {
        name: 'CS 2420',
        data: [0, 0, 0, 0, 6, 7, 7, 7, 156, 166, 201, 210, 233, 237, 243, 249, 254, 258, 261, 265, 266, 268, 269, 270, 276, 277, 278, 281, 284]
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

//ABOVE AND BEYOND - Secondary graph (pie chart)

var cs1400 = [0, 0, 0, 0, 0, 1, 1, 1, 43, 44, 57, 65, 84, 85, 90, 91, 94, 96, 98, 99, 99, 101, 102, 105, 109, 110, 110, 110, 113].reduce((partialSum, a) => partialSum + a, 0);
var cs1410 = [0, 0, 0, 0, 3, 3, 3, 3, 127, 141, 167, 173, 189, 193, 195, 201, 207, 213, 217, 223, 223, 222, 224, 229, 231, 233, 235, 235, 235].reduce((partialSum, a) => partialSum + a, 0);
var cs1420 = [0, 0, 0, 0, 0, 0, 0, 1, 44, 52, 61, 66, 72, 75, 76, 76, 81, 80, 80, 85, 85, 88, 89, 91, 94, 94, 94, 95, 95].reduce((partialSum, a) => partialSum + a, 0);
var cs2100 = [0, 0, 0, 0, 4, 5, 5, 5, 45, 51, 83, 91, 108, 110, 114, 118, 125, 128, 129, 130, 131, 132, 134, 138, 143, 143, 144, 146, 147].reduce((partialSum, a) => partialSum + a, 0);
var cs2420 = [0, 0, 0, 0, 6, 7, 7, 7, 156, 166, 201, 210, 233, 237, 243, 249, 254, 258, 261, 265, 266, 268, 269, 270, 276, 277, 278, 281, 284].reduce((partialSum, a) => partialSum + a, 0);


const piechart = new Highcharts.chart('piegraph_container', {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: 'Above & Beyond: Pie Chart Enrollments',
        align: 'center'
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    accessibility: {
        point: {
            valueSuffix: '%'
        }
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
            }
        }
    },

    series: [{
        name: 'Course Enrollments',
        colorByPoint: true,
        data: [{
            name: 'CS 1400',
            y: cs1400
        }, {
            name: 'CS 1410',
            y: cs1410
        }, {
            name: 'CS 1420',
            y: cs1420
        }, {
            name: 'CS 2100',
            y: cs2100
        }, {
            name: 'CS 2420',
            y: cs2420
        }]
    }]
});

//get enrollment data 
function enrollment_submit() {
    console.log("button clicked")
    var start_date = document.getElementById("start_date").value;
    console.log(start_date)
    var end_date = document.getElementById("end_date").value;
    console.log(end_date)

    var course_form = document.getElementById("course_select");
    var course = course_form.options[course_form.selectedIndex].text
    console.log(course)
    if (validation()) // Calling validation function
    {
        // document.getElementById("enrollment_form").submit(); //form submission
        GetEnrollments(start_date, end_date, course);
    }
}

function GetEnrollments(start_date, end_date, courseDeptNum) {
    let returnVal = '';
    var courseValues;
    var course;
    $.get(
        {
            url: "/Admin/GetEnrollmentData",
            data: { startDate: start_date, endDate: end_date, courseDeptNum: courseDeptNum },
            success: function (data) {
                returnVal = data
            }
        })
        .done(function (data) {
            console.log(data);

            var course_form = document.getElementById("course_select");
            course = course_form.options[course_form.selectedIndex].text

            var courseSet = data.split("\n");
            console.log("dates ", courseSet)
            var dates = courseSet[0].split(",")
            console.log("dates ", dates)
            var values = courseSet[1].split(",").map(Number)

            // remove all existing series from chart
            //while (enrollmentChart.series.length > 1) {
                //enrollmentChart.series[0].remove();
            //}

            //console.log(course)
            //console.log(values)
            //enrollmentChart.series[0].setData(values, false);
            //enrollmentChart.series[0].update({
                //name: enrollmentChart.name ? null : course
            //});

            //$('#container').highcharts().redraw();
            //enrollmentChart.series.
            // enrollmentChart.([{ name: course, data: values }], true, true, false)
            //enrollmentChart.series.

            $('#linegraph_container').highcharts().addSeries({
                name: course,
                data: values,
            })

            courseValues = values.reduce((partialSum, a) => partialSum + a, 0);
            $('#piegraph_container').highcharts().series[0].addPoint({
                name: course,
                y: courseValues
            });
            //$('#piegraph_container').highcharts().redraw();
            //piechart.series[0].addPoint(course, courseValues);
        });

    //piechart.series[0].addPoint([{
    //    name: course,
    //    y: courseValues
    //}]);

    return returnVal;
}

function validation() {
    var start_date = document.getElementById("start_date").value;
    var end_date = document.getElementById("end_date").value;
    var course_form = document.getElementById("course_select");
    var course = course_form.options[course_form.selectedIndex].text

    if (start_date === '' || end_date == '' || course == "Select a Course") {
        alert("Please fill all fields!");
        return false;
    }
    else {
        return true;
    }
}
