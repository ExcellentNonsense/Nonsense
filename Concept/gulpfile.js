const gulp = require('gulp');
const postcss = require('gulp-postcss');
const reporter = require('postcss-reporter');

gulp.task('css-lint', function() {
	const stylelint = require('stylelint');
	const stylelintConfig = {
		"extends": "stylelint-config-standard",
		"rules": {
			"at-rule-no-vendor-prefix": true,
			"media-feature-name-no-vendor-prefix": true,
			"property-no-vendor-prefix": true,
			"selector-no-vendor-prefix": true,
			"value-no-vendor-prefix": true,
			"rule-empty-line-before": "never",
			"at-rule-empty-line-before": "never",
			"max-empty-lines": 2
		}
	};
	const processors = [
		stylelint(stylelintConfig),
		reporter({
			clearReportedMessages: true,
			noIcon: true
		})
	];
	return gulp
		.src('src/css-lint/*')
		.pipe(postcss(processors));
});

gulp.task('styles', function() {
	const autoprefixer = require('autoprefixer');
	const cssnano = require('cssnano');
	const processors = [
		autoprefixer(),
		cssnano()
	];
	return gulp
		.src('src/styles/*')
		.pipe(postcss(processors))
		.pipe(gulp.dest('dest/styles'));
});

gulp.task('js-lint', function() {
	const eslint = require('gulp-eslint');
	return gulp
		.src('src/js-lint/*')
		.pipe(eslint())
		.pipe(eslint.format())
		.pipe(eslint.failAfterError());
});

gulp.task('scripts', function(cb) {
	const pump = require('pump');
	const uglify = require('gulp-uglify');
	pump([
			gulp.src('src/scripts/*'),
			uglify(),
			gulp.dest('dest/scripts')
		],
		cb
	);
});

gulp.task('images', function() {
	const imagemin = require('gulp-imagemin');
	return gulp
		.src('src/images/*')
		.pipe(imagemin())
		.pipe(gulp.dest('dest/images'));
});

gulp.task('htmls', function() {
	const htmlmin = require('gulp-htmlmin');
	return gulp
		.src('src/htmls/*')
		.pipe(htmlmin({
			collapseWhitespace: true,
			removeComments: true,
			removeAttributeQuotes: true
		}))
		.pipe(gulp.dest('dest/htmls'));
});