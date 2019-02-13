"use strict";

const gulp = require("gulp"),
  concat = require("gulp-concat");

const paths = {
  webroot: "./wwwroot/"
};

paths.mainCss = paths.webroot + "css/main/**/*.css";
paths.processedMainCss = paths.webroot + "css/site.css";

paths.areasCss = paths.webroot + "css/areas/*.css";
paths.processedAreasCss = paths.webroot + "css/";
paths.processedAdminCss = paths.webroot + "css/admin.css";

paths.js = paths.webroot + "js/**/*.js";
paths.processedJs = paths.webroot + "js/site.js";

gulp.task("clean", () => {
  const del = require("del");

  return del([
    paths.processedMainCss,
    paths.processedAdminCss,
    paths.processedJs
  ]);
});

gulp.task("process:mainCss", () => {
  const postcss = require("gulp-postcss");
  const autoprefixer = require("autoprefixer");
  const cssnano = require("cssnano");

  const processors = [
    autoprefixer(),
    cssnano()
  ];

  return gulp.src(paths.mainCss)
    .pipe(concat(paths.processedMainCss))
    .pipe(postcss(processors))
    .pipe(gulp.dest("."));
});

gulp.task("process:areasCss", () => {
  const postcss = require("gulp-postcss");
  const autoprefixer = require("autoprefixer");
  const cssnano = require("cssnano");

  const processors = [
    autoprefixer(),
    cssnano()
  ];

  return gulp.src(paths.areasCss)
    .pipe(postcss(processors))
    .pipe(gulp.dest(paths.processedAreasCss));
});

gulp.task("process:js", () => {
  const terser = require('gulp-terser');

  return gulp.src([paths.js, "!" + paths.processedJs])
    .pipe(concat(paths.processedJs))
    .pipe(terser())
    .pipe(gulp.dest("."));
});

gulp.task("build", gulp.series(["clean", "process:mainCss", "process:areasCss", "process:js"]));