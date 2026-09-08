import api from './api';

class UserApi {
    async login(credentials) {
        const response = await api.post('/auth/login', credentials);

        if (response.data?.accessToken) {
            localStorage.setItem(
                'accessToken',
                response.data.accessToken
            );
        }

        if (response.data?.refreshToken) {
            localStorage.setItem(
                'refreshToken',
                response.data.refreshToken
            );
        }

        return response.data;
    }

    async register(userData) {
        const response = await api.post('/auth/register', userData);

        if (response.data?.accessToken) {
            localStorage.setItem(
                'accessToken',
                response.data.accessToken
            );
        }

        if (response.data?.refreshToken) {
            localStorage.setItem(
                'refreshToken',
                response.data.refreshToken
            );
        }

        return response.data;
    }

    async googleLogin(idTokenOrObj) {
        let token = idTokenOrObj;
        if (typeof idTokenOrObj === 'object' && idTokenOrObj !== null) {
            token = idTokenOrObj.idToken || idTokenOrObj.credential;
        }

        const response = await api.post(
            '/auth/google-login',
            { idToken: token }
        );

        if (response.data?.accessToken) {
            localStorage.setItem('accessToken', response.data.accessToken);
        }

        if (response.data?.refreshToken) {
            localStorage.setItem('refreshToken', response.data.refreshToken);
        }

        return response.data;
    }

    async refreshToken() {
        const refreshToken = localStorage.getItem('refreshToken');

        const response = await api.post(
            '/auth/refresh-token',
            { refreshToken }
        );

        if (response.data?.accessToken) {
            localStorage.setItem(
                'accessToken',
                response.data.accessToken
            );
        }

        if (response.data?.refreshToken) {
            localStorage.setItem(
                'refreshToken',
                response.data.refreshToken
            );
        }

        return response.data;
    }

    async logout() {
        const refreshToken = localStorage.getItem('refreshToken');

        await api.post(
            '/auth/logout',
            {},
            {
                headers: {
                    'X-Refresh-Token': refreshToken
                }
            }
        );

        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
    }

    async getUserInfo() {
        const response = await api.get('/auth/user-info');

        return response.data;
    }
}

export default new UserApi();