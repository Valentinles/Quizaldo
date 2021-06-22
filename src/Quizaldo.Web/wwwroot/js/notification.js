"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
var notificationsToday = document.getElementsByClassName('notification');
var ctr = notificationsToday.length;
var sp = document.createElement('span');
var ntIcon = document.getElementById('notification-icon');
sp.style.cssText = 'position: relative; top: 5px; left: 5px; color: pink; font-family: "Lucida Console", "Courier New", monospace; font-size: x-small; font-weight: 800;';
connection.on("sendNotification", () => {
    if (ntIcon.hasChildNodes()) {
        ctr++;
        sp.innerText = ctr;
        ntIcon.style.color = 'pink';
    }
    else {
        ctr++;
        sp.innerText = ctr;
        ntIcon.style.color = 'pink';
        ntIcon.appendChild(sp);
    }


    
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});