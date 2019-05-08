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
	scrollMessages();
});

connection.on("ConnectMessage", (users) => {
	updateUsers(users);
});

connection.on("DisconnectMessage", (users) => updateUsers(users));

connection.on("SetName", (name) => updateName(name))

function updateName(name) {
	document.getElementById("nameInput").value = name;
}

function updateUsers(users) {
	var parent = document.getElementById("usersList");
	while (parent.firstChild)
		parent.removeChild(parent.firstChild);
	for (var id in users) {
		var li = document.createElement("li");
		li.textContent = id;
		parent.appendChild(li);
	}
	scrollUsers();
}

function filter(message) {
	return message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

function scrollMessages() {
	var element = document.getElementById("messagesList");
	element.scrollTop = element.scrollHeight;
}

function scrollUsers() {
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
