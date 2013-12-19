var angularDirectives = angular.module('directives', []);     //Define the directive module

angularDirectives.directive('greet', function () {             //use as 'test-directive' in HTML
    return {
        template: '<h2>Greeting {{from}} to {{to}}</h2>',
        controller: function ($scope, $element, $attrs) {
            $scope.from = $attrs.from;
            $scope.to = $attrs.greet;
        }
    };
}).directive('ngConfirmClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var msg = attr.ngConfirmClick || "Are you sure?";
                    var clickAction = attr.confirmedClick;
                    element.bind('click', function (event) {
                        if (window.confirm(msg)) {
                            scope.$eval(clickAction);
                        }
                    });
                }
            };
        }]);