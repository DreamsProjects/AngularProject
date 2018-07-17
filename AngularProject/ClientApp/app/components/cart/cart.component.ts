import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormsModule, NgForm } from '@angular/forms';


@Component({
	selector: 'cart',
	templateUrl: './cart.component.html'
})

export class CartComponent implements OnInit {

	hello: string = "";
	list: string[] = [];
	count: number = 0;
	clickedOnCard: boolean = true;

	constructor(private _service: Http) {

	}

	ngOnInit() {
		this.hello = "Welcome";

		this._service.get("/read").subscribe(result => {
			this.list = result.json() as string[];
		});

		this._service.get("/count").subscribe(result => {
			this.count = result.json() as number;
		});
	}

	modal(values: any): void  {
	}

	public paidCash() {

		var TblOrder = { "TotalAmount": this.count, "CardOrCash": "Cash" };

		this._service.post("/sendOrder", TblOrder).subscribe(result => {
			this.list = result.json() as string[];
			window.location.reload();
		});
	}

	public paidCard() {
		var TblOrder = { "TotalAmount": this.count, "CardOrCash": "Card" };

		this._service.post("/sendOrder", TblOrder).subscribe(result => {
			this.list = result.json() as string[];
			window.location.reload();
		});
	}
}