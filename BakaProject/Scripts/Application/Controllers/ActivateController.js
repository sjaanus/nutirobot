HomeModule.controller('ActivateController', ['$scope', '$http', 'User', 'toaster', 'Navigator', 'LoadingService',
	function ($scope, $http, User, toaster, Navigator, LoadingService) {

		$scope.gridOptions = {
			columnDefs: [
				{ name: 'Name', displayName: 'Nimi' },
				{
					name: 'UserLink', displayName: 'Sotsiaalmeediavõrgustiku konto',
					enableCellEdit: false,
					cellTemplate: '<a href="{{row.entity[col.field]}}" target="_blank">{{row.entity[col.field]}}</a>'
				},
				{
					name: 'Action',
					cellEditableCondition: false,
					cellTemplate:
					'<button kendo-button class="k-primary" ng-click="grid.appScope.activate(row.entity)">Lisa kasutaja õpetajarolli</button>' +
					'<button style="margin-left:5px;"kendo-button class="k-primary" ng-click="grid.appScope.reject(row.entity)">Lükka tagasi</button>'
				}
			],
			enableRowHeaderSelection: false,
			multiSelect: false,
			modifierKeysToMultiSelect: false,
			noUnselect: true,
			enableHorizontalScrollbar: 0
		};

		$scope.refreshExercises = function () {
			$http({
				type: "GET",
				url: "api/user/nonactive",
				dataType: "json"
			}).success(function (data) {
				$scope.gridOptions.data = data;
				setTimeout(function () { // ugly hack, ui grid does not play very nice with kendo
					$(window).trigger('resize'); // have to trigger resize event, because grid does not play well with kendo splitter
				}, 200);				
				LoadingService.complete();
			});
		}

		$scope.refreshExercises();

		$scope.activate = function (dataItem) {
			var user = new User();
			user.$activate({ id: dataItem.Id }, function (u) {
				toaster.pop('success', "Õnnestus", "Kasutaja õpetajaks lisamine õnnestus.");
				$scope.finish();
			});
		}

		$scope.reject = function (dataItem) {
			var user = new User();
			user.$reject({ id: dataItem.Id }, function (u) {
				toaster.pop('success', "Õnnestus", "Kasutaja taotlus lükati tagasi.");
				$scope.finish();
			});
		}
		$scope.finish = function () {
			$scope.refreshExercises();
			Navigator.refresh();
		}
	}]);