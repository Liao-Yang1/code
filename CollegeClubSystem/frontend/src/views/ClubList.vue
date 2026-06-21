<template>
  <div class="club-list-page">
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
      <div class="search-bar">
        <input 
          v-model="searchKeyword" 
          type="text" 
          placeholder="搜索社团名称..."
          @keyup.enter="loadClubs"
        />
        <select v-model="category" @change="loadClubs">
          <option value="">全部分类</option>
          <option value="学术科技">学术科技</option>
          <option value="文化艺术">文化艺术</option>
          <option value="体育竞技">体育竞技</option>
          <option value="公益服务">公益服务</option>
          <option value="创新创业">创新创业</option>
        </select>
        <button @click="loadClubs" class="btn-search">搜索</button>
      </div>

      <div class="club-grid">
        <div 
          v-for="club in clubs" 
          :key="club.clubId" 
          class="club-card"
          @click="goToDetail(club.clubId)"
        >
          <div class="club-logo">
            <span class="logo-icon">🏫</span>
          </div>
          <div class="club-info">
            <h3>{{ club.name }}</h3>
            <p class="category">{{ club.category }}</p>
            <p class="description">{{ club.description }}</p>
            <div class="stats">
              <span>{{ club.memberCount }} 成员</span>
              <span>{{ club.activityCount }} 活动</span>
            </div>
          </div>
        </div>
      </div>

      <div class="pagination" v-if="totalPages > 1">
        <button 
          @click="prevPage" 
          :disabled="currentPage === 1"
          class="btn-page"
        >上一页</button>
        <span class="page-info">第 {{ currentPage }} / {{ totalPages }} 页</span>
        <button 
          @click="nextPage" 
          :disabled="currentPage === totalPages"
          class="btn-page"
        >下一页</button>
      </div>
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
import { getClubs } from '@/api/clubs'

const router = useRouter()
const userStore = useUserStore()

const user = ref(null)
const clubs = ref([])
const searchKeyword = ref('')
const category = ref('')
const currentPage = ref(1)
const totalPages = ref(1)

onMounted(() => {
  user.value = userStore.user
  loadClubs()
})

async function loadClubs() {
  try {
    const response = await getClubs({
      keyword: searchKeyword.value,
      category: category.value,
      page: currentPage.value,
      pageSize: 12
    })
    
    if (response.success) {
      clubs.value = response.data.list
      totalPages.value = response.data.totalPages
    }
  } catch (error) {
    console.error('加载社团列表失败:', error)
  }
}

function goToDetail(id) {
  router.push(`/clubs/${id}`)
}

function prevPage() {
  if (currentPage.value > 1) {
    currentPage.value--
    loadClubs()
  }
}

function nextPage() {
  if (currentPage.value < totalPages.value) {
    currentPage.value++
    loadClubs()
  }
}
</script>

<style scoped>
.club-list-page {
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
}

.main {
  flex: 1;
  padding: 2rem 5%;
}

.search-bar {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
  max-width: 600px;
}

.search-bar input {
  flex: 1;
  padding: 0.8rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
}

.search-bar select {
  padding: 0.8rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
}

.btn-search {
  padding: 0.8rem 1.5rem;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

.club-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 1.5rem;
}

.club-card {
  background: white;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
  overflow: hidden;
  cursor: pointer;
  transition: transform 0.3s;
}

.club-card:hover {
  transform: translateY(-5px);
}

.club-logo {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 2rem;
  text-align: center;
}

.logo-icon {
  font-size: 3rem;
}

.club-info {
  padding: 1.5rem;
}

.club-info h3 {
  margin-bottom: 0.5rem;
  color: #333;
}

.category {
  color: #667eea;
  font-size: 0.9rem;
  margin-bottom: 0.5rem;
}

.description {
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 1rem;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.stats {
  display: flex;
  gap: 1.5rem;
  color: #999;
  font-size: 0.85rem;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-top: 2rem;
}

.btn-page {
  padding: 0.5rem 1rem;
  border: 1px solid #ddd;
  border-radius: 5px;
  background: white;
  cursor: pointer;
}

.btn-page:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-info {
  color: #666;
}

.footer {
  text-align: center;
  padding: 1.5rem;
  background: #333;
  color: white;
}
</style>