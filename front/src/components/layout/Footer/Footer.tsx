import React from 'react';
import { Flex, Image, Typography } from 'antd';

import OrangeBlock from '../../orangeBlock';

import imageUrl1 from '../../../img/magazine_1.png';
import imageUrl2 from '../../../img/magazine_2.png';
import imageUrl3 from '../../../img/magazine_3.png';
import imageUrl4 from '../../../img/magazine_4.png';

import styles from './Footer.module.scss';
import { useTranslation } from 'react-i18next';

interface FooterProps {
    bigText: string;
    smallText: string;
}

interface ImageData {
    imgSrc: string;
}

const imgListData: ImageData[] = [
    { imgSrc: imageUrl1 },
    { imgSrc: imageUrl2 },
    { imgSrc: imageUrl3 },
    { imgSrc: imageUrl4 },
];

const Footer: React.FC<FooterProps> = () => {
    const { t } = useTranslation();
    const primaryText = t("PAGE_1_TEXT.BOTTOM_MSG");
    const secondaryText = t("PAGE_1_TEXT.BOTTOM_MSG");
    return (
        <section className={styles.sectionBottom}>
            <div className={styles.sectionContainer}>
                <div className={styles.container}>
                    <div className={styles.containerItem}>
                        <div className={styles.orangeBlockWrapper}>
                            <OrangeBlock className={styles.orangeBlock} />
                        </div>

                        <div className={styles.containerImg}>
                            <Flex
                                vertical
                                justify="center"
                                className={styles.containerImgBottom}
                            >
                                <Flex
                                    vertical
                                    align="flex-start"
                                    className={styles.flexLeft}
                                >
                                    <Typography.Text
                                        className={styles.primaryText}
                                    >
                                        {primaryText}
                                    </Typography.Text>

                                    <Typography.Text
                                        className={styles.secondaryText}
                                    >
                                        {secondaryText}
                                    </Typography.Text>
                                </Flex>

                                <Flex
                                    justify="center"
                                    align="center"
                                    className={styles.bottomImages}
                                >
                                    {imgListData.map(({ imgSrc }) => (
                                        <div
                                            className={styles.imgBottom}
                                            key={imgSrc}
                                        >
                                            <Image
                                                src={imgSrc}
                                                width="100%"
                                                preview={false}
                                                alt=""
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