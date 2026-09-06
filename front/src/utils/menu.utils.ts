import { BASE_HOST } from '@constants/api';
import { MenuItemData } from '@models/menu.types';

export const getLocalizedTitle = (item: MenuItemData, currentLang: string): string => {
    if (currentLang === 'uk' || currentLang === 'ua') {
        return item.titleUa || item.title || '';
    }
    if (currentLang === 'en') {
        return item.titleEn || item.title || '';
    }
    return item.title || item.titleUa || '';
};

export const getImageUrl = (item: MenuItemData): string => {
    const rawImg = item.imageUrl || item.imgSrc || '';
    if (!rawImg) return '';
    
    return rawImg.startsWith('http') 
        ? rawImg 
        : `${BASE_HOST}${rawImg.startsWith('/') ? '' : '/'}${rawImg}`;
};