import React from 'react';
import MainPage from '../pages/MainPage';
import MenuPage from '../pages/MenuPage';
import PromotionDetailsPage from '../pages/PromotionDetailsPage';
import NewsPage from '../pages/NewsPage';
import ContactsPage from '@pages/ContactsPage';
import LoginPage from '../pages/LoginPage'; 

export const routesConfig = [
    { path: '', element: <MainPage />, index: true },
    { path: 'menu', element: <MenuPage /> },
    { path: 'menu/search/category/:category', element: <MenuPage /> },
    { path: 'sale', element: <PromotionDetailsPage /> },
    { path: 'news', element: <NewsPage /> },
    { path: 'contacts', element: <ContactsPage /> },
    { path: 'login', element: <LoginPage /> }, 
];