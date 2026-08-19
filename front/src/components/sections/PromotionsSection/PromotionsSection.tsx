import React from 'react';
import { Flex, Image, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';

import CornerAccent from '../../UI/CornerAccent';
import ButtonGreen from '../../UI/ButtonGreen/ButtonGreen';

import styles from './PromotionsSection.module.scss';

interface PromotionsSectionProps {
    imageUrl: string;
    secondaryText: string;
    primaryTextFirst: string;
    primaryTextSecond: string;
    buttonText: string;
}

const PromotionsSection: React.FC<PromotionsSectionProps> = ({
    imageUrl,
    secondaryText,
    primaryTextFirst,
    primaryTextSecond,
    buttonText,
}) => {
    const navigate = useNavigate();

    const handleClick = (): void => {
        navigate('/sale');
    };

    return (
        <section className={styles.promotionsSection}>
            <div className={styles.promotionsContainer}>
                <div className={styles.promotionsContent}>
                    <div className={styles.promotionsCard}>
                        <div className={styles.decoration}>
                            <CornerAccent className={styles.decorationBlock} />
                        </div>

                        <Flex className={styles.promotionsLayout}>
                            <Flex
                                vertical
                                className={styles.promotionsInfo}
                            >
                                <Typography.Text
                                    className={styles.subtitle}
                                >
                                    {secondaryText}
                                </Typography.Text>

                                <Flex vertical>
                                    <Typography.Text
                                        className={styles.title}
                                    >
                                        {primaryTextFirst}
                                    </Typography.Text>

                                    <Typography.Text
                                        className={styles.title}
                                    >
                                        {primaryTextSecond}
                                    </Typography.Text>
                                </Flex>

                                <ButtonGreen
                                    name="stock"
                                    id="stock"
                                    text={buttonText}
                                    width="240px"
                                    onClick={handleClick}
                                />
                            </Flex>

                            <div className={styles.imageContainer}>
                                <Image
                                    src={imageUrl}
                                    alt="Promotion"
                                    preview={false}
                                    className={styles.promotionImage}
                                />
                            </div>
                        </Flex>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default PromotionsSection;