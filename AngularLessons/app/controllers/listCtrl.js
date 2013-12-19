var ListCtrl = function ($scope, $location, Todo) {
    $scope.search = function () {
        Todo.query({ sort: $scope.sortOrder, desc: $scope.isDesc, limit: $scope.limit, offset: $scope.offset, q: $scope.query }, function (res) {
            $scope.more = res.length === $scope.limit;
            $scope.todos = $scope.todos.concat(res);
        }, function(res) {
            
        });
    };

    $scope.sort = function (col) {
        if ($scope.sortOrder === col) {
            $scope.isDesc = !$scope.isDesc;
        }
        $scope.sortOrder = col;
    };

    $scope.showMore = function () {
        $scope.offset += $scope.limit;
        $scope.search();
    };
    $scope.hasMore = function () {
        return $scope.more;
    };

    $scope.reset = function () {
        $scope.limit = 20;
        $scope.offset = 0;
        $scope.todos = [];
        $scope.more = true;
        $scope.search();
    };

    $scope.delete = function () {
        var item = this.item;
        Todo.delete({ id: item.TodoItemId }, function () {
            $('#todo_' + item.Id).fadeOut();
            $scope.todos.splice($scope.todos.indexOf(item), 1);
        }, function (err) {
            alert("sorry");
        });

    };

    $scope.sortOrder = "Priority";
    $scope.isDesc = false;

    $scope.reset();
};

var CreateCtrl = function ($scope, $location, Todo) {
    $("#DueDate").change(function () {
        if ($scope.item) {
            $scope.item.DueDate = $(this).val();
        }
    });
    $('#DueDate').datepicker();
    $scope.action = "Add";
    $scope.save = function () {
        Todo.save($scope.item, function () {
            $location.path('/');
        });
    };

};
var EditCtrl = function ($scope, $location, $routeParams, Todo) {
    $("#DueDate").change(function () {
        if ($scope.item) {
            $scope.item.DueDate = $(this).val();
        }
    });
    $('#DueDate').datepicker();
    $scope.action = "Save";
    var id = $routeParams.itemId;
    $scope.item = Todo.get({ id: id });
    $scope.save = function () {
        Todo.update({ id: id }, $scope.item, function () {
            $location.path('/');
        });
    };
};