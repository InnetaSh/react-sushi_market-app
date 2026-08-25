import React from 'react';
import { useTranslation } from 'react-i18next';
import MenuItem from './MenuItem/MenuItem';
import styles from './MenuSection.module.scss';

interface MenuItemData {
    id: number | string;
    imgSrc?: string;
    ImgSrc?: string;
    imageUrl?: string;
    ImageUrl?: string;
    count?: string | number;
    title?: string;
    titleUa?: string;
    titleEn?: string;
}

interface MenuSectionProps {
    menuItems: MenuItemData[];
    onCategoryClick: (id: number | string) => void;
}

const MenuSection: React.FC<MenuSectionProps> = ({
    menuItems,
    onCategoryClick,
}) => {
    const { i18n } = useTranslation();
    const currentLang = i18n.language;

    const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';
    const BASE_HOST = API_URL.replace(/\/api\/?$/, '');

    const getLocalizedTitle = (item: MenuItemData) => {
        if (currentLang === 'uk' || currentLang === 'ua') {
            return item.titleUa || item.title || '';
        }
        if (currentLang === 'en') {
            return item.titleEn || item.title || '';
        }
        return item.title || item.titleUa || '';
    };

    const getImageUrl = (item: MenuItemData) => {
        const rawImg = item.imgSrc || item.ImgSrc || item.imageUrl || item.ImageUrl || '';
        if (!rawImg) return '';
        
       return rawImg.startsWith('http') 
            ? rawImg 
            : `${BASE_HOST}${rawImg.startsWith('/') ? '' : '/'}${rawImg}`;
    };

    return (
        <div className={styles.menuSection}>
            {menuItems.map((item) => {
                const localizedTitle = getLocalizedTitle(item);
                const fullImageUrl = getImageUrl(item);

                return (
                    <MenuItem
                        key={item.id || localizedTitle}
                        imageUrl={fullImageUrl}
                        count={item.count ? String(item.count) : ""}
                        title={localizedTitle}
                        onClick={() => onCategoryClick(item.id)} 
                    />
                );
            })}
        </div>
    );
};

export default MenuSection;