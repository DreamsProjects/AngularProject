import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
	selector: 'user',
	templateUrl: './user.component.html',
})

export class UserComponent implements OnInit {
	user: string[] = [];
	email: string = "";
	password: string = "";
	results: string[] = [];
	notDeliveredToday: string[] = [];
	deliveredToday: string[] = [];
	orderInfo: string[] = [];
	moneyToday: number = 0;
	moneyYear: number = 0;
	getValueOfClient: string[] = [];
	notNull: number = 0;
	numberNow: number = 0;
	userOrders: string[] = [];

	constructor(private _service: Http) {
	}

	ngOnInit() {
		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});

		this._service.get("/order/userOrders").subscribe(result => {
			this.userOrders = result.json() as string[];
		});

		this._service.get("/order/delivered").subscribe(result => {
			this.deliveredToday = result.json() as string[];
		});

		this._service.get("/order/today").subscribe(result => {
			this.moneyToday = result.json() as number;
		});

		this._service.get("/order/year").subscribe(result => {
			this.moneyYear = result.json() as number;
		});

		this._service.get("/order/notDelivered").subscribe(result => {
			this.notDeliveredToday = result.json() as string[];
		});
	}

	public delivered() {
		var TblOrder = { "OrderId": this.numberNow };

		this._service.post("/markAsDelivered", TblOrder).subscribe(result => {
		});
	}

	giveInfo(values: any, ints: any): void {
		this.notNull = ints;
		this.numberNow = values;

		var TblOrder = { "OrderId": values };

		this._service.post("/getOrderClient", TblOrder).subscribe(result => {
			this.getValueOfClient = result.json() as string[];
		});

		this._service.post("/orderInfo", TblOrder).subscribe(result => {
			this.orderInfo = result.json() as string[];
		});
	}

	change(infoForm: NgForm) {
		var name = infoForm.value["Names"];
		var lastName = infoForm.value["LastNames"];
		var phone = infoForm.value["Phones"];
		var address = infoForm.value["Addresss"];
		var email = infoForm.value["Emails"];
		var password = infoForm.value["Passwords"];

		var TblPerson = { "FirstName": name, "LastName": lastName, "Telephone": phone, "Address": address, "Email": email, "Pass": password };

		this._service.post("/changeInfo", TblPerson).subscribe(result => {
			window.location.reload();
		})
	}

	createUser(userForm: NgForm) {
		var name = userForm.value["Name"];
		var lastName = userForm.value["LastName"];
		var phone = userForm.value["Phone"];
		var address = userForm.value["Address"];
		var email = userForm.value["Email"];
		var password = userForm.value["Password"];

		var TblPerson = { "FirstName": name, "LastName": lastName, "Telephone": phone, "Address": address, "Email": email, "Pass": password };
		
		this._service.post("/api/createUser", TblPerson).subscribe(result => {
			window.location.reload();
		})
	}

	login(loginForm: NgForm) {
		this.email = loginForm.value["Email"];
		this.password = loginForm.value["Pass"];

		var TblPerson = { "Email": this.email, "Pass": this.password };

		this._service.post("/api/login", TblPerson).subscribe(result => {
			this.results = result.json() as string[];
			window.location.reload();
		})
	}

	logout(logoutForm: NgForm) {

		this._service.get("/LogOut").subscribe(result => {
			this.user = result.json() as string[];
			window.location.reload();
		});
	}
}


