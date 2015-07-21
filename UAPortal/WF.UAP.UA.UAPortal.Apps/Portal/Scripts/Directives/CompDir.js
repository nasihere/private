app.directive("uapHeader", function () {
    return {
        transclude: true,
        scope: {
            formname: "@"
        },
        template: '<header role="heading">' +
            '<div class="jarviswidget-ctrls" role="menu">   <a href="javascript:void(0);" class="button-icon jarviswidget-toggle-btn" rel="tooltip" title="" data-placement="bottom" data-original-title="Collapse"><i class="fa fa-minus "></i></a> <a href="javascript:void(0);" class="button-icon jarviswidget-fullscreen-btn" rel="tooltip" title="" data-placement="bottom" data-original-title="Fullscreen"><i class="fa fa-expand "></i></a> <a href="javascript:void(0);" class="button-icon jarviswidget-delete-btn" rel="tooltip" title="" data-placement="bottom" data-original-title="Delete"><i class="fa fa-times"></i></a></div>' +
            '<span class="widget-icon"> <i class="fa fa-hand-o-up"></i> </span>' +
            '<h2>{{formname}}</h2>' +
            '<span class="jarviswidget-loader"><i class="fa fa-refresh fa-spin"></i></span>' +
            '</header>' +
            '<div role="content">'+

                '<div class="widget-body"  ng-transclude >'+
                    
                  '  </div>'+
                '</div>'+

            '</div>'
};


});


