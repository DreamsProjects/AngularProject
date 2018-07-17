"use strict";
var bullet = 0;
var compBullet = 0;

console.log("This is running");

var Player = class Player {
	constructor(value, bullet) {
		this.player = value;
		this.bullets = bullet;
	}

	tostring() {
		return this.player;
	}
}

var Computer = class Computer {
	constructor(value, bullet) {
		this.computer = value;
		this.bullets = compBullet;
	}

	tostring() {
		return this.computer;
	}

	computerPlay(choice) {
		var random = Math.floor(Math.random() * 3);

		if (random === 0) {
			var letter = new Computer("load").tostring();
		}

		else if (random === 1) {
			var letter = new Computer("block").tostring();
		}

		else if (random === 2 && compBullet >= 1) {
			var letter = new Computer("shot").tostring();
		}

		else if (random === 3 && compBullet >= 3) {
			var letter = new Computer("shotgun").tostring();
		}

		GamePlay(letter, choice);
	}
}

function LoadButton() {
	console.log("This is running");
	var play = new Player("load", bullet);
	var comp = new Computer("");
	comp.computerPlay(play.tostring());
}

function ShotButton() {
	var play = new Player("shot", bullet);
	var comp = new Computer("");
	comp.computerPlay(play.tostring());
}

function GunButton() {
	var play = new Player("shotgun", bullet);
	var comp = new Computer("");
	comp.computerPlay(play.tostring());
}

function BlockButton() {
	var play = new Player("block", bullet);
	var comp = new Computer("");
	comp.computerPlay(play.tostring());
}

function RestartGame() {
	$(function () //JQuery
	{
		$('#Restart').show();
		$("#Restart").click(function () {
			var play = new Player("", 0);
			var comp = new Computer("", 0);
			bullet = 0;
			compBullet = 0;
			document.getElementById('print').innerHTML = "";
			document.getElementById('Play').innerHTML = 0;
			document.getElementById('Comp').innerHTML = 0;
			$('#Restart').hide();
		});
	});
}

function GamePlay(comp1, user) {
	var player = new Player(user, bullet);
	var computer = new Computer(comp1, compBullet);

	if (comp1 === "shot" && user === "block") {
		if (computer.bullets >= 1) {
			computer.bullets--;
			document.getElementById('print').innerHTML = "Du blockerar datorns skott";
		}
	}

	else if (comp1 === "load" && user === "load") {
		player.bullets++;
		computer.bullets++;
		document.getElementById('print').innerHTML = "Ni laddar båda era vapen";
	}

	else if (comp1 === "block" && user === "block") {
		document.getElementById('print').innerHTML = "Ni blockerar varandra ";
	}

	else if (comp1 === "block" && user === "shot") {
		if (player.bullets >= 1) {
			player.bullets--;
			document.getElementById('print').innerHTML = "Datorn blockerar ditt skott";
		}
		else
			document.getElementById('print').innerHTML = "Du behöver ha ett skott för att kunna skjuta";
	}

	else if (comp1 === "shot" && user === "shot") {
		if (bullet >= 1 && compBullet >= 1) {
			player.bullets--;
			computer.bullets--;
			document.getElementById('print').innerHTML = "Ni skjuter på varandra och förlorar båda ett skott\n";
		}
	}

	else if (comp1 === "load" && user === "shot") {
		if (player.bullets >= 1) {
			document.getElementById('print').innerHTML = "Du skjuter medan datorn laddar sitt vapen, du vinner!!!!";
			RestartGame();
		}
		else
			document.getElementById('print').innerHTML = "Du behöver ha ett skott för att kunna skjuta";
	}

	else if (comp1 === "shot" && user === "load") {
		if (computer.bullets >= 1) {
			document.getElementById('print').innerHTML = "Datorn skjuter medan du laddar ditt vapen, datorn vinner!!!!";
			RestartGame();
		}
	}

	else if (comp1 === "load" && user === "block") {
		computer.bullets++;
		document.getElementById('print').innerHTML = "Du blockerar medan datorn laddar sitt vapen";
	}

	else if (comp1 === "block" && user === "load") {
		player.bullets++;
		document.getElementById('print').innerHTML = "Du laddar ditt vapen medan datorn blockerar";
	}

	else if (comp1 === "shotgun" && user === "shot" || user === "block" || user === "load") {
		if (computer.bullets >= 3) {
			document.getElementById('print').innerHTML = "Datorn använde shotgun och vann!!!!";
			RestartGame();
		}
	}

	else if (comp1 === "shot" || comp1 === "block" || comp1 === "load" && user === "shotgun") {
		if (player.bullets >= 3) {
			document.getElementById('print').innerHTML = "Du använde shotgun och vann!!!!";
			RestartGame();
		}
		else
			document.getElementById('print').innerHTML = "Du behöver tre skott för att använda shotgun";
	}

	bullet = player.bullets;
	compBullet = computer.bullets;
	document.getElementById('Play').innerHTML = player.bullets;
	document.getElementById('Comp').innerHTML = computer.bullets;
}