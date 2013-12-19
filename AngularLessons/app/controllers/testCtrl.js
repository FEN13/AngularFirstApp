var TestCtrl = function($scope, $location, $http) {
    //$scope.phones = [{
    //    'name': 'Nexus S',
    //    'snippet': 'Fast just got faster with Nexus S.',
    //    'age': 1
    //},
    //{
    //    'name': 'Motorola XOOM™ with Wi-Fi',
    //    'snippet': 'The Next, Next Generation tablet.',
    //    'age': 2
    //},
    //{
    //    'name': 'MOTOROLA XOOM™',
    //    'snippet': 'The Next, Next Generation tablet.',
    //    'age': 3
    //}];

    $http.get('/app/data/phones.json').success(function(data) {
        $scope.phones = data;
    });

    $scope.orderProp = 'age';
};