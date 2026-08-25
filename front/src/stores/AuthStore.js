import { makeAutoObservable, runInAction } from "mobx";
import UserApi from "@api/UserApi";

class AuthStore {
    user = null;
    isAuthenticated = false;
    isLoading = false;
    error = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return this.isAuthenticated;
    }

    get isAdmin() {
        const roles = this.user?.roles || [];
        return roles.includes('MainAdministrator');
    }

    setUserLoginResponse(data) {
        this.user = data?.user || data;
        this.isAuthenticated = true;
    }

    async login(credentials) {
        this.isLoading = true;
        this.error = null;
        try {
            const data = await UserApi.login(credentials);
            runInAction(() => {
                this.setUserLoginResponse(data);
                this.isLoading = false;
            });
            return data;
        } catch (error) {
            runInAction(() => {
                this.error = error.message;
                this.isLoading = false;
            });
            throw error;
        }
    }

    async register(userData) {
        this.isLoading = true;
        this.error = null;
        try {
            const data = await UserApi.register(userData);
            runInAction(() => {
                this.isLoading = false;
            });
            return data;
        } catch (error) {
            runInAction(() => {
                this.error = error.message;
                this.isLoading = false;
            });
            throw error;
        }
    }

    async logout() {
        this.isLoading = true;
        this.error = null;
        try {
            await UserApi.logout();
            runInAction(() => {
                this.user = null;
                this.isAuthenticated = false;
                this.isLoading = false;
            });
        } catch (error) {
            runInAction(() => {
                this.error = error.message;
                this.isLoading = false;
            });
            throw error;
        }
    }

    async checkAuth() {
        this.isLoading = true;
        try {
            const data = await UserApi.getUserInfo();
            runInAction(() => {
                if (data.isAuthenticated) {
                    this.user = { email: data.email, roles: data.roles };
                    this.isAuthenticated = true;
                } else {
                    this.user = null;
                    this.isAuthenticated = false;
                }
                this.isLoading = false;
            });
        } catch (error) {
            runInAction(() => {
                this.user = null;
                this.isAuthenticated = false;
                this.isLoading = false;
            });
        }
    }
}

export default new AuthStore();