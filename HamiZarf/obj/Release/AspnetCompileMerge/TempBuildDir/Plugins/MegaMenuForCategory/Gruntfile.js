'use strict';
module.exports = function(grunt) {

  grunt.initConfig({
    jshint: {
      options: {
        jshintrc: '.jshintrc'
      },
      all: [
        'Gruntfile.js',
        'js/*.js',
        '!js/dynamic_shadow_color.min.js'
      ]
    },
    sass: {
      dist: {
        files: {
          'css/style.css' : 'css/style.scss'
        }
      }
    },
    uglify: {
      dist: {
        files: {
          'js/dynamic_shadow_color.min.js': 'js/dynamic_shadow_color.js'
        }
      }
    },
    watch: {
      css: {
        files: 'css/style.scss',
        tasks: ['sass']
      },
      js: {
        files: 'js/dynamic_shadow_color.js',
        tasks: ['uglify']
      },
      // js: {
      //   files: [
      //     '<%= jshint.all %>'
      //   ],
      //   tasks: ['jshint', 'uglify', 'version']
      // },
      livereload: {
        // Browser live reloading
        // https://github.com/gruntjs/grunt-contrib-watch#live-reloading
        options: {
          livereload: false
        },
        files: [
          'css/style.css',
          'js/dynamic_shadow_color.min.js',
          'app/*.php',
          '*.php'
        ]
      }
    },
    clean: {
      dist: [
        'css/style.css',
        'js/dynamic_shadow_color.min.js'
      ]
    }
  });

  // Load tasks
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-sass');
  grunt.loadNpmTasks('grunt-contrib-watch');

  // Register tasks
  grunt.registerTask('default', [
    'clean',
    'sass',
    'uglify'
  ]);
  grunt.registerTask('dev', [
    'watch'
  ]);

};