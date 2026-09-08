import api from './api';

class NewsApi {
    async getNews() {
        try {
            const response = await api.get('/News');

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при завантаженні новин'
            );
        }
    }
}

export default new NewsApi();