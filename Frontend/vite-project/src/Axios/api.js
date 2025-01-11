import axios from 'axios';
const baseURL = import.meta.env.VITE_API_BASE_URL;

// for sending request with token in headers
const apiWithAuth = axios.create({
    baseURL
});

apiWithAuth.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});


// for sending request without token in headers (login, register, basic datas etc..)
const api = axios.create({
    baseURL
});

export { api, apiWithAuth };