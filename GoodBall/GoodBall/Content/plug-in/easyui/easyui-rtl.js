(function($){
	$.fn.slider.defaults.reversed = true;
	
	var datagrid_freezeRow = $.fn.datagrid.methods.freezeRow;
	$.fn.datagrid.methods.freezeRow = function(jq, index){
		return jq.each(function(){
			datagrid_freezeRow.call(this, jq, index);
			var state = $.data(this, 'datagrid');
			if (!state.rtlscroll){
				state.rtlscroll = true;
				var dc = state.dc;
				dc.body2.bind('scroll', function(){
					var ftable = $(this).children('table.datagrid-btable-frozen');
					ftable.css('left',  $(this)._outerWidth() + $(this)._scrollLeft() - ftable._outerWidth());
				});
			}
		});
	}
})(jQuery);
