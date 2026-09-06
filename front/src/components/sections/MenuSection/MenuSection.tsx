import React from 'react';
import { useTranslation } from 'react-i18next';
import MenuItem from './MenuItem/MenuItem';
import { MenuItemData, getLocalizedTitle, getImageUrl } from '@/utils/menu.utils';
import styles from './MenuSection.module.scss';

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

    return (
        <div className={styles.menuSection}>
            {menuItems.map((item) => {
                const localizedTitle = getLocalizedTitle(item, currentLang);
                const fullImageUrl = getImageUrl(item);

                return (
                    <MenuItem
                        key={item.id}
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