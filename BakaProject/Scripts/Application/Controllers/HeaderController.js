HomeModule.controller('HeaderController',
	['$scope', '$http', '$location', '$localStorage', 'User', 'Navigator', '$window', '$auth',
	function ($scope, $http, $location, $localStorage, User, Navigator, $window, $auth) {
		$scope.storage = $localStorage;

		$scope.updateInfo = function () {
			new User().$info(function (response) {
				$scope.storage.user = response;
			});
		}

		if ($auth.isAuthenticated()) {
			$scope.updateInfo();
		}

		$scope.logout = function () {
			$auth.logout();
			Navigator.refresh();
		};

		$scope.goHome = function () {
			$location.path("/");
		}

		$scope.authenticate = function (provider) {
			$auth.authenticate(provider).then(function (response) {
				if ($auth.isAuthenticated()) {
					// refresh stuff
					$scope.updateInfo();
					Navigator.refresh();
				}
			});
		};

		$scope.isAuthenticated = function () {
			return $auth.isAuthenticated();
		}
	}]);