import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
	selector: 'create',
	templateUrl: './create.component.html'
})
export class CreateComponent implements OnInit{

	name: string = "";
	pizzaName: string = "";
	pizzaId: string = "";
	IngreId: string = "";
	price: number = 5;
	food: string[] = [];
	products: string[] = [];
	pizzas: string[] = [];
	ingredients: string[] = [];
	id: number = 0;
	id2: number = 0;
	number: number = 0;
	chosen: number = 0;
	ingred: number = 0;
	

	constructor(private _service: Http, private router: Router) {

	}

	ngOnInit() {
		this._service.get("/getIngredients").subscribe(result => {
			this.products = result.json() as string[];
		});

		this._service.get("/api/GetPizzas").subscribe(result => {
			this.pizzas = result.json() as string[];
		});

		this._service.get("/connections").subscribe(result => {
			this.ingredients = result.json() as string[];
		});

		this._service.get("/category").subscribe(result => {
			this.food = result.json() as string[];
		});
	}

	giveValue(id: any) {
		this.number = id;
	}

	getValue(id: any) {
		this.id = id;
	}

	getOtherValue(id2: any) {
		this.id2 = id2;
	}

	getChoosenIdPizza(value: any) {
		this.chosen = value;
	}

	getChoosenIdIngr(value: any) {
		this.ingred = value;
	}


	public removing(value: any) {
		alert(value.pizzaId);
		var TblPizza = { "PizzaId": value.pizzaId };

		this._service.post("/removePizza", TblPizza).subscribe(result => {
			window.location.reload();
		})
	}

	update(updateForm: NgForm) {
		this.name = updateForm.value["pizzaName"];
		this.price = updateForm.value["prices"];

		var TblPizza = { "PizzaId": this.number, "Price": this.price, "CategoryId": this.id2, "Name": this.name };

		this._service.post("/updatePizza", TblPizza).subscribe(result => {
			window.location.reload();
		})
	}

	create(productForm: NgForm) {
		this.name = productForm.value["nameIng"];
		this.price = productForm.value["price"];

		var TblIngredient = { "Name": this.name, "Price": this.price };

		this._service.post("/makeIngredients", TblIngredient).subscribe(result => {
			window.location.reload();
		})	
	}

	createPizza(pizzaForm: NgForm) {
		this.pizzaName = pizzaForm.value["name"];
		this.price = pizzaForm.value["prices"];

		var TblPizza = { "Name": this.pizzaName, "CategoryId": this.id, "Price": this.price };
		console.log("TblPizza;" + TblPizza);
		this._service.post("/createPizza", TblPizza).subscribe(result => {
			window.location.reload();
		})
	}

	Pizza(createPizzaForm: NgForm) {	//name och pizzanamn återanvänd, skapa senare: så man kan lägga till fler ingredienser på samma gång

		
		this.IngreId = createPizzaForm.value["ingred"];

		var TblPizzaIngredients = { "PizzaId": this.chosen, "IngredientId": this.ingred };

		this._service.post("/makePizzas", TblPizzaIngredients).subscribe(result => {
			window.location.reload();
		})
	}
}
