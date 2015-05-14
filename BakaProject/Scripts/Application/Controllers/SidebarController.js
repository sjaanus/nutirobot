HomeModule.controller('SidebarController', ['$scope', '$http', '$location', '$localStorage', 'toaster', 'Navigator', 'LoadingService',
	function ($scope, $http, $location, $localStorage, toaster, Navigator, LoadingService) {
		$scope.changeView = function (item) {
			$location.path(item.route);
		}

		// hiding until shown
		$scope.$on('$routeChangeStart', function (next, current) {
			LoadingService.start();
		});

		$(document).ajaxError(function (e, xhr, settings) {
			if (xhr.getResponseHeader("ShowError")) {
				$scope.$apply(function () {
					toaster.pop('error', "Error", xhr.statusText);
				});
			}
		});
		//$.ajaxSetup({
		//	beforeSend: function (xhr) {
		//		xhr.setRequestHeader('Authorization', "Bearer " + localStorage.getItem('satellizer_token'));
		//	}
		//});

		$scope.updateNavigator = function () {
			$http({
				method: "GET",
				url: "api/sidebar",
				dataType: "json"
			}).success(function (e) {
				console.log(e);
				$scope.tabs = e;
			});
		}
		$scope.updateNavigator();
		Navigator.setUpdater($scope.updateNavigator);
	}]);