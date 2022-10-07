var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var sass = require('gulp-sass')(require('sass'));
var cssmin = require('gulp-cssmin');

var paths = {
    webroot: "./wwwroot/",
    nodeModules: "./node_modules/"
}

paths.jqueryjs = paths.nodeModules + "jquery/dist/jquery.js";
paths.destinationFolder = paths.webroot + "scripts/";
paths.vendorjsFileName = "vendor.min.js";
paths.jsFiles = "./Scripts/*.js";
paths.jsFileName = "script.min.js";
paths.sassFiles = "./Styles/*.scss"
paths.compiledCssFileName = "main.min.css";
paths.destinationCssFolder = paths.webroot + "css/";

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

gulp.task("min:css", function () {
    return gulp.src(paths.sassFiles)
        .pipe(sass().on('error', sass.logError))
        .pipe(concat(paths.compiledCssFileName))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.destinationCssFolder))
});

gulp.task("js-watcher", function () {
    return gulp.watch(paths.jsFiles, gulp.series("min:js"));
});

gulp.task("sass-watcher", function () {
    return gulp.watch(paths.sassFiles, gulp.series("min:css"));
});