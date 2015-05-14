HomeModule.controller('ResourceController', ['$scope', '$http', 'User', 'toaster', 'Navigator', 'LoadingService', 'FileUploader', 'Resources',
	function ($scope, $http, User, toaster, Navigator, LoadingService, FileUploader, Resources) {
		LoadingService.complete();
		$scope.uploader = new FileUploader({
			url: 'api/ResourceUpload',
			headers: {
				'Authorization': "Bearer " + localStorage.getItem('satellizer_token')
			},
			onSuccessItem: function () {
				$scope.getImages();
			},
			onErrorItem: function () {
				toaster.pop('error', "Error", "Faili üles laadimine ebaõnnestus");
			}
		});

		$scope.getImages  = function() {
			Resources.query(function (resources) {
				$scope.resources = resources;
			});
		}
		$scope.getImages();
		
		$scope.openResourceWindow = function(resource) {
			$scope.resource = resource;
			$scope.resourceWindow.center();
			$scope.resourceWindow.open();
		}

		$scope.delete = function (resource) {
			var user = Resources.delete({ id: resource.Id }, function () {
				$scope.getImages();
				$scope.resourceWindow.close();
				toaster.pop('success', "Õnnestus", "Faili kustutamine õnnestus.");
			});
		}
}]);