import request from '@/utils/request'

export function getClubs(params) {
  return request({
    url: '/clubs',
    method: 'get',
    params
  })
}

export function getClubById(id) {
  return request({
    url: `/clubs/${id}`,
    method: 'get'
  })
}

export function createClub(data) {
  return request({
    url: '/clubs',
    method: 'post',
    data
  })
}

export function updateClub(id, data) {
  return request({
    url: `/clubs/${id}`,
    method: 'put',
    data
  })
}

export function deleteClub(id) {
  return request({
    url: `/clubs/${id}`,
    method: 'delete'
  })
}

export function approveClub(id) {
  return request({
    url: `/clubs/${id}/approve`,
    method: 'post'
  })
}

export function getClubMembers(id) {
  return request({
    url: `/clubs/${id}/members`,
    method: 'get'
  })
}

export function joinClub(id) {
  return request({
    url: `/clubs/${id}/join`,
    method: 'post'
  })
}