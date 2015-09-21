

define([
	'angular', 
	'datetimepicker',
	'jquery.fileupload'
], function() {
	var moduleDirect = angular.module('moduleDirect', []);

	moduleDirect.directive('datetimepicker',
		function() {
			return {
				priority: 0,
				template: '',
				replace: false,
				transclude: false,
				restrict: 'A',
				scope: false,
				link: function postLink(scope, iElement, iAttrs, ctrl) {
					$(iElement).val(query_list.month).datetimepicker({
						format: "yyyy-mm",
						autoclose: true,
						language: 'zh-CN',
						startView: "year",
						minView: "year",
						minuteStep: 1,
						endDate: new Date()
					});
				}
			};
		});

	moduleDirect.directive('fileupload',
		function() {
			return {
				priority: 0,
				template: '',
				replace: false,
				transclude: false,
				restrict: 'A',
				scope: false,
				link: function postLink(scope, iElement, iAttrs, ctrl) {
					var url = '/api/user/portrait';
		            $('#uploadInput').fileupload({
		                url: url,
		                dataType: 'json',
		                add: function (e, data) {
		                    $.each(data.files, function (index, file) {
		                    	console.log(file);
		                    });
		                    data.submit();
		                },
		                done: function (e, data) {
		                    if(!data.result) return alert('未知的错误');
		                    if(data.result.state == 'fail') {
		                       console.log('fail')
		                    }
		                    else {
		                        $('#logoImg').attr('src',data.result.result).show()
		                    }
		                },
		                progressall: function (e, data) {
		                }
		            }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
				}
			};
		});


	
})