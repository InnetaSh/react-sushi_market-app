import api from './api';

class LocationApi {
    async getLocations() {
        try {
            const response = await api.get('/Locations');

            return response.data;
        } catch (error) {
            throw new Error(
                error.response?.data?.message ||
                'Помилка при завантаженні локацій'
            );
        }
    }
}

export default new LocationApi();