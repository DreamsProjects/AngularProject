import { Component, OnInit, Inject, NgModule } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';

@Component({
	selector: 'food',
	templateUrl: './food.component.html',
	styleUrls: ['./food.component.css']
})
export class FoodComponent implements OnInit {
	apiValues: string[] = [];
	holdValue: number = 0;
	list: string[] = [];
	clickedValues: string[] = [];
	getValues: string[] = [];
	valueOfCategory: number = 0;
	money: number = 0;
	strings: string = "";
	number: number = 0;
	user: string[] = [];
	name: string = "";
	id: number = 0;
	error: string = "";

	constructor(private _service: Http) {
	}

	ngOnInit() {
		this._service.get("/api/GetPizzas").subscribe(result => {
			this.apiValues = result.json() as string[];
		});

		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});

		this._service.get("/getSize").subscribe(result => {
			this.list = result.json() as string[];
		});
	}

	modal(values: any): void {
		this.holdValue = values.pizzaId;

		var valuable = (<HTMLInputElement>document.getElementById('valuable')).value;

		this.name = values.name;
		this.id = values.pizzaId;

		var value = this.number;

		if (value == 7 || value == 11) { //Om vanlig
			this.money = values.price;
		}

		else if (value == 8 || value == 12) { //Om familj
			this.money = values.price + 90;
		}
		else {
			this.money = 0;
		}

		this._service.get("/Id", { params: { id: this.holdValue } }).subscribe(result => {
			this.clickedValues = result.json() as string[];
		})
	}

	countMoney(value: any) {
		var valuable = (<HTMLInputElement>document.getElementById('valuable')).value;

		var y = parseInt(valuable);

		if (value == 7 || value == 11) { //Om vanlig
			this.money = y;
		}

		else { //Om familj
			this.money = y + 90;
		}

		this.number = value;
	}

	toListRoll(ToListForm: NgForm) {
		var price = 70;
		var alternative = 6;
		var id = this.id;

		var TblPizza = { "PizzaId": id, "CategoryId": alternative, "Price": price };

		this._service.post("/cart", TblPizza).subscribe(result => { 
			window.location.reload();
		})
	}

	toListSalad(ToListForm: NgForm) {
		var price = 75;
		var alternative = 4;
		var id = this.id;

		var TblPizza = { "PizzaId": id, "CategoryId": alternative, "Price": price };

		this._service.post("/cart", TblPizza).subscribe(result => { 
			window.location.reload();
		})
	}

	toList(ToListForm: NgForm) {
		var price = this.money;
		var alternative = this.number;
		var id = this.id;

		var TblPizza = { "PizzaId": id, "CategoryId": alternative, "Price": price };

		this._service.post("/cart", TblPizza).subscribe(result => { //int id, int size, decimal price
			if (result.status == 200) {
				window.location.reload();
			}
		})
	}
}

