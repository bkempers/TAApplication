/**
 * Global access to the PIXI Application 
 */
var app = null;
var is_mouse_down = false;
var buttonDefaultColor = 0x33b2ff;
var buttonClickedColor = 0xffe48f;
var disBtwnLines = 0;
var startButtonY = 0;
var endButtonY = 0;

var buttonArray = new Array();

/**
 *  Create PIXI stage
 */
function setup_pixi_stage(width, height) {
    app = new PIXI.Application({ backgroundColor: 0x000000 });
    app.renderer.resize(width, height);
    $("#canvas_avail").append(app.view);
    disBtwnLines = app.renderer.height / 64;
}

class AvailabilityTable extends PIXI.Graphics {
    constructor() {
        super();

        let width = app.renderer.width / 20;
        let height = app.renderer.height / 16;

        for (let i = 2; i < 15; i++) {
            this.lineStyle(1, 0xffffff);
            this.moveTo(width * 2, height * i)
            this.lineTo(width * 16, height * i);
        }
    }
}

function set_time_day() {
    // draw times on left of table
    let width = app.renderer.width / 16;
    let height = app.renderer.height / 16;
    startButtonY = height * 2;
    endButtonY = height * 14;

    for (let i = 2; i < 15; i++) {
        var time = get_time(i);
        console.log(time);

        const timeStyle = new PIXI.TextStyle({
            fontFamily: 'Arial',
            fontSize: 20,
            fill: ['#ffffff'],
        });

        const timeText = new PIXI.Text(time, timeStyle);
        timeText.x = width * 14;
        timeText.y = (height * i) - 10;

        app.stage.addChild(timeText);
    }

    width = app.renderer.width / 7;
    console.log(app.renderer.width)
    height = app.renderer.height / 32;
    // draw days on top of table
    for (let i = 1; i < 6; i++) {
        var day = get_day(i);
        console.log(day);

        const dayStyle = new PIXI.TextStyle({
            fontFamily: 'Arial',
            fontSize: 20,
            fill: ['#ffffff'],
        });

        const dayText = new PIXI.Text(day, dayStyle);
        // have to do extra math to compensate for width of text
        dayText.x = (width * (i)) - Math.pow(day.length, 1.5);
        dayText.y = height;

        // set all buttons for that day at the same starting x coordinate
        this.set_availability_buttons(dayText.x, app.renderer.height, i);

        app.stage.addChild(dayText);
    }
}

function get_time(i) {
    let time;
    switch (i) {
        case 2:
            time = "8:00am";
            break;
        case 3:
            time = "9:00am";
            break;
        case 4:
            time = "10:00am";
            break;
        case 5:
            time = "11:00am";
            break;
        case 6:
            time = "12:00pm";
            break;
        case 7:
            time = "1:00pm";
            break;
        case 8:
            time = "2:00pm";
            break;
        case 9:
            time = "3:00pm";
            break;
        case 10:
            time = "4:00pm";
            break;
        case 11:
            time = "5:00pm";
            break;
        case 12:
            time = "6:00pm";
            break;
        case 13:
            time = "7:00pm";
            break;
        case 14:
            time = "8:00pm";
            break;
    }
    return time;
}

function get_day(i) {
    let day;
    switch (i) {
        case 1:
            day = "Monday";
            break;
        case 2:
            day = "Tuesday";
            break;
        case 3:
            day = "Wednesday";
            break;
        case 4:
            day = "Thursday";
            break;
        case 5:
            day = "Friday";
            break;
    }
    return day;
}

/*
 * BUTTON MANAGEMENT HERE 
 */

class AvailabilityButton extends PIXI.Graphics {
    constructor(x, y, id, i, startingHeight) {
        super();

        this.beginFill(buttonDefaultColor);
        this.drawRect(0, 0, 75, disBtwnLines);
        this.x = x;
        this.y = startingHeight + (y * i);
        this.interactive = true;
        this.id = id + i - 1;
        this.isClicked = false;
        this.i = i;

        console.log(this.id);
        this.date = get_day(id);
        console.log(this.date);
        this.time = get_button_time(i, id);
        console.log(this.time);
    }

    toString()
    {
        return `${this.isClicked}`;
    }
}

function set_availability_buttons(x, y, id) {
    let height = (endButtonY - startButtonY) / 48;
    for (let i = 0; i < 48; i++) {
        var button = new AvailabilityButton(x, height, id, i, startButtonY);
        app.stage.addChild(button);

        button.on('mousedown', mouse_down);
        button.on('mouseover', mouse_over);
        button.on('mouseup', mouse_up);

        buttonArray.push(button);
    }
}

