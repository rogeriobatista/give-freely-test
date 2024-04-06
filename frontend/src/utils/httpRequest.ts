import axios from 'axios';
import { API_URL } from './apiURL';

const api = axios.create({
  baseURL: API_URL, // Substitua pela URL da sua API
});

// Interceptador para adicionar o token de atualização a todas as solicitações
api.interceptors.request.use(async (config) => {
  const accessToken = localStorage.getItem('accessToken'); // Recupere o token de acesso (ou de onde quer que esteja armazenado)
  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`;
  }
  return config;
});

// Interceptador para lidar com erros de resposta, incluindo a renovação do token de acesso com o token de atualização
api.interceptors.response.use(
  (response) => response,
  async (error) => {    
    return Promise.reject(error);
  }
);

export default api;
