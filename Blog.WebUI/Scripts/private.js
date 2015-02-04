(function (angular) {
    angular.module('app', ['monospaced.elastic', 'maxLength', 'angularFileUpload'])
    .controlelr('avatarCtrl', ['$scope', '$http', function ($scope, $http) {
    }])
    .controlelr('summaryCtrl', ['$scope', '$http', function ($scope, $http) {
        var url = '';
        var initial = $scope.summary;
        $scope.allowSubmit = false;
        $scope.change = function () {
            $scope.allowSubmit = $scope.summary !== initial;
        }
        $scope.init = function (updateUrl) {
            url = updateUrl;
        }
        $scope.update() = function () {
            $http.post(url, { summary: $scope.summary })
            .success(function () {
                initial = $scope.summary;
                $scope.allowSubmit = $scope.summary !== initial;
            })
        }
    }])
})(angular);