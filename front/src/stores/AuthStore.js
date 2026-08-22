import { makeAutoObservable } from 'mobx';

class AuthStore {
    token = localStorage.getItem('auth_token') || null;
    refreshToken = localStorage.getItem('refresh_token') || null;
    user = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.token;
    }

    setUserLoginResponse(response, customRefreshToken = null) {
       this.token = response.token || response.accessToken;
        this.refreshToken = response.refreshToken || customRefreshToken;
        this.user = response.user || null;

        if (this.token) {
            localStorage.setItem('auth_token', this.token);
        }
        if (this.refreshToken) {
            localStorage.setItem('refresh_token', this.refreshToken);
        }
    }

    logout = () => {
        this.token = null;
        this.refreshToken = null;
        this.user = null;
        localStorage.removeItem('auth_token');
        localStorage.removeItem('refresh_token');
    }
}

export default new AuthStore();