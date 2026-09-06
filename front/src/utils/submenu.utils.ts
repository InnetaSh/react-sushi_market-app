import { SubmenuItem } from '@models/submenu.types';

export const getLocalizedSubmenuTitle = (item: SubmenuItem, currentLang: string): string => {
    if (currentLang === 'uk' || currentLang === 'ua') {
        return item.titleUa || item.title || '';
    }
    if (currentLang === 'en') {
        return item.titleEn || item.title || '';
    }
    return item.title || item.titleUa || '';
};

export const getLocalizedSubmenuDescription = (item: SubmenuItem, currentLang: string): string => {
    if (currentLang === 'uk' || currentLang === 'ua') {
        return item.descriptionUa || '';
    }
    if (currentLang === 'en') {
        return item.descriptionEn || '';
    }
    return item.descriptionUa || '';
};

export const getSubmenuImageUrl = (item: SubmenuItem, baseHost: string): string => {
    const rawImg = item.imgSrc || item.ImgSrc || item.imageUrl || item.ImageUrl || '';
    if (!rawImg) return '';
    return rawImg.startsWith('http')
        ? rawImg
        : `${baseHost}${rawImg.startsWith('/') ? '' : '/'}${rawImg}`;
};