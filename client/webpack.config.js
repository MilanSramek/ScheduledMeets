const dotenv = require('dotenv');
dotenv.config();
dotenv.config({ path: '.env.development' });

const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const webpack = require('webpack');

module.exports = {
  mode: 'development',
  entry: './src/index.tsx',
  output: {
    path: path.resolve(__dirname, 'dist/'),
    filename: '[name].bundle.js',
    clean: true,
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.jsx'],
    fallback: {
      fs: false,
      path: false,
      os: false,
    },
  },
  devtool: 'inline-source-map',
  devServer: {
    server: {
      type: 'https',
      options: {
        key: process.env.SSL_KEY_FILE,
        cert: process.env.SSL_CRT_FILE,
      },
    },
    static: './dist',
    port: process.env.HOST_PORT,
    proxy: {
      [`${process.env.HOST_GRAPHQL_ENDPOINT}`]: {
        target: process.env.SERVER_UNSECURED_URL,
        secure: false,
        changeOrigin: true,
      },
    },
  },
  plugins: [
    new webpack.DefinePlugin({
      'env.HOST_GRAPHQL_ENDPOINT': JSON.stringify(
        process.env.HOST_GRAPHQL_ENDPOINT
      ),
    }),
    new HtmlWebpackPlugin({
      template: path.join(__dirname, './src/index.html'),
    }),
  ],
};
