/// <binding BeforeBuild='minify' AfterBuild='watch-all' Clean='clean, watch-all' ProjectOpened='watch-all' />
var gulp = require('gulp');
var bower = require('gulp-bower');
var del = require('del');
var minifyCss = require("gulp-minify-css");
var rename = require('gulp-rename');
var uglify = require("gulp-uglify");
var less = require("gulp-less");
var jshint = require("gulp-jshint");

var lib = '/lib';

gulp.task('default', ['bower:install'], function () {
    return;
});

gulp.task('bower:install', ['clean'], function () {
    return bower({
        directory: lib
    });
});

gulp.task('clean', function (done) {
    del(lib, done);
});

gulp.task('minify-css', ['clean'], function () {

    gulp.src('./Content/Site.css')
        .pipe(minifyCss())
        .pipe(rename({ suffix: ".min", extname: ".css" }))
        .pipe(gulp.dest("./Content"));

});

gulp.task('minify-js', ['clean'], function () {

    gulp.src('./Scripts/maps.js')
        .pipe(uglify())
        .pipe(rename({ suffix: ".min", extname: ".js" }))
        .pipe(gulp.dest("./Scripts"));

    gulp.src('./Scripts/bootstrap-386.js')
        .pipe(uglify())
        .pipe(rename({ suffix: ".min", extname: ".js" }))
        .pipe(gulp.dest("./Scripts"));

});

gulp.task('compile-less', ['clean'], function () {

    gulp.src('./Content/Bootstrap.less')
        .pipe(less())
        .pipe(rename({ extname: ".css" }))
        .pipe(gulp.dest("./Content"));

});

gulp.task('minify', ['minify-css', 'minify-js', 'compile-less'], function () {

});

gulp.task('jsLint', function () {
    gulp.src('./Scripts/maps.js')
    .pipe(jshint())
    .pipe(jshint.reporter());
});

gulp.task('watch-js', function () {
    gulp.watch(['./Scripts/*.js'], ['jsLint', 'minify-js']);
});

gulp.task('watch-css', function () {
    gulp.watch(['./Content/*.css'], ['minify-css']);
});

gulp.task('watch-less', function () {
    gulp.watch(['./Content/*.less'], ['compile-less']);
});

gulp.task('watch-all', ['watch-js', 'watch-css', 'watch-less'], function() {
    
})