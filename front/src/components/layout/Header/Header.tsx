import React from 'react';
import { Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { ButtonOrange } from '../../UI/ButtonOrange/ButtonOrange';

import styles from './Header.module.scss';

import logo from '../../../img/logo.png';
import icon from '../../../img/icon.png';

const Header: React.FC = () => {
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();

    const handleMenuClick = (): void => {
        navigate('/menu');
    };

    const toggleLanguage = (): void => {
        i18n.changeLanguage(i18n.language === 'uk' ? 'en' : 'uk');
    };

    return (
        <header className={styles.header}>
            <div className={styles.headerContainer}>
                <a href="/" className={styles.logo}>
                    <img
                        src={logo}
                        alt="OSAMA sushi-bar logo"
                        className={styles.logoImage}
                    />

                    <div className={styles.logoTitle}>
                        <Typography.Text className={styles.logoName}>
                            OSAMA
                        </Typography.Text>

                        <Typography.Text
                            className={styles.logoSubtitle}
                        >
                            sushi-bar
                        </Typography.Text>
                    </div>
                </a>

                <div className={styles.headerActions}>
                    <div className={styles.phone}>
                        <Typography.Text className={styles.phoneLabel}>
                            {t('HEADER.PHONE_LABEL')}
                        </Typography.Text>

                        <Typography.Text
                            className={styles.phoneNumber}
                        >
                            {t('HEADER.PHONE_NUMBER')}
                        </Typography.Text>
                    </div>

                    <ButtonOrange
                        text={i18n.language.toUpperCase()}
                        onClick={toggleLanguage}
                        width="60px"
                    />

                    <ButtonOrange
                        text="MENU"
                        onClick={handleMenuClick}
                        width="150px"
                        icon={
                            <img
                                src={icon}
                                alt="Menu"
                                style={{
                                    width: 20,
                                    height: 20,
                                }}
                            />
                        }
                    />
                </div>
            </div>
        </header>
    );
};

export default Header;