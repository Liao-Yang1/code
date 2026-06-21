<template>
  <el-container class="sidebar-container">
    <el-aside width="200px" class="sidebar">
      <div class="logo">
        <h2>社团管理系统</h2>
      </div>
      <el-menu :default-active="activeMenu" class="sidebar-menu">
        <el-menu-item index="/">
          <i class="el-icon-s-home"></i>
          <span>首页</span>
        </el-menu-item>
        <el-menu-item index="/clubs">
          <i class="el-icon-user-solid"></i>
          <span>社团列表</span>
        </el-menu-item>
        <el-menu-item index="/activities">
          <i class="el-icon-calendar"></i>
          <span>活动列表</span>
        </el-menu-item>
        <el-menu-item index="/profile">
          <i class="el-icon-user"></i>
          <span>个人中心</span>
        </el-menu-item>
        <el-submenu index="/admin">
          <template slot="title">
            <i class="el-icon-setting"></i>
            <span>管理中心</span>
          </template>
          <el-menu-item index="/admin/users">用户管理</el-menu-item>
          <el-menu-item index="/admin/clubs">社团管理</el-menu-item>
          <el-menu-item index="/admin/activities">活动管理</el-menu-item>
          <el-menu-item index="/admin/finance">财务管理</el-menu-item>
        </el-submenu>
      </el-menu>
    </el-aside>
    <el-main class="main-content">
      <router-view />
    </el-main>
  </el-container>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const activeMenu = computed(() => route.path)
</script>

<style scoped>
.sidebar-container {
  height: 100vh;
}

.sidebar {
  background-color: #2a3f5f;
  color: white;
}

.logo {
  padding: 20px;
  text-align: center;
  border-bottom: 1px solid #3d5270;
}

.logo h2 {
  margin: 0;
  font-size: 16px;
  font-weight: normal;
}

.sidebar-menu {
  border-right: none;
}

.el-menu-item, .el-submenu__title {
  color: #b8c7ce;
}

.el-menu-item:hover, .el-submenu__title:hover {
  background-color: #1f2d4a;
}

.el-menu-item.is-active {
  background-color: #1f2d4a;
  color: white;
}

.main-content {
  padding: 20px;
