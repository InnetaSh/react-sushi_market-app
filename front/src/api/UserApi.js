import axios from 'axios';


const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000/api';

class UserApi {

  async login(credentials) {
        const response = await axios.post(`${API_URL}/auth/login`, credentials);
        return response.data;
    }

    async register(userData) {
        const response = await axios.post(`${API_URL}/auth/register`, userData);
        return response.data;
    }

    async googleLogin(tokenData) {
        const response = await axios.post(`${API_URL}/auth/google`, tokenData);
        return response.data;
    }
}

export default new UserApi();