"use strict";

const gulp = require("gulp"),
  concat = require("gulp-concat");

const paths = {
  webroot: "./wwwroot/"
};

paths.css = paths.webroot + "css/**/*.css";
paths.processedCssDest = paths.webroot + "css/site.css";

paths.js = paths.webroot + "js/**/*.js";
paths.processedJsDest = paths.webroot + "js/site.js";

gulp.task("clean", () => {
  const del = require("del");

  return del([
    paths.processedCssDest,
    paths.processedJsDest
  ]);
});

gulp.task("process:css", () => {
  const postcss = require("gulp-postcss");
  const autoprefixer = require("autoprefixer");
  const cssnano = require("cssnano");

  const processors = [
    autoprefixer(),
    cssnano()
  ];

  return gulp.src([paths.css, "!" + paths.processedCssDest])
    .pipe(concat(paths.processedCssDest))
    .pipe(postcss(processors))
    .pipe(gulp.dest("."));
});

gulp.task("process:js", () => {
  const terser = require('gulp-terser');

  return gulp.src([paths.js, "!" + paths.processedJsDest])
    .pipe(concat(paths.processedJsDest))
    .pipe(terser())
    .pipe(gulp.dest("."));
});

gulp.task("build", gulp.series(["clean", "process:css", "process:js"]));