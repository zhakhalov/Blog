(function ($, angular) {
    angular.module('app', ['monospaced.elastic', 'maxLength'])
    .controller('RateCtrl', ['$scope', '$http', function ($scope, $http) {
        var id = '';
        var url = '';
        $scope.init = function (users, username, isAuthenticated, rating, articleId, rateUrl) {
            $scope.canRate = (isAuthenticated && (-1 == users.indexOf(username)));
            $scope.rating = rating;
            $scope.positive = $scope.rating >= 0;
            id = articleId;
            url = rateUrl;
        };
        $scope.like = function (isLike) {
            $scope.canRate = false;
            $http.post(url, { articleId: id, like: isLike })
            .success(function (date) {
                $scope.rating += (date.like) ? 1 : -1;
                $scope.color = $scope.rating >= 0 ? 'green' : 'coral';
            })
        };
    }])
    .controller('CommentCtrl', ['$scope', '$http', function ($scope, $http) {
        var url = '';
        var id = '';
        $scope.init = function (canComment, articleId, commentUrl, limit) {
            $scope.canComment = canComment;
            $scope.limit = limit;
            id = articleId;
            url = commentUrl;
        };
        $scope.addComment = function () {
            $scope.pending = true
            $http.post(url, { comment: $scope.comment, id: id })
            .success(function (data) {
                $scope.pending = false;
                $('#comment').after(data);
            });
        }
    }]);
})(window.jQuery, window.angular);