import Vue from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import CircularCountDownTimer from 'vue-circular-count-down-timer';
import Axios from 'axios';
import { BootstrapVue } from 'bootstrap-vue'


Axios.defaults.baseURL = 'http://localhost:5000/api';
Vue.use(CircularCountDownTimer);
Vue.use(BootstrapVue);

new Vue({
  render: h => h(App),
}).$mount('#app')
