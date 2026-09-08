import { JSX } from 'react';

export interface ProtectedRouteProps {
    children: JSX.Element;
    requiredRole?: string;
}

export interface IUser {
    email?: string;
    roles?: string[];
    [key: string]: any;
}

export interface IAuthResponse {
    token?: string;
    user?: IUser;
    roles?: string[];
    [key: string]: any;
}