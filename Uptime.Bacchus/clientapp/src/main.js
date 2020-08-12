import Vue from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import CircularCountDownTimer from 'vue-circular-count-down-timer';
import { BootstrapVue } from 'bootstrap-vue'

Vue.use(CircularCountDownTimer);
Vue.use(BootstrapVue);

new Vue({
  render: h => h(App),
}).$mount('#app')
