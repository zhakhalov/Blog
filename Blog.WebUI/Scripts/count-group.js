var CountGroup = function (group, limit) {
    var _private = {
        limit: limit,
        text: $('.content', group),
        count: $('.count', group)
    };
    _private.text.keyup(function () {
        _private.count.text(_private.limit - _private.text.val().length);
    });
    _private.text.keydown(function () {
        if (_private.text.val().length >= _private.limit) {
            _private.text.val(_private.text.val().substring(0, _private.limit));
        }
    })
    _private.count.text(_private.limit);
}