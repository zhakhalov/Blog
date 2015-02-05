(function (angular) {
    angular.module('app', ['monospaced.elastic', 'maxLength', 'angularFileUpload'])
    .controller('avatarCtrl', ['$scope', '$http', 'FileUploader', function ($scope, $http, FileUploader) {
        $scope.allowUpload = false;
        $scope.uploader = new FileUploader();
        $scope.init = function (avatarUrl, uploadUrl) {
            $scope.avatarUrl = avatarUrl;
            $scope.uploader.url = uploadUrl;
        };
        $scope.uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        });
        $scope.uploader.onAfterAddingFile = function () {
            console.log($scope.uploader);
        };
        $scope.uploading = true;
        $scope.progress = 55;
    }])
    .controller('summaryCtrl', ['$scope', '$http', function ($scope, $http) {
        var url = '';
        var initial = '';
        $scope.allowSubmit = false;
        $scope.init = function (summaryUrl, summaryLimit, summary) {
            initial = $scope.summary = summary;
            $scope.summaryLimit = summaryLimit;
            url = summaryUrl;
        };
        $scope.change = function () {
            $scope.allowSubmit = $scope.summary !== initial;
        };
        $scope.update = function () {
            $http.post(url, { summary: $scope.summary })
            .success(function () {
                initial = $scope.summary;
                $scope.allowSubmit = $scope.summary !== initial;
            })
        }
    }])
})(angular);