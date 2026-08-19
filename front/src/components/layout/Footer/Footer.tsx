import React from 'react';
import { Flex, Image, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import CornerAccent from '../../UI/CornerAccent';

import imageUrl1 from '../../../img/magazine_1.png';
import imageUrl2 from '../../../img/magazine_2.png';
import imageUrl3 from '../../../img/magazine_3.png';
import imageUrl4 from '../../../img/magazine_4.png';

import styles from './Footer.module.scss';

interface ImageData {
    imageUrl: string;
}

const imageList: ImageData[] = [
    { imageUrl: imageUrl1 },
    { imageUrl: imageUrl2 },
    { imageUrl: imageUrl3 },
    { imageUrl: imageUrl4 },
];

const Footer: React.FC = () => {
    const { t } = useTranslation();

    const primaryText = t("PAGE_1_TEXT.BOTTOM_MSG");
    const secondaryText = t("PAGE_1_TEXT.BOTTOM_DESC");

    return (
        <section className={styles.footer}>
            <div className={styles.footerContainer}>
                <div className={styles.footerContent}>
                    <div className={styles.footerCard}>
                        <div className={styles.decoration}>
                            <CornerAccent
                                className={styles.decorationBlock}
                            />
                        </div>

                        <div className={styles.footerLayout}>
                            <Flex
                                vertical
                                justify="center"
                                className={styles.footerInfo}
                            >
                                <Flex
                                    vertical
                                    align="flex-start"
                                    className={styles.footerText}
                                >
                                    <Typography.Text
                                        className={styles.title}
                                    >
                                        {primaryText }
                                    </Typography.Text>

                                    <Typography.Text
                                        className={styles.subtitle}
                                    >
                                        {secondaryText}
                                    </Typography.Text>
                                </Flex>

                                <Flex
                                    justify="center"
                                    align="center"
                                    className={styles.imageList}
                                >
                                    {imageList.map(({ imageUrl }) => (
                                        <div
                                            className={styles.imageCard}
                                            key={imageUrl}
                                        >
                                            <Image
                                                src={imageUrl}
                                                width="100%"
                                                preview={false}
                                                alt="Magazine"
                                            />
                                        </div>
                                    ))}
                                </Flex>
                            </Flex>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default Footer;