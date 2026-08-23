import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true 
});

class CategoryApi {
    async getCategories() {
        try {
            const response = await api.get('/Categories');
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при завантаженні категорій');
        }
    }

    async getCategoryById(id) {
        try {
            const response = await api.get(`/Categories/${id}`);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при отриманні категорії');
        }
    }

    async createCategory(categoryData) {
        try {
            const response = await api.post('/Categories', categoryData);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при створенні категорії');
        }
    }

    async updateCategory(id, categoryData) {
        try {
            const response = await api.put(`/Categories/${id}`, categoryData);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при оновленні категорії');
        }
    }

    async deleteCategory(id) {
        try {
            const response = await api.delete(`/Categories/${id}`);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при видаленні категорії');
        }
    }

    async reorderCategories(orderedCategories) {
        try {
            const response = await api.post('/Categories/reorder', orderedCategories);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при збереженні порядку категорій');
        }
    }
}

export default new CategoryApi();