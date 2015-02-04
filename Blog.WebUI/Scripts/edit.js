(function (angular) {
    angular.module('app', ['monospaced.elastic', 'textAngular', 'maxLength'])
    .controller('textController', ['$scope', '$http', function ($scope) {
        var checkUrl = ';'
        $scope.init = function (url) {
            createUrl = url;
        };
        $scope.titleChange = function () {
            $http.post(url, { title: $scope.title })
            .success(function (data) {
                $scope.titleExists = data.exists;
            })
        }
    }])
    .controller('tagController', ['$scope', '$http', function ($scope, $http) {
        var createUrl = '';

        $scope.available = [];
        $scope.tags = [];
        $scope.pending = false;
        $scope.result = '';
        $scope.newTag = '';
        $scope.existsTag = false;
        $scope.init = function (available, tags, url) {
            $scope.available = available.split(',');
            $scope.tags = tags.split(',');
            createUrl = url;
        };
        $scope.add = function (index) {
            $scope.tags.push($scope.available[index]);
            $scope.available.splice(index, 1);
            $scope.result = $scope.tags.join(',');
        };
        $scope.remove = function (index) {
            $scope.available.push($scope.tags[index]);
            $scope.tags.splice(index, 1);
            $scope.result = $scope.tags.join(',');
        };
        $scope.tagChange = function () {
            $scope.existsTag = (-1 != $scope.tags.indexOf($scope.newTag))
               || (-1 != $scope.available.indexOf($scope.newTag));
        };
        $scope.createTag = function () {
            if (!$scope.newTag.length
                || (-1 != $scope.tags.indexOf($scope.newTag))
                || (-1 != $scope.available.indexOf($scope.newTag)))
            { return; }
            $scope.pending = true;
            $http.post(createUrl, { tag: $scope.newTag })
            .success(function () {
                $scope.available.push($scope.newTag);
                $scope.pending = false;
            });
        };
    }]);
})(angular);