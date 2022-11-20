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
    alias: {
      src: path.resolve(__dirname, 'src'),
      components: path.resolve(__dirname, 'src/components/'),
      parts: path.resolve(__dirname, 'src/parts/'),
      store: path.resolve(__dirname, 'src/store/'),
      config: path.resolve(__dirname, 'src/config/'),
      gql: path.resolve(__dirname, 'src/gql/'),
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
    historyApiFallback: {
      index: 'index.html',
    },
    static: './dist',
    port: process.env.HOST_PORT,
    proxy: {
      [`${process.env.HOST_GRAPHQL_ENDPOINT}`]: {
        target: process.env.SERVER_SECURED_URL,
        secure: false, // Certificate is not checked.
        changeOrigin: true,
      },
    },
  },
  plugins: [
    new webpack.DefinePlugin({
      'env.HOST_GRAPHQL_ENDPOINT': JSON.stringify(
        process.env.HOST_GRAPHQL_ENDPOINT
      ),
      'env.GOOGLE_GSI_CLIENT_URL': JSON.stringify(
        process.env.GOOGLE_GSI_CLIENT_URL
      ),
      'env.GOOGLE_CLIENT_ID': JSON.stringify(process.env.GOOGLE_CLIENT_ID),
    }),
    new HtmlWebpackPlugin({
      template: path.join(__dirname, './src/index.html'),
      favicon: path.join(__dirname, './src/favicon.ico'),
    }),
  ],
};
