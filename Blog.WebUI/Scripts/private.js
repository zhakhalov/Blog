(function (angular) {
    angular.module('app', ['monospaced.elastic', 'maxLength', 'angularFileUpload', 'directives.inputMatch'])
    .controller('avatarCtrl', ['$scope', 'FileUploader', function ($scope, FileUploader) {
        $scope.allowUpload = false;
        $scope.uploader = new FileUploader({ queueLimit: 1 });
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
        $scope.uploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.avatarUrl = response.url.replace('~', '');
            $scope.uploader.clearQueue();
        };
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
        $scope.submit = function () {
            $scope.submited = false;
            initial = $scope.summary;
            $scope.allowSubmit = $scope.summary !== initial;
            $http.post(url, { summary: $scope.summary })
            .success(function () {
                $scope.submited = true;                
            });
        };
    }])
    .controller('passCtrl', ['$scope', '$http', function ($scope, $http) {
        var url = '';
        $scope.init = function (changePassUrl) {
            url = changePassUrl;
        };
        $scope.submit = function () {
            $http.post(url, { oldPassword: $scope.oldPassword, newPassword: $scope.newPassword })
            .success(function () {
                $scope.changed = true;
                $scope.submited = true;
            });
        };
    }]);
})(angular);