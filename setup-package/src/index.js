import Vue from 'vue';
import App from './views/app.vue';

require("./scss/index.scss");

new Vue({
    el: "#app",
    render: h => h(App)
});