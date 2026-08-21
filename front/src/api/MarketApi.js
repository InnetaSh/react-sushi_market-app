const API_URL = 'http://localhost:5292/api';

class MarketApi {
    async getMarketData() {
        const response = await fetch(`${API_URL}/Market`);
        if (!response.ok) {
            throw new Error('Помилка при завантаженні даних маркета');
        }
        return await response.json();
    }
}

export default new MarketApi();