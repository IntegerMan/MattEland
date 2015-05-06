/// <binding BeforeBuild='minify' Clean='clean' />
var gulp = require('gulp');
var bower = require('gulp-bower');
var del = require('del');
var minifyCss = require("gulp-minify-css");
var rename = require('gulp-rename');
var uglify = require("gulp-uglify");

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

gulp.task('minify', ['minify-css', 'minify-js'], function () {

});