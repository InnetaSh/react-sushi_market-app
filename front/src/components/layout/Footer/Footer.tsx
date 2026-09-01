import React from 'react';
import { Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import logo from '@img/logo.png';

import styles from './Footer.module.scss';
import backImg from '@img/back_footer.jpg';

const Footer: React.FC = () => {
    const { t } = useTranslation();

    const secondaryText = t("PAGE_1_TEXT.BOTTOM_DESC");

    return (
        <PageSectionLayout backgroundImage={backImg}>
            <div className={styles.footerLayout}>
                <Flex
                    vertical
                    className={styles.footerContainer}
                >
                    <Flex
                        justify="space-between"
                        align="center"
                        className={styles.footerTopRow}
                    >
                        <Link to="/" className={styles.logo}>
                            <img
                                src={logo}
                                alt="OSAMA sushi-bar logo"
                                className={styles.logoImage}
                            />
                            <div className={styles.logoTitle}>
                                <Typography.Text className={styles.logoName}>
                                    OSAMA
                                </Typography.Text>
                                <Typography.Text className={styles.logoSubtitle}>
                                    sushi-bar
                                </Typography.Text>
                            </div>
                        </Link>

                        <Flex
                            align="center"
                            className={styles.navLinks}
                            gap="large"
                        >
                            <Link to="/menu" className={styles.navLink}>
                                {t("BREADCRUMBS.MENU") || "Меню"}
                            </Link>
                            <Link to="/sale" className={styles.navLink}>
                                {t("BREADCRUMBS.PROMOTIONS") || "Акції"}
                            </Link>
                            <Link to="/contacts" className={styles.navLink}>
                                {t("BREADCRUMBS.CONTACTS") || "Контакти"}
                            </Link>
                        </Flex>
                    </Flex>

                    <Flex
                        vertical
                        align="center"
                        className={styles.footerBottomRow}
                    >
                        <Typography.Text className={styles.subtitle}>
                            {secondaryText}
                        </Typography.Text>
                    </Flex>
                </Flex>
            </div>
        </PageSectionLayout>
    );
};

export default Footer;