module.exports = {
    context: __dirname + '/wwwroot/js',
    entry: './restaurant-controller.js',
    output: {
        path: __dirname + '/wwwroot/js',
        filename: 'bundle.js'
    }
};