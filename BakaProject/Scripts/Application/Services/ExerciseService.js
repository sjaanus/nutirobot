HomeModule.factory("Exercise", ['$resource', function ($resource) {
	return $resource('api/exercise/:id', { id: '@id' },
		{
			share: {
				url: 'api/exercise/share',
				method: 'POST'
			},
			getShare: {
				url: 'api/exercise/share/get',
				method: 'GET'
			},
			getFile: {
				url: 'api/exercise/share/file',
				method: 'POST',
				responseType: 'arraybuffer',
				transformResponse: function (data, headersGetter) {
					return { data: data };
				}
			}
		})
}]);