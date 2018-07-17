import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
	selector: 'competition',
	templateUrl: './competition.component.html',
	styleUrls: ['./competition.component.css']
})
export class CompetitionComponent implements OnInit {

	bullet: number = 0;
	compBullet: number = 0;
	edit: boolean = false;
	user: string[] = [];
	number: number = 0;

	constructor(private _service: Http) {
	}

	ngOnInit() {
		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});
	}

	public LoadButton() {
		var play = new Player("load", this.bullet);
		var comp = new Computer("", this.compBullet);

		var chosed = comp.computerPlay(play.tostring());
		this.GamePlay(chosed, "load")
	}

	public ShotButton() {
		var play = new Player("shot", this.bullet);
		var comp = new Computer("", this.compBullet);
		var chosed = comp.computerPlay(play.tostring());
		this.GamePlay(chosed, "shot")
	}

	public GunButton() {
		var play = new Player("shotgun", this.bullet);
		var comp = new Computer("", this.compBullet);
		var chosed = comp.computerPlay(play.tostring());

		var chosed = comp.computerPlay(play.tostring());
		this.GamePlay(chosed, "shotgun")
	}

	public BlockButton() {
		var play = new Player("block", this.bullet);
		var comp = new Computer("", this.compBullet);
		var chosed = comp.computerPlay(play.tostring());
		this.GamePlay(chosed, "block")
	}

	public RestartGame() {
		var play = new Player("", 0);
		var comp = new Computer("", 0);
		this.bullet = 0;
		this.compBullet = 0;
		this.edit = false;
	}

	Win() {
		if (this.number == 1) {
			let valuable = <HTMLButtonElement>document.getElementById("#print");
			valuable;
		} 
	}

	SaveToDb() {

		var TblHighscore = { "Points": this.bullet };

		this._service.post("/winner", TblHighscore).subscribe(result => {
			this.RestartGame();
		});
	}

	public GamePlay(comp1: any, user: any) {
		var player = new Player(user, this.bullet);
		var computer = new Computer(comp1, this.compBullet);
		this.edit = false;

		if (comp1 === "shot" && user === "block") {
			if (computer.bullets >= 1) {
				computer.bullets--;
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du blockerar datorns skott";
			}
		}

		else if (comp1 === "load" && user === "load") {
			player.bullets++;
			computer.bullets++;

			let valuable = <HTMLElement>document.querySelector("#print");
			valuable.innerHTML = "Ni laddar båda era vapen";
		}

		else if (comp1 === "block" && user === "block") {
			let valuable = <HTMLElement>document.querySelector("#print");
			valuable.innerHTML = "Ni blockerar varandra";
		}

		else if (comp1 === "block" && user === "shot") {
			if (player.bullets >= 1) {
				player.bullets--;

				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Datorn blockerar ditt skott";
			}
			else {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du behöver ha ett skott för att kunna skjuta";
			}
		}

		else if (comp1 === "shot" && user === "shot") {
			if (this.bullet >= 1 && this.compBullet >= 1) {
				player.bullets--;
				computer.bullets--;

				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Ni skjuter på varandra och förlorar båda ett skott\n";
			}
		}

		else if (comp1 === "load" && user === "shot") {
			if (player.bullets >= 1) {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du skjuter medan datorn laddar sitt vapen, du vinner!!!!";
				this.edit = true;
				this.number = 1;
				this.Win();
			}
			else {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du behöver ha ett skott för att kunna skjuta";
			}
		}

		else if (comp1 === "shot" && user === "load") {
			if (computer.bullets >= 1) {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Datorn skjuter medan du laddar ditt vapen, datorn vinner!!!!";
				this.number = 0;
				this.edit = true;
			}
		}

		else if (comp1 === "load" && user === "block") {
			computer.bullets++;
			let valuable = <HTMLElement>document.querySelector("#print");
			valuable.innerHTML = "Du blockerar medan datorn laddar sitt vapen";
		}

		else if (comp1 === "block" && user === "load") {
			player.bullets++;
			let valuable = <HTMLElement>document.querySelector("#print");
			valuable.innerHTML = "Du laddar ditt vapen medan datorn blockerar";
		}

		else if (comp1 === "shotgun" && user === "shot" || user === "block" || user === "load") {
			if (computer.bullets >= 3) {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Datorn använde shotgun och vann!!!!";
				this.edit = true;
				this.number = 0;
			}
		}

		else if (comp1 === "shot" || comp1 === "block" || comp1 === "load" && user === "shotgun") {
			if (player.bullets >= 3) {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du använde shotgun och vann!!!!";
				this.edit = true;
				this.number = 1;
				this.Win();
			}
			else {
				let valuable = <HTMLElement>document.querySelector("#print");
				valuable.innerHTML = "Du behöver tre skott för att använda shotgun";
			}
		}
		this.bullet = player.bullets;
		this.compBullet = computer.bullets;
	}
}


export class Player {

	bullets: number = 0;
	player: string = "";

	constructor(value: any, bullet: any) {
		this.player = value;
		this.bullets = bullet;
	}

	tostring() {
		return this.player;
	}
}

export class Computer {
	bullets: number = 0;
	computer: string = "";


	constructor(value: any, compBullet: any) {
		this.computer = value;
		this.bullets = compBullet;
	}

	tostring() {
		return this.computer;
	}

	public computerPlay(choice: any) {
		var random = Math.floor(Math.random() * 3);

		if (random === 0) {
			this.computer = "load";
		}

		else if (random === 1) {
			this.computer = "block";
		}

		else if (random === 2 && this.bullets >= 1) {
			this.computer = "shot";
		}

		else if (random === 3 && this.bullets >= 3) {
			this.computer = "shotgun";
		}
		return this.computer
	}
}