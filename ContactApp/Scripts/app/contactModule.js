/// <reference path="../angular.js" />
var app = angular.module('contacts', ['ngRoute']);

app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
        .when('/', {
            controller: 'ListCtrl',
            templateUrl: '/Routes/list.html'
        })
        .when('/new', {
            controller: 'CreateCtrl',
            templateUrl: '/Routes/addEdit.html'
        })
        .when('/edit/:contactId', {
            controller: 'EditCtrl',
            templateUrl: '/Routes/addEdit.html'
        })
        .otherwise({
            redirectTo: '/'
        });
});

app.factory('contactFactory', function ($http) {
    var factory = {};
    var url = '/api/Contacts/';

    factory.getContacts = function () {
        return $http.get(url);
    };

    factory.getContactById = function (id) {
        return $http.get(url + id);
    };

    factory.updateContact = function (id, contact) {
        return $http.put(url + id, contact);  // angular.toJson(contact)
    };

    factory.deleteContact = function (id) {
        return $http.delete(url + id);
    };

    factory.createContact = function (contact) {
        return $http.post(url, contact);
    };

    return factory;
});

app.controller('ListCtrl', function ($scope, contactFactory, $routeParams) {
    contactFactory.getContacts()
        .then(function (data) {
            $scope.contacts = data.data;
        }, function (message, status) {
            alert(message + ' status: ' + status);
        });
    $scope.delete = function (contactID, FirstName, LastName) {
        var torf = confirm('Are you sure you want to delete: ' + FirstName + ' ' + LastName + '?');
        if (torf) {
            contactFactory.deleteContact(contactID)
                .then(function (data) {
                    contactFactory.getContacts()
                        .then(function (data) {
                            $scope.contacts = data.data;
                        }, function (message, status) {
                            alert(message + ' status: ' + status);
                        });
                }, function (message, status) {
                    alert(message + ' status: ' + status);
                });
        };
    };
});

app.controller('EditCtrl', function ($scope, $location, $routeParams, contactFactory) {
    $scope.showNew = false;
    $scope.showEdit = true;

    contactFactory.getContactById($routeParams.contactId)
        .then(function (data) {
            $scope.contact = data.data;
        }, function (message, status) {
            alert(message + ' status: ' + status + ' Edit');
        });

    $scope.showDelete = false;

    $scope.delete = function () {
        contactFactory.deleteContact($routeParams.contactId)
            .then(function () {
                $location.path('/');
            }, function (message, status) {
                alert(message + ' status: ' + status);
            });
    };

    $scope.save = function () {
        contactFactory.updateContact($routeParams.contactId, $scope.contact)
            .then(function () {
                $location.path('/');
            }, function (message, status) {
                alert(message + ' status: ' + status);
            });
    };
});

app.controller('CreateCtrl', function ($scope, $location, contactFactory) {
    $scope.showNew = true;
    $scope.showEdit = false;
    $scope.save = function () {
        contactFactory.createContact($scope.contact)
            .then(function () {
                $location.path('/');
            }, function (message, status) {
                alert(message + ' status: ' + status + ' Create');
            });
    };

    $scope.showDelete = false;
});