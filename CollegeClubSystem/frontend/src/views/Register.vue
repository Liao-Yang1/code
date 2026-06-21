<template>
  <div class="register-page">
    <div class="register-container">
      <div class="register-header">
        <h1>注册</h1>
        <p>创建您的社团管理系统账号</p>
      </div>
      
      <form @submit.prevent="handleRegister" class="register-form">
        <div class="form-group">
          <label>用户名</label>
          <input 
            v-model="form.username" 
            type="text" 
            placeholder="请输入用户名"
            required
          />
        </div>
        
        <div class="form-group">
          <label>真实姓名</label>
          <input 
            v-model="form.realName" 
            type="text" 
            placeholder="请输入真实姓名"
            required
          />
        </div>
        
        <div class="form-group">
          <label>邮箱</label>
          <input 
            v-model="form.email" 
            type="email" 
            placeholder="请输入邮箱"
            required
          />
        </div>
        
        <div class="form-group">
          <label>密码</label>
          <input 
            v-model="form.password" 
            type="password" 
            placeholder="请输入密码"
            required
          />
        </div>
        
        <div class="form-group">
          <label>确认密码</label>
          <input 
            v-model="form.confirmPassword" 
            type="password" 
            placeholder="请再次输入密码"
            required
          />
        </div>
        
        <button type="submit" class="btn-register">注册</button>
      </form>
      
      <div class="register-footer">
        <p>已有账号？<router-link to="/login">立即登录</router-link></p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { register } from '@/api/users'
import { ElMessage } from 'element-plus'

const router = useRouter()

const form = ref({
  username: '',
  realName: '',
  email: '',
  password: '',
  confirmPassword: ''
})

async function handleRegister() {
  if (form.value.password !== form.value.confirmPassword) {
    ElMessage.error('两次输入的密码不一致')
    return
  }
  
  try {
    const response = await register({
      username: form.value.username,
      realName: form.value.realName,
      email: form.value.email,
      password: form.value.password
    })
    
    if (response.success) {
      ElMessage.success('注册成功，请登录')
      router.push('/login')
    } else {
      ElMessage.error(response.message || '注册失败')
    }
  } catch (error) {
    ElMessage.error('注册失败，请检查网络连接')
  }
}
</script>

<style scoped>
.register-page {
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.register-container {
  background: white;
  padding: 3rem;
  border-radius: 15px;
  box-shadow: 0 10px 40px rgba(0,0,0,0.1);
  width: 450px;
}

.register-header {
  text-align: center;
  margin-bottom: 2rem;
}

.register-header h1 {
  font-size: 1.8rem;
  color: #333;
  margin-bottom: 0.5rem;
}

.register-header p {
  color: #666;
}

.register-form {
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.form-group label {
  font-weight: 500;
  color: #333;
}

.form-group input {
  padding: 0.8rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus {
  outline: none;
  border-color: #667eea;
}

.btn-register {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  padding: 1rem;
  border-radius: 8px;
  font-size: 1.1rem;
  cursor: pointer;
  transition: transform 0.3s;
  margin-top: 0.5rem;
}

.btn-register:hover {
  transform: translateY(-2px);
}

.register-footer {
  text-align: center;
  margin-top: 1.5rem;
}

.register-footer a {
  color: #667eea;
  text-decoration: none;
}

.register-footer a:hover {
  text-decoration: underline;
}
</style>