import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import axios from 'axios'
import  Bar  from 'vue-chartjs'
import VueMoment from 'vue-moment'
import 'moment/locale/ru'
import 'moment/locale/en-gb'


Vue.config.productionTip = false
axios.defaults.headers.common['Content-Type'] = 'application/json'
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = 'POST, GET, OPTIONS, DELETE, PUT';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';

if(localStorage.getItem('user')){
  axios.defaults.headers.common['Authorization'] = 'Bearer ' + JSON.parse(localStorage.getItem('user')).access_token;
}
Vue.use(VueMoment)

new Vue({
  router,
  store,
  axios,
  vuetify,
  Bar,
  render: h => h(App)
}).$mount('#app')
