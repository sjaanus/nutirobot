HomeModule.controller('HomeController', ['$scope', '$http', '$location', '$localStorage', 'toaster', 'Navigator', 'LoadingService',
	function ($scope, $http, $location, $localStorage, toaster, Navigator, LoadingService) {
		LoadingService.complete();

	}]);