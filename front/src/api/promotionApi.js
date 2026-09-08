import api from './api';

class PromotionApi {
    async getPromotions() {
        try {
            const response = await api.get('/Promotions');

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при завантаженні акцій'
            );
        }
    }
}

export default new PromotionApi();