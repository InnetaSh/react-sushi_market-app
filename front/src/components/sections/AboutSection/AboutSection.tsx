import React from 'react';
import { Flex, Image, Typography } from 'antd';

import CornerAccent from '../../UI/CornerAccent';
import ButtonGreen from '../../UI/ButtonGreen/ButtonGreen';

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
                            <CornerAccent
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

                                <ButtonGreen
                                    name="stock"
                                    id="stock"
                                    text={buttonText}
                                    width="240px"
                                    onClick={() => console.log('Button clicked')}
                                />
                            </Flex>

                            <div className={styles.imageContainer}>
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