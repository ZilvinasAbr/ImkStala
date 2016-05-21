module.exports = {
    context: __dirname + '/wwwroot/js',
    entry: { 
        restaurant: './restaurant-controller.js',
        visitor: './visitor-controller.js'
    },
    output: {
        path: __dirname + '/wwwroot/js',
        filename: '[name].js'
    }
};