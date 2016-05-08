module.exports = {
    context: __dirname + '/wwwroot/js',
    entry: { 
        restaurant: './restaurant-controller.js',
        map: './loadMap.js'
    },
    output: {
        path: __dirname + '/wwwroot/js',
        filename: '[name].js'
    }
};