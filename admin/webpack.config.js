var path = require('path');

var config = {
  context: __dirname + "/src",
  entry: "./index.js",

  output: {
    filename: "index.js",
    path: path.join(__dirname, '../src/ChaosCMS/admin'),
    publicPath: '/api/resources/ChaosCMS/admin/'
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
        query: {
          presets: ['react', 'es2015']
        }
      },
      {
          test: /\.scss$/,
          loaders: ['style-loader', 'css-loader', 'sass-loader']
      },
      {
          test: /\.(eot|svg|ttf|woff|woff2)$/,
          loader: 'file-loader?name=/fonts/[name].[ext]'
      }
    ],
  },
};
module.exports = config;