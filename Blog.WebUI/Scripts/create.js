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
        $scope.result = "";
        $scope.init = function (tags, url) {
            $scope.available = tags.split(',');
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

//(function ($) {
//    $(function () {
//        var _private = {            
//            contentCount: new CountGroup($('#content'), 5000),
//            titleCount: new CountGroup($('#title'), 100),
//            tags: {
//                tags: [],
//                createUrl: $('#createUrl').val(),
//                $result: $('.result', $('#tags')),
//                $addTags: $('.add-tag', $('#tags')),
//                $removeTags: $('.remove-tag', $('#tags'))
//            }
//        };
//        $('.content', $('#content')).autosize();
//        $('#create-tag').click(function () {
//            if (!$('#new-tag').val().length) { return; }
//            var newTag = $('#new-tag').val();
//            $('#create-tag').attr('disabled', 'disabled');
//            $.ajax({
//                method: 'POST',
//                data: { tag: newTag },
//                success: function (data) {
//                    $addTags.append($(data))
//                    $('#create-tag').removeAttr('disabled');
//                }
//            })
//        });
//        $('.btn-tag', _private.$addTags).each(function () {
//            var btn = $(this);
//            var click = function () {
//                var parent = (btn.parent('div.remove-tag').length)
//                    ? _private.tags.$addTags
//                    : _private.tags.$removeTags
//                // swap button parent
//                btn.remove();
//                parent.append(btn);
//                btn.click(click);

//                var result = _private.tags.$result.val().split(',');

//                if (parent == _private.tags.$addTags) {
//                    _private.tags.tags.splice(_private.tags.tags.indexOf(btn.text()), 1);
//                } else {
//                    _private.tags.tags.push(btn.text());
//                }                
//                _private.tags.$result.val(result.join(','));

//                console.log(_private.tags.$result.val());
//            }
//            btn.click(click);
//        });
//    })
//})(jQuery);

