import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/users/login',
    method: 'post',
    data
  })
}

export function register(data) {
  return request({
    url: '/users/register',
    method: 'post',
    data
  })
}

export function getProfile() {
  return request({
    url: '/users/profile',
    method: 'get'
  })
}

export function updateProfile(data) {
  return request({
    url: '/users/profile',
    method: 'put',
    data
  })
}

export function changePassword(data) {
  return request({
    url: '/users/changepwd',
    method: 'put',
    data
  })
}

export function getUsers(params) {
  return request({
    url: '/users',
    method: 'get',
    params
  })
}

export function updateUserStatus(id, status) {
  return request({
    url: `/users/${id}/status`,
    method: 'put',
    data: { status }
  })
}