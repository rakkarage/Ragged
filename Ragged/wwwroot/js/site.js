"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("Index.html").build();

document.getElementById("SendButton").disabled = true;

connection.on("EditedTopic", (message) => {
	document.getElementById("topicInput").value = filter(message);
});

connection.on("ReceiveMessage", (user, message) => {
	var li = document.createElement("li");
	li.textContent = user + " says " + filter(message);
	document.getElementById("messagesList").appendChild(li);
	updateMessages();
});

connection.on("ConnectMessage", (user) => {
	var li = document.createElement("li");
	li.textContent = user;
	document.getElementById("usersList").appendChild(li);
	updateUsers();
});

connection.on("DisconnectMessage", (user) => {

});

function filter(message) {
	return message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

function updateMessages() {
	var element = document.getElementById("messagesList");
	element.scrollTop = element.scrollHeight;
}

function updateUsers() {
	var element = document.getElementById("usersList");
	element.scrollTop = element.scrollHeight;
}

connection.start().then(() => {
	document.getElementById("SendButton").disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("topicInput").addEventListener("input", () => {
	var value = document.getElementById("topicInput").value;
	connection.invoke("EditTopic", value).catch(err => console.error(err));
	event.preventDefault();
});

document.getElementById("SendButton").addEventListener("click", (event) => {
	var user = document.getElementById("nameInput").value;
	var message = document.getElementById("messageInput").value;
	connection.invoke("SendMessage", user, message).catch((err) => {
		return console.error(err.toString());
	});
	event.preventDefault();
});