function get_button_time(i, id) {
    
    let minutes;
    switch (i%4) {
        case (0):
            minutes = ":00";
            break;
        case (1):
            minutes = ":15";
            break;
        case (2):
            minutes = ":30";
            break;
        case (3):
            minutes = ":45";
            break;
    }

    /* first we divide i by 4 and floor it to normalize it to an hour (every four buttons changes the hour)
     * next we modulo by 12 and add 2 to normalize it to the get_time cases
     * then we split the returned time by semicolon so we can get just the hour value
     * finally return the first section of the split string by accessing only the first index of the returned array
     * 
     * it's not efficient by any means but it works...
     */
    let hourArray = get_time((Math.floor(i / 4) % 12) + 2).split(":");
    let hour = hourArray[0];
    // then we can use the second part of the hour to determine am/pm
    let am_pm = hourArray[1].slice(2);

    time = `${hour}${minutes}${am_pm}`;
    return time;
}


function mouse_down() {
    console.log('mouse down on button' + this.id);
    if (this.isClicked) {
        this.clear();
        this.beginFill(buttonDefaultColor);
        this.drawRect(0, 0, 75, disBtwnLines);
        this.isClicked = false;
    }
    else {
        this.clear();
        this.beginFill(buttonClickedColor);
        this.drawRect(0, 0, 75, disBtwnLines);
        this.isClicked = true;
    }
    is_mouse_down = true;
    document.getElementById("saveWarning").style.visibility = "visible";
    document.getElementById("saveSuccess").style.display = "none";
    document.getElementById("saveWarning").style.display = "block";

    // uncomment this to see how the buttons change
    // printButtons()
}

function mouse_up() {
    console.log('mouse up on button' + this.id);
    console.log("index var:" + this.i);
    console.log('button time: ' + this.time);
    console.log('button day: ' + this.date);

    is_mouse_down = false;
}

function mouse_over() {
    console.log('mouse over on button' + this.id);
    if (is_mouse_down) {
        if (this.isClicked) {
            this.clear();
            this.beginFill(buttonDefaultColor);
            this.drawRect(0, 0, 75, disBtwnLines);
            this.isClicked = false;
        }
        else {
            this.clear();
            this.beginFill(buttonClickedColor);
            this.drawRect(0, 0, 75, disBtwnLines);
            this.isClicked = true;
        }
    }
}

function printButtons()
{
    for (let i = 0; i < buttonArray.length; i++)
    {
        console.log("" + buttonArray[i].isClicked)
    }
}

function PostAvailabilityRequest(userId) {

    toggleSpinner(true);

    let availabilityTimes = "";
    for (let i = 0; i < buttonArray.length; i++) {
        availabilityTimes += `${(+buttonArray[i].isClicked)}`;
    }

    console.log(userId);
    $.post(
        {
            url: "/Availabilities/SetSchedule",
            data: { availabilitySlots : availabilityTimes, id: userId }
        })
        .done(function (data) {
            console.log(data);
            document.getElementById("saveWarning").style.display = "none";
            document.getElementById("saveError").style.display = "none";
            document.getElementById("saveSuccess").style.display = "block";
            toggleSpinner(false);
        })
        .fail(function (data) {
            console.log(data);
            document.getElementById("saveError").style.display = "block";
            toggleSpinner(false);
        });
}

function toggleSpinner(state) {
    if (state) {
        document.getElementById("save-text").style.display = "none";
        let loadingElements = document.getElementsByClassName('load-spinner');
        for (let i = 0; i < loadingElements.length; i++) {
            loadingElements[i].style.display = "inline-block"
        }
    }
    else {
        document.getElementById("save-text").style.display = "block";
        loadingElements = document.getElementsByClassName('load-spinner');
        for (let i = 0; i < loadingElements.length; i++) {
            loadingElements[i].style.display = "none"
        }
    }
}

function GetAvailabilityRequest(userID) {
    let availabilityTimes = "";
    $.get(
        {
            url: "/Availabilities/GetSchedule",
            data: { id : userID }
        })
        .done(function (data) {
            console.log(data);
            for (let i = 0; i < data.length; i++)
            {
                if (data[i] == '1') {
                    buttonArray[i].isClicked = true;
                    buttonArray[i].beginFill(buttonClickedColor);
                    buttonArray[i].drawRect(0, 0, 75, disBtwnLines);
                }
                // for error prevention we will only check for 1's and assume everything else is a 0
                else {
                    buttonArray[i].isClicked = false;
                    buttonArray[i].beginFill(buttonDefaultColor);
                    buttonArray[i].drawRect(0, 0, 75, disBtwnLines);
                }
            }
        });
    return availabilityTimes;
}

function set_availability_data(availabilities) {
    console.log(availabilities);
}
