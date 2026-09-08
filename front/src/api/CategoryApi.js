import api from './api';

class CategoryApi {
    async getCategories() {
        try {
            const response = await api.get('/Categories');
            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при завантаженні категорій'
            );
        }
    }

    async getCategoryById(id) {
        try {
            const response = await api.get(`/Categories/${id}`);
            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при отриманні категорії'
            );
        }
    }

    async getCategoryWithProducts(id) {
        try {
            const response = await api.get(
                `/Categories/${id}/products`
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при отриманні продуктів категорії'
            );
        }
    }

    async getCategoriesWithProducts() {
        try {
            const response = await api.get(
                '/Categories/with-products'
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при отриманні меню категорій та продуктів'
            );
        }
    }

    async createCategory(categoryData) {
        try {
            const response = await api.post(
                '/Categories',
                categoryData
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при створенні категорії'
            );
        }
    }

    async updateCategory(id, categoryData) {
        try {
            const response = await api.put(
                `/Categories/${id}`,
                categoryData
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при оновленні категорії'
            );
        }
    }

    async deleteCategory(id) {
        try {
            const response = await api.delete(
                `/Categories/${id}`
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при видаленні категорії'
            );
        }
    }

    async reorderCategory(categoryId, newSortOrder) {
        try {
            const response = await api.patch(
                '/Categories/reorder',
                {
                    categoryId,
                    newSortOrder
                }
            );

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при збереженні порядку категорій'
            );
        }
    }
}

export default new CategoryApi();