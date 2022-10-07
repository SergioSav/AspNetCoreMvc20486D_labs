var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');

var paths = {
    webroot: "./wwwroot/",
    nodeModules: "./node_modules/"
}

paths.jqueryjs = paths.nodeModules + "jquery/dist/jquery.js";
paths.destinationFolder = paths.webroot + "scripts/";
paths.vendorjsFileName = "vendor.min.js";
paths.jsFiles = "./Scripts/*.js";
paths.jsFileName = "script.min.js";

gulp.task("copy-js-file", function() {
    return gulp.src(paths.jqueryjs)
        .pipe(gulp.dest(paths.destinationFolder));
});

gulp.task("min-vendor:js", function () {
    return gulp.src(path.jqueryjs)
        .pipe(concat(paths.vendorjsFileName))
        .pipe(uglify())
        .pipe(gulp.dest(paths.destinationFolder));
});

gulp.task("min:js", function () {
    return gulp.src(paths.jsFiles)
        .pipe(concat(paths.jsFileName))
        .pipe(uglify())
        .pipe(gulp.dest(paths.destinationFolder));
});

gulp.task("js-watcher", function () {
    return gulp.watch(paths.jsFiles, gulp.series("min:js"));
});