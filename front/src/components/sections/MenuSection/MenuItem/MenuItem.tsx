import React from 'react';
import { Image, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import CornerAccent from '@UI/CornerAccent';
import ButtonGreen from '@UI/ButtonGreen/ButtonGreen';

import styles from './MenuItem.module.scss';

interface MenuItemProps {
    imageUrl: string;
    count: string;
    title: string;
    onClick: () => void;
}

const MenuItem: React.FC<MenuItemProps> = ({
    imageUrl,
    count,
    title,
    onClick,
}) => {
    const { t } = useTranslation();
    const currentLang = useTranslation().i18n.language;
    const isEn = currentLang === 'en';

    return (
        <div className={styles.menuItem}>
            <div className={styles.imageContainer}>
                <CornerAccent className={styles.orangeBlock} />

                <div className={styles.imageWrapper}>
                    <Image
                        src={imageUrl}
                        width="100%"
                        height="330px"
                        preview={false}
                        alt={title}
                    />
                </div>
            </div>

            <div className={styles.itemLink}>
                <div className={styles.itemText}>
                    <Typography.Text className={styles.secondaryText}>
                        {count}
                    </Typography.Text>

                    <Typography.Text className={styles.primaryText}>
                        {title}
                    </Typography.Text>
                </div>

                <ButtonGreen
                    name="stock"
                    id="stock"
                    text={t("MENU.GO_ TO_MENU", isEn ? "Go to menu" : "Перейти в меню")}
                    onClick={onClick}
                />
            </div>
        </div>
    );
};

export default MenuItem;