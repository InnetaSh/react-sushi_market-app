const API_URL = 'http://localhost:5292/api';

class MenuApi {
    async getMenu(category = null) {
        const url = category 
            ? `${API_URL}/Menu/search/category/${category}`
            : `${API_URL}/Menu`;

        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Помилка при завантаженні меню');
        }
        return await response.json();
    }
}

export default new MenuApi();