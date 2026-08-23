import { create } from 'zustand';
import UserApi from '../api/UserApi';

export const useAuthStore = create((set) => ({
    user: null,
    isAuthenticated: false,
    isLoading: false,
    error: null,

    login: async (credentials) => {
        set({ isLoading: true, error: null });
        try {
            const data = await UserApi.login(credentials);
            set({ 
                user: data.user, 
                isAuthenticated: true, 
                isLoading: false 
            });
            return data;
        } catch (error) {
            set({ error: error.message, isLoading: false });
            throw error;
        }
    },

    register: async (userData) => {
        set({ isLoading: true, error: null });
        try {
            const data = await UserApi.register(userData);
            set({ 
                user: data.user, 
                isAuthenticated: true, 
                isLoading: false 
            });
            return data;
        } catch (error) {
            set({ error: error.message, isLoading: false });
            throw error;
        }
    },

    logout: async () => {
        set({ isLoading: true, error: null });
        try {
            await UserApi.logout();
            set({ user: null, isAuthenticated: false, isLoading: false });
        } catch (error) {
            set({ error: error.message, isLoading: false });
            throw error;
        }
    },

    checkAuth: async () => {
        set({ isLoading: true });
        try {
            const data = await UserApi.getUserInfo();
            if (data.isAuthenticated) {
                set({ 
                    user: { email: data.email, roles: data.roles }, 
                    isAuthenticated: true, 
                    isLoading: false 
                });
            } else {
                set({ user: null, isAuthenticated: false, isLoading: false });
            }
        } catch (error) {
            set({ user: null, isAuthenticated: false, isLoading: false });
        }
    }
}));