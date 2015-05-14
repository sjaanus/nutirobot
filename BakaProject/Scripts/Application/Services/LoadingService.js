HomeModule.service("LoadingService", ['$resource','ngProgress', '$document', function ($resource, ngProgress, $document) {
	this.start = function (stayVisible) {
		if (!stayVisible) {
			$('#viewContainer').hide();
		}
		ngProgress.setParent($document.find('#viewBarContainer')[0]);
		ngProgress.start();
	}
 
	this.complete = function () {
		ngProgress.complete();
		$('#viewContainer').show();
	}
}]);