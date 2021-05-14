const webpack = require('webpack');
const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

const config = {
    entry: './src/index.js',
    output: {
        path: path.resolve(__dirname, '../wwwroot'),
        filename: 'app_scripts.js'
    },
    plugins: [
        new MiniCssExtractPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'sass-loader'
                ]
            },
            {
                test: /\.js$/,
                use: 'babel-loader',
                exclude: /node_modules/
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.(ttf|eot|woff|woff2|svg)$/,
                use: {
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]',
                        outputPath: 'fonts/'
                    },
                }
            }
        ]
    },
    resolve: {
        extensions: [
            '.js',
            '.vue'
        ]
    },
    plugins: [
        new VueLoaderPlugin(),
        new MiniCssExtractPlugin()
    ]
};

module.exports = config;