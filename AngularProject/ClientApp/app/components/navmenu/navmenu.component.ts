import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
	selector: 'nav-menu',
	templateUrl: './navmenu.component.html',
	styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
	user: string[] = [];
	count: number = 0;
	list: string[] = [];
	valuesInCart: string[] = [];

	constructor(private _service: Http) {
	}

	ngOnInit() {
		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});

		this._service.get("/read").subscribe(result => {
			this.list = result.json() as string[];

			if (this.list == null) {
				this.count = 0;
			}

			else {
				this.count = this.list.length;
			}
		});
	}

	public removing(pizzaId: any, categoryId: any) {

		var PizzaViewModel = { "PizzaId": pizzaId, "CategoryId": categoryId };

		this._service.post("/RemoveFromCart", PizzaViewModel).subscribe(result => {
			window.location.reload();
		})
	}


	logout() {

		this._service.get("/LogOut").subscribe(result => {
			this.user = result.json() as string[];
			window.location.reload();
		});
	}
}
