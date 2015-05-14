var HomeModule = angular.module('HomeModule',
	['ngRoute', 'ngResource', 'ngAnimate', 'kendo.directives', 'ngStorage', 'toaster', 'ui.bootstrap',
		'ui.grid', 'ui.grid.cellNav', 'ui.grid.selection', 'textAngular', 'satellizer', 'ngProgress', 'ngSanitize', 'ngTagsInput', 'angularFileUpload']);

HomeModule.config(['$routeProvider', '$locationProvider', '$httpProvider', '$provide', '$authProvider',
function ($routeProvider, $locationProvider, $httpProvider, $provide, $authProvider) {

	$provide.decorator('taOptions', ['taRegisterTool', '$delegate', '$rootScope', function (taRegisterTool, taOptions, $rootScope) {

		taOptions.toolbar = [
			  ['h1', 'h2', 'h3'],
			  ['bold', 'italics', 'underline', 'ul', 'ol'],
			  ['justifyLeft', 'justifyCenter', 'justifyRight'], ['insertImage']
		];
		//taOptions.toolbar[3].push('customInsertImage');
		return taOptions;
	}]);

	$authProvider.facebook({
		clientId: 'FACEBOOKCLIENTID'
	});

	$authProvider.google({
		clientId: 'GOOGLECLIENTID'
	});


	$routeProvider
		.when('/', { templateUrl: '/home/home', controller: 'HomeController' })
		.when('/browse', { templateUrl: '/browse/home', controller: 'BrowseController' })
		.when('/upload', { templateUrl: '/upload/home', controller: 'UploadController' })
		.when('/activate', { templateUrl: '/activate/home', controller: 'ActivateController' })
		.when('/share', {  })
		.when('/policy', {  })
		.when('/resource', { templateUrl: '/resource/home', controller: 'ResourceController' })
		.otherwise({ redirectTo: '/' });

	$locationProvider.html5Mode({
		enabled: true,
		requireBase: false
	});


	$provide.factory('TokenHttpInterceptor', ['$q', '$localStorage', 'toaster', function ($q, $localStorage, toaster) {
		return {
			// On request success
			request: function (config) {
				// Return the config or wrap it in a promise if blank.
				return config || $q.when(config);
			},

			// On request failure
			requestError: function (rejection) {
				//console.log(rejection); // Contains the data about the error on the request.

				// Return the promise rejection.
				return $q.reject(rejection);
			},

			// On response success
			response: function (response) {
				//console.log(response); // Contains the data from the response.

				// Return the response or promise.
				return response || $q.when(response);
			},

			// On response failture
			responseError: function (rejection) {
				toaster.pop('error', "Tõrge", rejection.statusText);
				// Return the promise rejection.
				return $q.reject(rejection);
			}
		};
	}]);

	// Add the interceptor to the $httpProvider.
	$httpProvider.interceptors.push('TokenHttpInterceptor');
}]);

HomeModule.run(['$route', angular.noop]);  // this helps the initial ng-view to load https://github.com/angular/angular.js/issues/1213