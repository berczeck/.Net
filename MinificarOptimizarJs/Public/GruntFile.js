module.exports = function (grunt) {
    grunt.initConfig({
        uglify: {
            options: {
                compress: true,
                report: true,
                banner: "/*Minified on <%= grunt.template.date() %>*/\n"
            },
            app: {
                files: {
                    "app.min.js": [
                        "js/init.js",
                        "js/main.js"
                    ],
                    "vendors.min.js": [
                        "js/vendor/underscore.js",
                        "js/vendor/knockout-3.0.js",
                        "js/vendor/modernizr-2.7.1.js"
                    ]
                }
            }
        },
        cssmin: {
            add_banner: {
                options : {
                    banner: "/*Minified on <%= grunt.template.date() %>*/"
                },
                files : {
                    "site.min.css": [
                        "css/normalize.css",
                        "css/site.css",
                        "css/nuevo.css"
                    ]
                }
            }
        }
    });
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.registerTask("default", ["uglify", "cssmin"]);
};