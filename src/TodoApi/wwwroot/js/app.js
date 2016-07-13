var TodoApp = angular.module("TodoApp", ["ngRoute", "ngResource"]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
    when('/', {
        controller: TodoListCtrl,
        templateUrl: 'templates/todolist.html'
    }).
    otherwise({
        redirectTo: '/'
    });
}]);

TodoApp.factory('Todo', function ($resource) {
    return $resource('http://localhost:62487/api/todo/:id',
        { id: '@id' },
        { update: { method: 'PUT' }
    });
});

var TodoListCtrl = function ($scope, $resource, $location, Todo) {
    // get all items from server
    $scope.items = Todo.query();

    // create new task item object -- will trigger display of create new task item form
    $scope.createNewItem = function () {
        $scope.newItem = {};
        $scope.newItem.save = function () {
            Todo.save({
                "Key": "",
                "Name": $('#newName').val(),
                "isComplete": false
            }, function () {
                delete $scope.newItem;
                $('#newName').val('');
                $scope.items = Todo.query();
            })
        };

        // hide the create new task form 
        $scope.newItem.cancel = function () {
            delete $scope.newItem;
            $('#newName').val('');
        }
    };

    // enable inline editing mode for a task item
    $scope.edit = function () {
        var itemKey = this.item.key;
        $scope.editItem = Todo.get({
            id: itemKey
        });
    };

    // update existing task item on server, end inline editing mode, and refresh items
    $scope.update = function () {
        Todo.update({ id: this.item.key }, {
            "Key": this.item.key,
            "Name": $('#name_' + this.item.key).val(),
            "isComplete": $('#status_' + this.item.key).is(':checked')
        }, function () {
            delete $scope.editItem;
            $scope.items = Todo.query();
        });
    };

    // delete task item from server and refresh list
    $scope.delete = function () {
        var itemKey = this.item.key;
        Todo.delete({ id: itemKey }, function () {
            $scope.items = Todo.query();
        });
    };
};
