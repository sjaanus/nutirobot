HomeModule.factory("Resources", ['$resource', function ($resource) {
	return $resource('api/resourceupload/:id', { id: '@id' },
		{
			query: {
				isArray: true,
				method: 'GET'
			}
		})
}]);