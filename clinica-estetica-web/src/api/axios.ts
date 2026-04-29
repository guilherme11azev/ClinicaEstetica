import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7000/api', // ajuste a porta da sua API
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor de requisição — adiciona o token JWT automaticamente
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor de resposta — redireciona para login se 401
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('usuario');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;