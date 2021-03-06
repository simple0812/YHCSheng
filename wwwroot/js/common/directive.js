﻿//controller最先运行,通过$scope.$parent与页面controller交互
//compile阶段进行标签解析和变换，link阶段进行数据绑定等操作
//当complie存在的时候，link会被屏蔽
//通过require属性可以指定关联的controller

//如果scope是bool值的时候 指令的$scope指向controller的$scope
//如果scope是对象的时候 指令的$scope.$parent指向controller的$scope

//$emit向父级发送事件 $broadcast向子级发送事件

//@:引用 =:双向绑定 &:以wrapper function形式引用

define([
    '',
	'angular',
	'common',
	'datetimepicker',
	'jquery.fileupload'
], function () {
    var commonDirect = angular.module('commonDirect', []);

    commonDirect.directive('datetimepicker',
		function () {
		    return {
		        priority: 0,
		        template: '',
		        replace: false,
		        transclude: false,
		        restrict: 'A',
		        scope: false,
		        link: function postLink(scope, iElement, iAttrs, ctrl) {
		            $(iElement).val('').datetimepicker({
		                format: 'yyyy-mm',
		                autoclose: true,
		                language: 'zh-CN',
		                startView: 'year',
		                minView: 'year',
		                minuteStep: 1,
		                endDate: new Date()
		            });
		        }
		    };
		});

    commonDirect.directive('pager', ['svc', function (svc) {
        return {
            priority: 0,
            template: '',
            replace: false,
            transclude: false,
            restrict: 'A',
            scope: {},
            controller: function ($scope, $element, $attrs) {
                $scope.showList = function () {
                    $scope.pager.moveIndicator(arguments[0]);
                    svc.retrieve($scope.pager.condition)
                        .done(function (json) {
                            $scope.$parent.models = json.result.entities || [];
                            $('.userList').show();
                            $scope.pager.setRecordCount(json.result.total);
                            $scope.pager.renderNumberStyleHtml($($element).get(0));
                        }).fail(function () {
                            console.log('数据获取失败');
                        });
                }

                $scope.pager = new Pager($scope.$parent.pageCondition.pageSize, 0, 1, $scope.$parent.pageCondition, $scope.showList, -1);
                $scope.$on('pager', function () {
                    setTimeout(function () {
                        $scope.showList({ mode: 'nums', val: 1 });
                    });
                });
            },

            link: function postLink($scope, iElement, iAttrs, ctrl) {
                $scope.pager.renderNumberStyleHtml($(iElement).get(0));
                $scope.showList({ mode: 'nums', val: 1 });
            }
        };
    }]);

    commonDirect.directive('chkItem',
		function () {
		    return {
		        priority: 0,
		        template: '',
		        replace: false,
		        transclude: false,
		        restrict: 'A',
		        scope: {},
		        controller: function ($scope, $element, $attrs) {
		            $scope.$parent.$watch('model.selStatus', function (newValue, oldValue, scope) {
		                if (newValue === oldValue) return;
		                $scope.$emit('selectItem', {
		                    id: $scope.$parent.model.id,
		                    val: newValue
		                });
		            });
		        }
		    };
		});

    commonDirect.directive('fileupload',
		function () {
		    return {
		        priority: 0,
		        template: '',
		        replace: false,
		        transclude: false,
		        restrict: 'A',
		        scope: false,
		        link: function postLink(scope, iElement, iAttrs, ctrl) {
		            var url = '/api/user/portrait';
		            $(iElement).fileupload({
		                url: url,
		                dataType: 'json',
		                add: function (e, data) {
		                    data.submit();
		                },
		                done: function (e, data) {
		                    if (!data.result) return console.log('未知的错误');
		                    if (data.result.state === 'fail') return console.log('fail');

		                    console.log(data.result.result, scope);
		                    scope.$apply(function () {
		                        scope.model.portrait = data.result.result;
		                    });
		                },
		                progressall: function (e, data) { }
		            }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
		        }
		    };
		});

    //modal-dialog
    commonDirect.directive('modalDialog',
		function () {
		    return {
		        priority: 0,
		        template: '',
		        replace: false,
		        transclude: false,
		        restrict: 'A',
		        scope: {},
		        controller: function ($scope, $element, $attrs) {
		            $scope.$on('postSave', function () {
		                setTimeout(function () {
		                    $($element).modal('hide');
		                });
		            });
		        },
		        link: function postLink(scope, iElement, iAttrs, ctrl) {
		            $(iElement).on('hidden.bs.modal', function (e) {
		                scope.$parent.model = {};
		                scope.$apply();
		            });
		        }
		    };
		});

    commonDirect.directive('showModal',
		function () {
		    return {
		        priority: 0,
		        template: '',
		        replace: false,
		        transclude: false,
		        restrict: 'A',
		        scope: {
		            saveType: '@',
		            targetModal: '@'
		        },
		        controller: function ($scope, $element, $attrs) {
		            //console.log('showmodal ->',$scope)
		        },
		        link: function postLink(scope, iElement, iAttrs, ctrl) {
		            $(iElement).on('click', function () {
		                scope.$apply(function () {
		                    var model = scope.saveType == 'update' ? scope.$parent.model : {};
		                    scope.$emit('preSave', {
		                        model: model,
		                        saveType: scope.saveType
		                    });
		                    $(scope.targetModal).modal('show');
		                });
		            });
		        }
		    };
		});

})