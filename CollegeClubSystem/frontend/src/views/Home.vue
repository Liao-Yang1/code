<template>
  <div class="home-page">
    <header class="header">
      <div class="logo">高校社团管理系统</div>
      <nav class="nav">
        <router-link to="/">首页</router-link>
        <router-link to="/clubs">社团列表</router-link>
        <router-link to="/activities">活动列表</router-link>
        <router-link v-if="!user" to="/login">登录</router-link>
        <router-link v-else to="/profile">个人中心</router-link>
      </nav>
    </header>

    <main class="main">
      <section class="hero">
        <h1>欢迎来到高校社团管理系统</h1>
        <p>发现精彩社团，参与丰富活动，开启你的校园精彩生活</p>
        <button @click="goToClubs" class="btn-primary">探索社团</button>
      </section>

      <section class="features">
        <h2>系统特色</h2>
        <div class="feature-grid">
          <div class="feature-card">
            <div class="icon">🏫</div>
            <h3>社团管理</h3>
            <p>便捷的社团创建、审批和管理功能</p>
          </div>
          <div class="feature-card">
            <div class="icon">🎉</div>
            <h3>活动管理</h3>
            <p>活动发布、报名、签到一站式服务</p>
          </div>
          <div class="feature-card">
            <div class="icon">💰</div>
            <h3>财务管理</h3>
            <p>透明的经费申请和报销流程</p>
          </div>
          <div class="feature-card">
            <div class="icon">👥</div>
            <h3>成员管理</h3>
            <p>完善的成员管理和权限控制</p>
          </div>
        </div>
      </section>

      <section class="stats">
        <h2>平台数据</h2>
        <div class="stats-grid">
          <div class="stat-item">
            <div class="stat-number">{{ stats.clubs }}</div>
            <div class="stat-label">社团数量</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ stats.activities }}</div>
            <div class="stat-label">活动数量</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ stats.members }}</div>
            <div class="stat-label">成员数量</div>
          </div>
        </div>
      </section>
    </main>

    <footer class="footer">
      <p>&copy; 2026 高校社团管理系统 - 版权所有</p>
    </footer>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/store/user'

const router = useRouter()
const userStore = useUserStore()
const user = ref(null)

const stats = ref({
  clubs: 0,
  activities: 0,
  members: 0
})

onMounted(() => {
  user.value = userStore.user
  loadStats()
})

function loadStats() {
  stats.value = {
    clubs: 128,
    activities: 526,
    members: 8923
  }
}

function goToClubs() {
  router.push('/clubs')
}
</script>

<style scoped>
.home-page {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 1rem 5%;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
}

.logo {
  font-size: 1.5rem;
  font-weight: bold;
}

.nav {
  display: flex;
  gap: 2rem;
}

.nav a {
  color: white;
  text-decoration: none;
  transition: opacity 0.3s;
}

.nav a:hover {
  opacity: 0.8;
}

.main {
  flex: 1;
  padding: 2rem 5%;
}

.hero {
  text-align: center;
  padding: 4rem 0;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
  border-radius: 10px;
  margin-bottom: 2rem;
}

.hero h1 {
  font-size: 2.5rem;
  margin-bottom: 1rem;
  color: #333;
}

.hero p {
  font-size: 1.2rem;
  color: #666;
  margin-bottom: 2rem;
}

.btn-primary {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  padding: 1rem 2rem;
  font-size: 1.1rem;
  border-radius: 50px;
  cursor: pointer;
  transition: transform 0.3s;
}

.btn-primary:hover {
  transform: translateY(-2px);
}

.features {
  margin-bottom: 2rem;
}

.features h2 {
  text-align: center;
  font-size: 1.8rem;
  margin-bottom: 2rem;
  color: #333;
}

.feature-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 1.5rem;
}

.feature-card {
  background: white;
  padding: 2rem;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
  text-align: center;
  transition: transform 0.3s;
}

.feature-card:hover {
  transform: translateY(-5px);
}

.feature-card .icon {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.feature-card h3 {
  margin-bottom: 0.5rem;
  color: #333;
}

.feature-card p {
  color: #666;
  font-size: 0.9rem;
}

.stats {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 3rem;
  border-radius: 10px;
}

.stats h2 {
  text-align: center;
  margin-bottom: 2rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 2rem;
}

.stat-item {
  text-align: center;
}

.stat-number {
  font-size: 3rem;
  font-weight: bold;
}

.stat-label {
  font-size: 1rem;
  opacity: 0.9;
}

.footer {
  text-align: center;
  padding: 1.5rem;
  background: #333;
  color: white;
}
</style>