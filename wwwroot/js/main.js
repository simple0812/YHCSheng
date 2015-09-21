define([], function() {
    require.config({
        baseUrl: '/js',
        paths: {
            'angular': 'lib/angular',
            'bootstrap': 'lib/bootstrap',
            'underscore': 'lib/underscore',
            'extension': 'lib/extension',
            'common': 'lib/common',
            'moment': 'lib/moment',
            'validator': 'lib/validator',
            'backbone': 'lib/backbone',
            'pager': 'lib/pager',
            'extension': 'lib/extension',
            'jquery': 'lib/jquery',
            'jquery.ui.widget': 'lib/jquery.ui.widget',
            'jquery.fileupload': 'lib/jquery.fileupload',
            'datetimepicker': 'lib/bootstrap-datetimepicker',
            'md5': 'md5'
        },
        shim: {
            "angular": {
                exports: "angular"
            },
            'datetimepicker': {
                exports: 'datetimepicker',
                deps: ['jquery']
            },
            "angular-route": {
                exports: "angular-route"
            },
            'common': {
                exports: "angular-route",
                deps: ['jquery', 'bootstrap']
            },
            'moment': {
                exports: 'moment'
            },
            'validator': {
                exports: 'validator',
                deps: ['jquery', 'common']
            },
            'bootstrap': ['jquery'],
            'extension': {
                exports: 'extension',
                deps: ['jquery']
            },
            'pager': {
                exports: 'pager'
            },
            'jquery.fileupload': {
                exports: 'jquery.fileupload',
                deps: ['jquery', 'jquery.ui.widget']
            }
        }
    });
});