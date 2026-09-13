import React from 'react';
import { Navigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import AuthStore from "@/stores/authStore";

import { ProtectedRouteProps } from "@models/user.types";

export const ProtectedRoute: React.FC<ProtectedRouteProps> = observer(({ children, requiredRole }) => {
    const token = localStorage.getItem('token');

    if (!token && !AuthStore.isLoggedIn) {
        return <Navigate to="/login" replace />;
    }

    if (requiredRole) {
        if (requiredRole === 'MainAdministrator' && AuthStore.isAdmin) {
            return children;
        }

        let roles = AuthStore.user?.roles;
        
        if (!roles) {
            const savedUserStr = localStorage.getItem('user');
            if (savedUserStr) {
                try {
                    const parsedUser = JSON.parse(savedUserStr);
                    roles = parsedUser.roles || parsedUser.role;
                } catch (e) {
                    roles = [];
                }
            }
        }

        const hasRole = Array.isArray(roles)
            ? roles.includes(requiredRole)
            : roles === requiredRole;

        if (!hasRole) {
            return <Navigate to="/" replace />;
        }
    }
    
    return children;
});