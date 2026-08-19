import React from 'react';
import { Flex, Image, Typography } from 'antd';

import styles from './SubmenuCard.module.scss';

interface SubmenuCardProps {
    imageUrl: string;
    title: string;
    description: string[];
}

const SubmenuCard: React.FC<SubmenuCardProps> = ({
    imageUrl,
    title,
    description,
}) => {
    return (
        <Flex
            justify="space-between"
            align="center"
            className={styles.card}
        >
            <div className={styles.imageWrapper}>
                <Image
                    src={imageUrl}
                    alt={title}
                    preview={false}
                    className={styles.image}
                />
            </div>

            <Flex
                vertical
                className={styles.content}
            >
                <Typography.Text className={styles.title}>
                    {title}
                </Typography.Text>

                <Flex
                    vertical
                    className={styles.description}
                >
                    {description.map((text, index) => (
                        <Typography.Text
                            key={text}
                            className={`${styles.descriptionItem} ${index === description.length - 1 ? styles.price : ''}`}
                        >
                            {text}
                        </Typography.Text>
                    ))}
                </Flex>
            </Flex>
        </Flex>
    );
};

export default SubmenuCard;