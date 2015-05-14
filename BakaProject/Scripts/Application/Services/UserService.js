HomeModule.factory("User", ['$resource', function ($resource) {
	return $resource('api/user/:id', { id: '@id' },
		{
			activate: {
				url: 'api/user/activate/:id',
				method: 'POST'
			},
			reject: {
				url: 'api/user/reject/:id',
				method: 'POST'
			},
			info: {
				url: 'api/user/info',
				method: 'GET'
			}
		})
}]);