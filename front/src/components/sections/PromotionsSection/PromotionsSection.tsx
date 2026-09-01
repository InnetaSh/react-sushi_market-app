import React from 'react';
import { Flex, Image, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import ButtonGreen from '@UI/ButtonGreen/ButtonGreen';

import styles from './PromotionsSection.module.scss';
import backImg from '@img/back_promotion.jpg';

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
        <PageSectionLayout backgroundImage={backImg}>
            <Flex className={styles.container}>
                <Flex vertical className={styles.wrapperItems}>
                    <Typography.Text className={styles.subtitle}>
                        {secondaryText}
                    </Typography.Text>

                    <Flex vertical>
                        <Typography.Text className={styles.title}>
                            {primaryTextFirst}
                        </Typography.Text>
                        <Typography.Text className={styles.title}>
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
                        className={styles.image}
                    />
                </div>
            </Flex>
        </PageSectionLayout>
    );
};

export default PromotionsSection;