import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormsModule, NgForm } from '@angular/forms';


@Component({
	selector: 'blog',
	templateUrl: './blog.component.html'
})
export class BlogComponent implements OnInit {

	user: string[] = [];
	title: string = "";
	text: string = "";
	food: string[] = [];
	results: string[] = [];
	uniqeblog: string[] = [];
	allblog: string[] = [];
	id: string[] = [];

	constructor(private _service: Http) {

	}

	ngOnInit() {
		this._service.get("/api/GetUser").subscribe(result => {
			this.user = result.json() as string[];
		});

		this._service.get("/api/GetPizzas").subscribe(result => {
			this.food = result.json() as string[];
		});


		this._service.get("/findPost").subscribe(result => {
			this.uniqeblog = result.json() as string[];
		});

		this._service.get("/getPostById").subscribe(result => {
			this.allblog = result.json() as string[];
		});
	}

	getValue(id: any) {
		this.title = id;
	}

	blog(blogForm: NgForm):void {
		this.text = blogForm.value["text"];

		var TblPost = { "Title": this.title, "Text": this.text };

		this._service.post("/addPost", TblPost).subscribe(result => {
			this.results = result.json() as string[];
			window.location.reload();
		})
	}


	public removing(value:any) {
		var TblPost = { "PostId": value.postId};

		this._service.post("/deletePost", TblPost).subscribe(result => {
			this.results = result.json() as string[];
			window.location.reload();
		})
	}
}
