import React from 'react';
import { Image, Typography } from 'antd';

import styles from './SubmenuCard.module.scss';

interface SubmenuCardProps {
    imageUrl: string;
    title: string;
    description: string[];
    price: string | number;
}

const SubmenuCard: React.FC<SubmenuCardProps> = ({
    imageUrl,
    title,
    description,
    price,
}) => {
    return (
        <div className={styles.card}>
            <div className={styles.imageWrapper}>
                <Image
                    src={imageUrl}
                    alt={title}
                    preview={false}
                    className={styles.image}
                />
            </div>

            <div className={styles.content}>
                <Typography.Text className={styles.title}>
                    {title}
                </Typography.Text>

                <div className={styles.description}>
                    {description.map((text) => (
                        text ? (
                            <Typography.Text key={text} className={styles.descriptionItem}>
                                {text}
                            </Typography.Text>
                        ) : null
                    ))}
                </div>
            </div>

            <div className={styles.priceColumn}>
                <Typography.Text className={styles.price}>
                    {price}
                </Typography.Text>
            </div>
        </div>
    );
};

export default SubmenuCard;