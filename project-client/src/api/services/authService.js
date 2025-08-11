// src/services/authService.js
import axiosInstance from '../axiosInstance';

const login = async (loginDto) => {
    const response = await axiosInstance.post('auth/login', loginDto);
    return response.data;
};

const logout = async () => {
    const response = await axiosInstance.post('auth/logout');
    return response.data;
};

const sendResetEmail = async (email) => {
    const response = await axiosInstance.post('auth/send-reset-email', { email });
    return response.data;
};

export default {
    login,
    logout,
    sendResetEmail,
};