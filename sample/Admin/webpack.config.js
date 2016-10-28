"use strict";
var path = require('path');

module.exports = {
    entry: path.join(__dirname, "src/index.js"),
    output: {
        path: path.join(__dirname, "../SampleSite/admin"),
        publicPath: "",
        filename: "index.js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: "babel-loader",
                query: {
                    presets: ['react', 'es2015', 'stage-0'],
                    //plugins: [
                    //  ["transform-decorators-legacy"]
                    //]
                }
            }
        ]
    }
};