import React from 'react';
import { Image, Typography } from 'antd';

import CornerAccent from '../../../UI/CornerAccent';
import ButtonGreen from '../../../UI/ButtonGreen/ButtonGreen';

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
                    text="Перейти в меню"
                      width="480px"
                    onClick={onClick}
                />
            </div>
        </div>
    );
};

export default MenuItem;