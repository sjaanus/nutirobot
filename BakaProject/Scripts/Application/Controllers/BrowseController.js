HomeModule.controller('BrowseController', ['$scope', '$http', 'Exercise', 'toaster', 'LoadingService', '$timeout', '$window', '$localStorage', '$auth',
	function ($scope, $http, Exercise, toaster, LoadingService, $timeout, $window, $localStorage, $auth) {
		$scope.isAuthenticated = $auth.isAuthenticated();

		$scope.user = $localStorage.user;
		$scope.display = {
			Title: true,
			Question: true,
			Tip: true,
			Answer: true,
			Tags: true
		};
		$scope.downloadPDF = function (dataItem) {
			$scope.getShareObject(dataItem).$share(function (link) {
				var win = window.open("http://htmltopdfapi.com/querybuilder/api.php?url=" + link.Message, '_blank');
				win.focus();
				console.log(link);
			});
			//$scope.getShareObject(dataItem).$getFile(function (result) {
			//	var data = result.data;
			//	var blob = new Blob([data]);
			//	var link = document.createElement('a');
			//	link.href = window.URL.createObjectURL(blob);
			//	link.download = "ylesanne.pdf";
			//	link.click();
			//});
		}
		$scope.update = function (dataItem) {
			var exercise = new Exercise();
			angular.extend(exercise, dataItem);
			exercise.$save(function (u) {
				toaster.pop('success', "Õnnestus", "Ülesande muutmine õnnestus.");
				$scope.viewWindow.close();
			});
		}
		$scope.delete = function (dataItem) {
			var exercise = new Exercise();
			exercise.$delete({ id: dataItem.Id }, function (u) {
				toaster.pop('success', "Õnnestus", "Ülesande kustutamine õnnestus.");
				$scope.viewWindow.close();
				$scope.refreshExercises();
			});
		}
		$scope.share = function (dataItem) {
			$scope.getShareObject(dataItem).$share(function (link) {
				$scope.shareLink = link.Message;
				$scope.shareWindow.center();
				$scope.shareWindow.open();
			});
		}
		$scope.getShareObject = function (dataItem, callback) {
			var exercise = new Exercise();
			angular.extend(exercise, dataItem);

			Object.keys($scope.display).forEach(function (key) {
				if (!$scope.display[key]) {
					exercise[key] = null;
				}
			});
			return exercise;
		}
		$scope.gridOptions = {
			columnDefs: [
				{ name: 'Title', displayName: 'Tiitel' },
				{ name: 'User', displayName: 'Kasutaja' },
				{ name: 'PrivacyString', displayName: 'Privaatsus' },
				{ name: 'TagsText', displayName: 'Sildid' }
			],
			enableRowSelection: true,
			enableRowHeaderSelection: false,
			multiSelect: false,
			modifierKeysToMultiSelect: false,
			noUnselect: true,
			onRegisterApi: function (gridApi) {
				$scope.gridOptions.gridApi = gridApi;
				gridApi.selection.on.rowSelectionChanged($scope, function (row) {
					$scope.dataItem = row.entity;
					$scope.viewWindow.center();
					$scope.viewWindow.open();
				});
			},
			enableHorizontalScrollbar: 0
		};

		$scope.refreshExercises = function () {
			$http({
				type: "GET",
				url: "api/exercise",
				dataType: "json",
				params: {
					allowedTags: $scope.allowedTags ? $scope.allowedTags.map(function (value) { return value.text }) : null,
					deniedTags: $scope.deniedTags ? $scope.deniedTags.map(function (value) { return value.text }) : null,
				}
			}).success(function (data) {
				$scope.gridOptions.data = data;
				LoadingService.complete();
				$(window).trigger('resize'); // have to trigger resize event, because grid does not play well with kendo splitter
			});
		}

		$scope.refreshExercises();

		$scope.windowOptions = {
			height: $(document).height() - 150,
			width: $(document).width() - 350,
			close: function (e) {
				$scope.gridOptions.gridApi.selection.clearSelectedRows();
			}
		}
	}]);