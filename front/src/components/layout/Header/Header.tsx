import React, { useState } from 'react';
import { Typography } from 'antd';
import { useNavigate, Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { CloseOutlined, DownOutlined } from '@ant-design/icons';

import { ButtonOrange } from '../../UI/ButtonOrange/ButtonOrange';
import styles from './Header.module.scss';

import logo from '../../../img/logo.png';
import icon from '../../../img/icon.png';

const Header: React.FC = () => {
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    const toggleMenu = (): void => {
        setIsMenuOpen(!isMenuOpen);
    };

    const toggleLanguage = (): void => {
        i18n.changeLanguage(i18n.language === 'uk' ? 'en' : 'uk');
    };

    return (
        <>
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
                            <Typography.Text className={styles.logoSubtitle}>
                                sushi-bar
                            </Typography.Text>
                        </div>
                    </a>

                    <div className={styles.headerActions}>
                        <div className={styles.phone}>
                            <Typography.Text className={styles.phoneLabel}>
                                {t('HEADER.PHONE_LABEL')}
                            </Typography.Text>
                            <Typography.Text className={styles.phoneNumber}>
                                {t('HEADER.PHONE_NUMBER')}
                            </Typography.Text>
                        </div>

                        <ButtonOrange
                            text={i18n.language.toUpperCase()}
                            onClick={toggleLanguage}
                            width="60px"
                        />

                        <ButtonOrange
                            text="МЕНЮ"
                            onClick={toggleMenu}
                            width="150px"
                            icon={
                                <img
                                    src={icon}
                                    alt="Menu"
                                    style={{ width: 20, height: 20 }}
                                />
                            }
                        />
                    </div>
                </div>
            </header>

        
            <div 
                className={`${styles.overlay} ${isMenuOpen ? styles.overlayOpen : ''}`} 
                onClick={toggleMenu} 
            />

        
            <div className={`${styles.sideMenu} ${isMenuOpen ? styles.sideMenuOpen : ''}`}>
                <button className={styles.closeButton} onClick={toggleMenu}>
                    <CloseOutlined />
                </button>

                <nav className={styles.menuNav}>
                    <Link to="/" onClick={toggleMenu} className={styles.menuItem}>
                    {t('MENU.HOME')}
                    </Link>
                    
                    <div className={styles.menuItemWithSub}>
                        <span>{t('MENU.ABOUT')}</span>
                        <DownOutlined className={styles.subIcon} />
                    </div>

                    <div className={styles.menuItemWithSub}>
                        <span>{t('MENU.DISHES')}</span>
                        <DownOutlined className={styles.subIcon} />
                    </div>

                    <Link to="/sale" onClick={toggleMenu} className={styles.menuItem}>
                    {t('MENU.STOCK')}
                    </Link>
                    <Link to="/news" onClick={toggleMenu} className={styles.menuItem}>
                    {t('MENU.NEWS')}
                    </Link>
                    <Link to="/contacts" onClick={toggleMenu} className={styles.menuItem}>
                    {t('MENU.CONTACTS')}
                    </Link>
                    <Link to="/login" onClick={toggleMenu} className={styles.menuItem}>
                    {t('MENU.LOGIN')}
                    </Link>
                </nav>
            </div>
        </>
    );
};

export default Header;