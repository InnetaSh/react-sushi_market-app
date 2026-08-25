import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true
});

class ProductApi {
    async fetchProducts(categoryId) {
        try {
           const url = categoryId ? `/Products?categoryId=${categoryId}` : '/Products';
            const response = await api.get(url);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при завантаженні продуктів');
        }
    }

    async getProductById(id) {
        try {
            const response = await api.get(`/Products/${id}`);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при отриманні продукту');
        }
    }

    async createProduct(productData) {
        try {
            const response = await api.post('/Products', productData);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при створенні продукту');
        }
    }

    async updateProduct(id, productData) {
        try {
            const response = await api.put(`/Products/${id}`, productData);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при оновленні продукту');
        }
    }

    async deleteProduct(id) {
        try {
            const response = await api.delete(`/Products/${id}`);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при видаленні продукту');
        }
    }

    async reorderProducts(orderedProducts) {
        try {
            const response = await api.post('/Products/reorder', orderedProducts);
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при збереженні порядку продуктів');
        }
    }
}

export default new ProductApi();