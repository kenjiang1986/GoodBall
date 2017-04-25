(function (window, $, ui, Vue) {
    function template(score, parent, scope) {
        var $parent = $(parent);
        $parent.empty();
        for (var i = 1; i <= 5; i++) {
            $('<a/>', {
                score: i,
                class: score >= i ? 'star filled' : 'star'
            }).html('\u2605').appendTo($parent);
        }
        if ($parent.attr('canset')) {
            $('a', $parent).one('click', function () {
                scope.vm[scope.params.value] = $(this).attr('score');
                template($(this).attr('score'), parent, scope);
            });
        }
    }

    Vue.elementDirective('rating', {
        params: ['value'],
        twoWay: true,
        paramWatchers: {
            'value': function (val) {
                template(val, this.el, this)
            }
        },
        bind: function () {
            template(0, this.el, this)
        },
        unbind: function () {
            $(this.el).remove();
        }
    })
})(window, jQuery, Zepto, Vue);