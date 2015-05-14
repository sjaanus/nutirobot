HomeModule.factory("Navigator", ['$resource', function ($resource) {
	var updater;
	return {
		refresh: function () {
			updater();
		},
		setUpdater: function (upd) {
			updater = upd;
		}
	};
}]);