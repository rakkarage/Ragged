"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/editHub").build();

connection.on("ReceiveMessage", function (message) {
	var message = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	document.getElementById("edit").value = message;
});

connection.start().catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("edit").addEventListener("onchange", function (event) {
	var message = document.getElementById("edit").value;
	connection.invoke("SendMessage", message).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});
