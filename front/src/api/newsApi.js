import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true 
});

class NewsApi {
    async getNews() {
        try {
            const response = await api.get('/News');
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при завантаженні новин');
        }
    }
}

export default new NewsApi();