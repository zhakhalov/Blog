(function () {
    $(function () {
        var _private = {
            $comment: $('#comment'),
            commentCount: new CountGroup($('#comment'), 320)
        };
        $('.content', _private.$comment).autosize();

        $('.btn-comment').click(function () {
            $.ajax({
                type: 'POST',
                url: $('.action', _private.$comment).val(),
                data: {
                    comment: $('.content', _private.$comment).val(),
                    id: $('.id', _private.$comment).val()
                },
                success: function (data) {
                    _private.$comment.after(data);
                }
            })
        })
    });
})(jQuery);