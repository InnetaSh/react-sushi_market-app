import React from "react";
import { useNavigate } from 'react-router-dom';
import { useLanguage } from "../../contexts/LanguageContext";
import { ButtonOrange } from '../ButtonOrange/ButtonOrange';
import styles from './Header.module.scss';
import logo from "../../img/logo.png";
import icon from "../../img/icon.png";

export default function Header() {
    const navigate = useNavigate();
const { language, setLanguage } = useLanguage();

    const handleMenuClick = () => navigate(`/menu`);

    const toggleLanguage = () => {
        setLanguage(language === 'uk' ? 'en' : 'uk');
    };

    return (
        <header className={styles.headerContent}>
            <div className={styles.headerContent_container}>


                <a href="/" className={styles.logoContainer}>
                    <img src={logo} alt="logo" className={styles.logoImg} />
                    <div className={styles.logoText}>
                        <span style={{ fontSize: '30px' }}>OSAMA</span>
                        <span style={{ fontSize: '15px' }}>sushi-bar</span>
                    </div>
                </a>

                <div className={styles.rightPanel}>
                    <div className={styles.phoneContainer}>
                        <span style={{ fontSize: '15px' }}>Phone</span>
                        <span style={{ fontSize: '30px' }}>8(050)000-00-00</span>
                    </div>

                  <ButtonOrange
                        text={language.toUpperCase()}
                        onClick={toggleLanguage}
                        width="60px"
                    />
                    <ButtonOrange
                        text="MENU"
                        onClick={handleMenuClick}
                        width="150px"
                        icon={<img src={icon} alt="icon" style={{ width: 20, height: 20 }} />}
                    />
                </div>
            </div>
        </header>
    );
}