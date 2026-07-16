import React from 'react';
import { Flex, Image, Typography } from 'antd';

import OrangeBlock from '../../orangeBlock';
import ButtonGreenComponent from '../../buttonGreenComponent';

import styles from './AboutSection.module.scss';

interface AboutSectionProps {
    imageUrl: string;
    title: string;
    descriptionFirst: string;
    descriptionSecond: string;
    buttonText: string;
}

const AboutSection: React.FC<AboutSectionProps> = ({
    imageUrl,
    title,
    descriptionFirst,
    descriptionSecond,
    buttonText,
}) => {
    return (
        <section className={styles.section}>
            <div className={styles.sectionContainer}>
                <div className={styles.container}>
                    <div className={styles.containerItem}>
                        <div className={styles.orangeBlockWrapper}>
                            <OrangeBlock
                                className={styles.orangeBlock}
                            />
                        </div>

                        <Flex className={styles.containerImg}>
                            <Flex
                                vertical
                                className={styles.containerImgLeft}
                            >
                                <Typography.Text
                                    className={styles.primaryText}
                                >
                                    {title}
                                </Typography.Text>

                                <Flex
                                    vertical
                                    className={styles.description}
                                >
                                    <Typography.Text
                                        className={styles.secondaryText}
                                    >
                                        {descriptionFirst}
                                    </Typography.Text>

                                    <Typography.Text
                                        className={styles.secondaryText}
                                    >
                                        {descriptionSecond}
                                    </Typography.Text>
                                </Flex>

                                <ButtonGreenComponent
                                    name="stock"
                                    id="stock"
                                    text={buttonText}
                                    onClick={console.log('Button clicked')}
                                />
                            </Flex>

                            <div className={styles.imageWrapper}>
                                <Image
                                    src={imageUrl}
                                    alt="About"
                                    preview={false}
                                    className={styles.image}
                                />
                            </div>
                        </Flex>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default AboutSection;