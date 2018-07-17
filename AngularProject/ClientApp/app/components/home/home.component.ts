import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
	selector: 'home',
	templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
	user: string[] = [];

	constructor(private _service: Http) {
	}

	ngOnInit() {
		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});
	}
}
