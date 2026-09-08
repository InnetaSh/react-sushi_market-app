import React from 'react';
import { Navigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import AuthStore from "@stores/AuthStore";

import { ProtectedRouteProps } from "@models/user.types";

export const ProtectedRoute: React.FC<ProtectedRouteProps> = observer(({ children, requiredRole }) => {
    const token = localStorage.getItem('token');

    if (!token && !AuthStore.isLoggedIn) {
        return <Navigate to="/login" replace />;
    }

    if (requiredRole) {
        let roles = AuthStore.user?.roles;
        if (!roles) {
            const savedRoleStr = localStorage.getItem('role');
            roles = savedRoleStr ? JSON.parse(savedRoleStr) : [];
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