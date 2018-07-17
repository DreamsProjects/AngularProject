
angular.module('MyApp', []).
	controller('Pizza', function ($scope, $http) {

		$http.get("/api/GetPizzas").then(function (response) {
			$scope.Pizza = response.model;
		});
	}).
	controller('Food', ['$scope', '$http', function ($scope) {

		$http.get('/api/GetPizzas').success(function (data) {
			$scope.data = data.Name; //this scope object we can access in html
			$scope.message = "Hello";
		});
	}]).
	factory("FoodService", ["$http", function ($http) {

		var fac = {};

		fac.GetPizzasFromDb = function () {
			return $http.get("/api/GetPizzas");
		};

		fac.AddPizzaToCart = function (newPost) { //Lägger till i listan

			$http.post("/api/AddPizzaCart", newPost).success(function (response) {
				alert(response.status);
			});
		}

		fac.EditPizzaInCart = function (post) { //Ändrar från listan

			return $http.post("/api/EditPizzaCart", post).success(function (response) {
			});
		}

		fac.DeletePizzaInCart = function (id) { //Tar bort från listan

			$http.post("/api/RemovePizzaCart", id).success(function (response) { });
		}
		return fac;
	}]);

//app.config(["$routeProvider", function ($routeProvider) {

//	$routeProvider.when("/",
//		{
//			templateURL: "/api/food.html",
//			controller: "FoodController"
//		}).
//		when("/GetPizzas", {
//			templateURL: "/api/blog.html",
//			controller: "BlogController"
//		}).
//		otherwise({redirectTo:"/Home"})

//}])


//app .controller('Food', ['$scope', '$http', function ($scope, $http) {

//	$http.get('/api/GetPizzas').success(function (data) {
//		$scope.data = data.Name; //this scope object we can access in html
//	});
//}])


angular.module("SummerProjectApp.controllers", []).
	controller("MailController", function ($scope) {
		$scope.message = "Main Controller";
	}).
	controller("AddPostController", function ($scope) {

		$scope.message = "Add post";
	}).
	controller("EditPostController", function ($scope) {

		$scope.message = "Edit post";
	}).
	controller("DeletePostController", function ($scope) {

		$scope.message = "Remove post";
	}).

	factory("LoginService", ["$http", function ($http) {

		var fac = {};

		fac.GetPostFromDb = function () {
			return $http.get("/Blog/FindAllPost");
		};

		fac.GetUserById = function (id) {
			return $http.get("/Blog/FindAllPostById", { params: { id: id } });
		};

		fac.AddPost = function (newPost) {

			$http.post("/Blog/AddPost", newPost).success(function (response) {
				alert(response.status);
			});
		}

		fac.EditPost = function (post) {

			$http.post("/Blog/Edit", post).success(function (response) {
				alert(response.status);
			});
		}

		fac.DeletePost = function (id, userId) {

			$http.post("/Blog/Delete", { id: id, userId: userId }), success(function (response) {
				alert(response.status);
			});
		}

		return fac;
	}])









/*Ajax metoder om det går*/
/********************USER********************/

function getAllClients() {
	$.ajax({
		url: baseUrl + "Home/Login",
		type: "post",
		data: { variabel: infofromuser },
		success: function result() {
			//loggas in: success
		},
		error: function result() {
			//felmeddelande fail
		}
	});
}

function removeClient() { //håll hemlig:userId
	$.ajax({
		url: baseUrl + "Home/Delete",
		type: "post",
		data: { variabel: infofromuser },
		success: function result() {

		},
		error: function result() {

		}
	});
}

function changeClient() { //håll hemlig:userId
	$.ajax({
		url: baseUrl + "Home/Edit",
		type: "post",
		data: { variabel: infofromuser },
		success: function result() {

		},
		error: function result() {

		}
	});
}

/********************COMPETITION********************/

function saveRecord() { //håll hemlig:userId
	var input = {

	};

	$.ajax({
		url: baseUrl + "Home/Competition",
		type: "post",
		data: { variabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

/********************FOOD********************/

function Cart() {
	$.ajax({
		url: baseUrl + "Food/Cart",
		type: "post",
		data: { variabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

function RemoveFromCart() {
	$.ajax({
		url: baseUrl + "Food/RemoveFromCart",
		type: "post",
		data: { variabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

/********************ORDER********************/

function Orders() {
	$.ajax({
		url: baseUrl + "Order/Save",
		type: "post",
		data: { varuiabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}


/********************BLOG********************/

function Print() {
	$.ajax({
		url: baseUrl + "Blog/Print",
		type: "post",
		data: { varuiabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

function EditPost() {
	$.ajax({
		url: baseUrl + "Blog/Edit",
		type: "post",
		data: { varuiabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

function DeletePost() {
	$.ajax({
		url: baseUrl + "Blog/Print",
		type: "post",
		data: { varuiabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}

/********************Pizza********************/
function createPizza() {
	$.ajax({
		url: baseUrl + "Blog/Print",
		type: "post",
		data: { varuiabel: input },
		success: function result() {

		},
		error: function result() {

		}
	});
}
