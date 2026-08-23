import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true
});

class UserApi {
    async login(credentials) {
        const response = await api.post('/auth/login', credentials);
        return response.data; 
    }

    async register(userData) {
        const response = await api.post('/auth/register', userData);
        return response.data; 
    }

    async logout() {
        const response = await api.post('/auth/logout');
        return response.data;
    }

    async getUserInfo() {
        const response = await api.get('/auth/user-info');
        return response.data;
    }

    async googleLogin(tokenData) {
        const response = await api.post('/auth/google', tokenData);
        return response.data;
    }
}

export default new UserApi();