import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true 
});

class LocationApi {
    async getLocations() {
        try {
            const response = await api.get('/Locations');
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Помилка при завантаженні локацій');
        }
    }
}

export default new LocationApi();