import React from 'react';

import MenuItem from './MenuItem/MenuItem';

import styles from './MenuSection.module.scss';

interface MenuItemData {
    imgSrc: string;
    count: string;
    title: string;
}

interface MenuSectionProps {
    menuItems: MenuItemData[];
    onCategoryClick: (title: string) => void;
}

const MenuSection: React.FC<MenuSectionProps> = ({
    menuItems,
    onCategoryClick,
}) => {
    return (
        <div className={styles.menuSection}>
            {menuItems.map((item) => (
                <MenuItem
                    key={item.title}
                    imageUrl={item.imgSrc}
                    count={item.count}
                    title={item.title}
                    onClick={() => onCategoryClick(item.title)}
                />
            ))}
        </div>
    );
};

export default MenuSection;