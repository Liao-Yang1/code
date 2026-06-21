import request from '@/utils/request'

export function getFinanceRecords(params) {
  return request({
    url: '/finance',
    method: 'get',
    params
  })
}

export function applyFunds(data) {
  return request({
    url: '/finance/apply',
    method: 'post',
    data
  })
}

export function reimburse(data) {
  return request({
    url: '/finance/reimburse',
    method: 'post',
    data
  })
}

export function approveFinance(id) {
  return request({
    url: `/finance/${id}/approve`,
    method: 'post'
  })
}

export function getFinanceReport(params) {
  return request({
    url: '/finance/report',
    method: 'get',
    params
  })
}