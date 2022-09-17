require('dotenv').config({ path: '.env.development' });
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

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
    static: './public',
    port: '3000',
    proxy: {
      '/graphql': {
        target: process.env.SERVER_UNSECURED_URL,
        secure: false,
        changeOrigin: true,
      },
    },
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: path.join(__dirname, './public/index.html'),
    }),
  ],
};
