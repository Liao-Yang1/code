import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('user', () => {
  const user = ref(null)
  const token = ref(localStorage.getItem('token'))

  function login(userInfo, tokenValue) {
    user.value = userInfo
    token.value = tokenValue
    localStorage.setItem('token', tokenValue)
    localStorage.setItem('user', JSON.stringify(userInfo))
  }

  function logout() {
    user.value = null
    token.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('user')
  }

  function initUser() {
    const storedUser = localStorage.getItem('user')
    if (storedUser) {
      user.value = JSON.parse(storedUser)
    }
  }

  return { user, token, login, logout, initUser }
})