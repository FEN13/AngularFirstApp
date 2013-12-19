var TodoApp = angular.module("TodoApp", ["ngResource", "ngRoute", "directives"]).
    config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: IndexCtrl, templateUrl: 'home.html' })
            .when('/todos', { controller: ListCtrl, templateUrl: 'list.html' })
            .when('/new', { controller: CreateCtrl, templateUrl: 'details.html' })
            .when('/edit/:itemId', { controller: EditCtrl, templateUrl: 'details.html' })
            .when('/test', { controller: TestCtrl, templateUrl: 'testPage.html' })
            .when('/error', { controller: ErrorCtrl, templateUrl: 'errorPage.html' })
            .otherwise({ redirectTo: '/' });
    });
