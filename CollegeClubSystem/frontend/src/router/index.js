import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  { path: '/', name: 'Home', component: () => import('@/views/Home.vue') },
  { path: '/login', name: 'Login', component: () => import('@/views/Login.vue') },
  { path: '/register', name: 'Register', component: () => import('@/views/Register.vue') },
  { path: '/clubs', name: 'ClubList', component: () => import('@/views/ClubList.vue') },
  { path: '/clubs/:id', name: 'ClubDetail', component: () => import('@/views/ClubDetail.vue') },
  { path: '/activities', name: 'ActivityList', component: () => import('@/views/ActivityList.vue') },
  { path: '/activities/:id', name: 'ActivityDetail', component: () => import('@/views/ActivityDetail.vue') },
  { path: '/profile', name: 'Profile', component: () => import('@/views/Profile.vue') },
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('@/views/Admin/AdminHome.vue'),
    children: [
      { path: 'users', name: 'AdminUsers', component: () => import('@/views/Admin/UserManagement.vue') },
      { path: 'clubs', name: 'AdminClubs', component: () => import('@/views/Admin/ClubManagement.vue') },
      { path: 'activities', name: 'AdminActivities', component: () => import('@/views/Admin/ActivityManagement.vue') },
      { path: 'finance', name: 'AdminFinance', component: () => import('@/views/Admin/FinanceManagement.vue') }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  if (to.path.startsWith('/admin') && !token) {
    next('/login')
  } else if (to.path === '/login' && token) {
    next('/')
  } else {
    next()
  }
})

export default router