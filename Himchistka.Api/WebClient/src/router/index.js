import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Login from '../views/Login.vue'
import Setting from '../views/Settings.vue'
import Orders from '../views/Orders.vue'
import Analytics from '../views/Analytics.vue'
import Clients from '../views/Clients.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/settings',
    name: 'settings',
    component: Setting
  },
  {
    path: '/orders',
    name: 'orders',
    component: Orders
  },
  {
    path: '/analytics',
    name: 'analytics',
    component: Analytics
  },
  {
    path: '/clients',
    name: 'clients',
    component: Clients
  },
  { path: '*', redirect: '/orders' },
]

const router = new VueRouter({
  routes
})

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
})
export default router
