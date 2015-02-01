(function () {
    $(function () {
        var _private = {
            contentCount: new CountGroup($('#content'), 5000),
            titleCount: new CountGroup($('#title'), 100),
            tags: {
                $result: $('.result', $('#tags')),
                $addTags: $('.add-tag', $('#tags')),
                $removeTags: $('.remove-tag', $('#tags'))
            }
        };
        $('.content', $('#content')).autosize();

        $('.btn-tag', _private.$addTags).each(function () {
            var btn = $(this);
            var click = function () {
                var parent = (btn.parent('div.remove-tag').length)
                    ? _private.tags.$addTags
                    : _private.tags.$removeTags
                // swap button parent
                btn.remove();
                parent.append(btn);
                btn.click(click);

                var result = _private.tags.$result.val();

                if (parent == _private.tags.$addTags) {
                    result = result.replace(',' + btn.text(), '');      // remove tag
                } else {
                    result = result + ',' + btn.text();                 // add tag
                }
                result = result.replace(/^,/, '');                      // trim first ","
                _private.tags.$result.val(result);

                console.log(_private.tags.$result.val());
            }
            btn.click(click);
        });
    })
})(jQuery);