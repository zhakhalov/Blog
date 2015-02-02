(function (angular) {
    angular.module('app', [])
    .controller('list', ['$scope, $http', function ($scope) {
        var createUrl = '';

        $scope.available = [];
        $scope.tags = [];
        $scope.pending = false;
        $scope.init = function (tags, url) {
            $scope.available = tags.split(',');
            createUrl = url;
        };
        $scope.add = function (index) {
            $scope.tags.push($scope.available[index]);
            $scope.available.splice(index, 1);
        };
        $scope.remove = function (index) {
            $scope.available.push($scope.tags[index]);
            $scope.tags.splice(index, 1);
        };
        $scope.createTag = function () {
            $scope.pending = true;
            $http.post(url, { tag: $scope.newTag })
            .success(function () {
                $scope.available.push($scope.newTag);
                $scope.pending = false;
            });
        };
    }]);
})(window.angular);