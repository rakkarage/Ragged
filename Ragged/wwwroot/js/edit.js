"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/editHub").build();

connection.on("EditedMessage1", function (message) {
	var value = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	document.getElementById("edit1").value = value;
});
connection.on("EditedMessage2", function (message) {
	var value = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	document.getElementById("edit3").value = value;
});
connection.on("EditedMessage3", function (message) {
	var value = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	document.getElementById("edit3").value = value;
});

connection.start().catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("edit1").addEventListener("change", () => {
	var value = document.getElementById("edit1").value;
	connection.invoke("EditMessage1", value).catch(err => console.error(err));
	event.preventDefault();
});
document.getElementById("edit2").addEventListener("onkeyup", () => {
	var value = document.getElementById("edit2").value;
	connection.invoke("EditMessage2", value).catch(err => console.error(err));
	event.preventDefault();
});
document.getElementById("edit3").addEventListener("input", () => {
	var value = document.getElementById("edit3").value;
	connection.invoke("EditMessage3", value).catch(err => console.error(err));
	event.preventDefault();
});
