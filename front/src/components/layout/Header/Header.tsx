import React, { useState } from 'react';
import { Typography } from 'antd';
import { useNavigate, Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { CloseOutlined, UserOutlined, SettingOutlined } from '@ant-design/icons';
import { observer } from 'mobx-react-lite';

import { ButtonOrange } from '@UI/ButtonOrange/ButtonOrange';
import AuthStore from '@stores/AuthStore';
import styles from './Header.module.scss';

import logo from '@img/logo.png';
import icon from '@img/icon.png';

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

    const handleLogout = async () => {
        await AuthStore.logout();
        navigate('/login');
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


                        <div className={styles.containerItems}>
                            {AuthStore.isLoggedIn && (
                                <div className={styles.userInfo} style={{ display: 'flex', alignItems: 'center', gap: '10px', color: '#fff' }}>
                                    <UserOutlined />
                                    <span style={{ fontSize: '14px', fontWeight: 500 }}>
                                        {AuthStore.user?.name || AuthStore.user?.email || 'User'}
                                    </span>
                                </div>
                            )}
                            <div className={styles.containerBtn}>
                                {AuthStore.isLoggedIn && AuthStore.isAdmin && (
                                        <ButtonOrange
                                            text=""
                                            onClick={() => navigate('/admin')}
                                            width="80px"
                                            icon={<SettingOutlined />}
                                        />
                            )}
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
                        <Link to="/menu" onClick={toggleMenu} className={styles.menuItem}>
                            {t('MENU.DISHES')}
                        </Link>
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

                    {AuthStore.isLoggedIn ? (
                        <>
                            {AuthStore.isAdmin && (
                                <Link to="/admin" onClick={toggleMenu} className={styles.menuItem}>
                                    {t('MENU.ADMIN', 'Панель администратора')}
                                </Link>
                            )}
                            <Link
                                to="#"
                                onClick={(e) => { e.preventDefault(); handleLogout(); toggleMenu(); }}
                                className={styles.menuItem}
                            >
                                {t('MENU.LOGOUT', 'Вийти')}
                            </Link>
                        </>
                    ) : (
                        <Link to="/login" onClick={toggleMenu} className={styles.menuItem}>
                            {t('MENU.LOGIN')}
                        </Link>
                    )}
                </nav>
            </div>
        </>
    );
};

export default observer(Header);