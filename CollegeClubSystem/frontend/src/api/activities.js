import request from '@/utils/request'

export function getActivities(params) {
  return request({
    url: '/activities',
    method: 'get',
    params
  })
}

export function getActivityById(id) {
  return request({
    url: `/activities/${id}`,
    method: 'get'
  })
}

export function createActivity(data) {
  return request({
    url: '/activities',
    method: 'post',
    data
  })
}

export function updateActivity(id, data) {
  return request({
    url: `/activities/${id}`,
    method: 'put',
    data
  })
}

export function applyActivity(id) {
  return request({
    url: `/activities/${id}/apply`,
    method: 'post'
  })
}

export function signinActivity(id) {
  return request({
    url: `/activities/${id}/signin`,
    method: 'post'
  })
}

export function approveActivity(id) {
  return request({
    url: `/activities/${id}/approve`,
    method: 'post'
  })
}