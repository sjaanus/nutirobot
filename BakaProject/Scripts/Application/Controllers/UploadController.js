HomeModule.controller('UploadController', ['$scope', '$http', 'Exercise', 'toaster', 'LoadingService', 'textAngularManager', 'FileUploader', 'Resources', '$localStorage',
	function ($scope, $http, Exercise, toaster, LoadingService, textAngularManager, FileUploader, Resources, $localStorage) {
		$scope.user = $localStorage.user;
		$scope.exercise = new Exercise({ privacy: 1 });
		LoadingService.complete();
		$scope.upload = function () {
			$scope.exercise.$save(function () {
				toaster.pop('success', "Õnnestus", "Ülesande salvestamine õnnestus.");
			});
		}


		//textAngularManager.addTool("customInsertImage", {
		//	iconclass: "fa fa-picture-o",
		//	action: function ($deferred) {
		//		var textAngular = this;
		//		var savedSelection = rangy.saveSelection();
		//		$scope.resourceBrowseWindow.center();
		//		$scope.resourceBrowseWindow.open();
		//		//textAngular.$editor().wrapSelection('insertImage', "https://www.google.ee/images/nav_logo195.png");
		//		//$deferred.resolve();
		//		return false;
		//	}
		//});
	}]);