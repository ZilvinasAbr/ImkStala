﻿/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/",
    modules: "./node_modules/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("assets", function () {
    gulp.src(paths.modules + 'angular/angular.min.js')
        .pipe(gulp.dest(paths.webroot + 'assets'));
    gulp.src(paths.modules + 'moment/*')
        .pipe(gulp.dest(paths.webroot + 'assets/moment'));
    gulp.src(paths.modules + 'jquery/dist/jquery.min.js')
        .pipe(gulp.dest(paths.webroot + 'assets'));
    gulp.src(paths.modules + 'ng-infinite-scroll/build/ng-infinite-scroll.js')
        .pipe(gulp.dest(paths.webroot + 'assets'));
});


gulp.task("min", ["min:js", "min:css"]);
