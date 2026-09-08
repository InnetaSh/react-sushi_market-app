import { makeAutoObservable, runInAction } from "mobx";
import UserApi from "@/api/userApi";
import { IUser, IAuthResponse } from "@models/user.types";

class AuthStore {
    user: IUser | null = null;
    isAuthenticated = false;
    isLoading = false;
    error: string | null = null;

    constructor() {
        makeAutoObservable(this);

        const token = localStorage.getItem('accessToken');
        if (token) {
            this.isAuthenticated = true;
            const savedUser = localStorage.getItem('user');
            if (savedUser) {
                try {
                    this.user = JSON.parse(savedUser);
                } catch {
                    this.user = null;
                }
            }
        }
    }

    get isLoggedIn() {
        return this.isAuthenticated;
    }

    get isAdmin() {
        const roles = this.user?.roles;
        const singleRole = this.user?.role;

        if (Array.isArray(roles)) {
            return roles.includes('MainAdministrator');
        }
        return singleRole === 'MainAdministrator';
    }

    setUserLoginResponse(data: IAuthResponse) {
        this.user = data?.user || data;
        this.isAuthenticated = true;

        if (this.user) {
            localStorage.setItem('user', JSON.stringify(this.user));
        }
        if (data?.accessToken) {
            localStorage.setItem('accessToken', data.accessToken);
        }
        if (data?.refreshToken) {
            localStorage.setItem('refreshToken', data.refreshToken);
        }
    }

    async login(credentials: any) {
        this.isLoading = true;
        this.error = null;
        try {
            const data = await UserApi.login(credentials);
            runInAction(() => {
                this.setUserLoginResponse(data);
                this.isLoading = false;
            });
            return data;
        } catch (error: any) {
            runInAction(() => {
                this.error = error.message;
                this.isLoading = false;
            });
            throw error;
        }
    }

    async register(userData: any) {
        this.isLoading = true;
        this.error = null;
        try {
            const data = await UserApi.register(userData);
            runInAction(() => {
                if (data && (data.accessToken || data.user)) {
                    this.setUserLoginResponse(data);
                }
                this.isLoading = false;
            });
            return data;
        } catch (error: any) {
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
        } catch (error) {
            console.error('Logout error:', error);
        } finally {
            runInAction(() => {
                this.user = null;
                this.isAuthenticated = false;
                this.isLoading = false;
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                localStorage.removeItem('user');
            });
        }
    }

    async checkAuth() {
        this.isLoading = true;
        try {
            const data = await UserApi.getUserInfo();
            runInAction(() => {
                if (data) {
                    this.user = data;
                    this.isAuthenticated = true;
                    localStorage.setItem('user', JSON.stringify(data));
                } else {
                    this.user = null;
                    this.isAuthenticated = false;
                    localStorage.removeItem('accessToken');
                    localStorage.removeItem('refreshToken');
                    localStorage.removeItem('user');
                }
                this.isLoading = false;
            });
        } catch (error) {
            runInAction(() => {
                this.user = null;
                this.isAuthenticated = false;
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                localStorage.removeItem('user');
                this.isLoading = false;
            });
        }
    }
}

export default new AuthStore();