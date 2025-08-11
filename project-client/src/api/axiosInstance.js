import axios from 'axios';
import { store } from '../redux/store';
import { resetAuthState } from '../redux/slices/authSlice';

const axiosInstance = axios.create({
    baseURL: 'https://localhost:5002/api',
    withCredentials: true,
});

axiosInstance.interceptors.request.use(
    (config) => {
        config.headers['Content-Type'] = 'application/json';
        return config;
    },
    (error) => {clear
        return Promise.reject(error);
    }
);

axiosInstance.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response && error.response.status === 401) {
            const currentPath = window.location.pathname;
            if (currentPath !== '/login') {
                store.dispatch(resetAuthState());
                window.location.href = '/login';
            }
        }
        return Promise.reject(error);
    }
);

export default axiosInstance;
