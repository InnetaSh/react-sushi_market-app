import React, { useRef } from 'react';
import { Flex, Typography, Pagination } from 'antd';
import { useTranslation } from 'react-i18next';

import CornerAccent from '@UI/CornerAccent';
import SubmenuCard from './components/SubmenuCard/SubmenuCard';

import styles from './SubmenuSection.module.scss';
import backImgMenu from '@img/back_menu.jpg';

interface SubmenuItem {
    id: number | string;
    imgSrc?: string;
    ImgSrc?: string;
    imageUrl?: string;
    ImageUrl?: string;
    title?: string;
    titleUa?: string;
    titleEn?: string;
    descriptionUa?: string;
    descriptionEn?: string;
    weightOrVolume?: string | number;
    price: number | string;
}

interface SubmenuSectionProps {
    menuItems: SubmenuItem[];
    currentPage: number;
    setCurrentPage: (page: number) => void;
}

const PAGE_SIZE = 10;

const SubmenuSection: React.FC<SubmenuSectionProps> = ({
    menuItems,
    currentPage,
    setCurrentPage,
}) => {
    const { i18n } = useTranslation();
    const currentLang = i18n.language;

    const sectionTopRef = useRef<HTMLDivElement>(null);

    const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5292/api';
    const BASE_HOST = API_URL.replace(/\/api\/?$/, '');

    const getLocalizedTitle = (item: SubmenuItem) => {
        if (currentLang === 'uk' || currentLang === 'ua') {
            return item.titleUa || item.title || '';
        }
        if (currentLang === 'en') {
            return item.titleEn || item.title || '';
        }
        return item.title || item.titleUa || '';
    };

    const getLocalizedDescription = (item: SubmenuItem) => {
        if (currentLang === 'uk' || currentLang === 'ua') {
            return item.descriptionUa || '';
        }
        if (currentLang === 'en') {
            return item.descriptionEn || '';
        }
        return item.descriptionUa || '';
    };

    const getImageUrl = (item: SubmenuItem) => {
        const rawImg = item.imgSrc || item.ImgSrc || item.imageUrl || item.ImageUrl || '';
        if (!rawImg) return '';
        return rawImg.startsWith('http')
            ? rawImg
            : `${BASE_HOST}${rawImg.startsWith('/') ? '' : '/'}${rawImg}`;
    };

    const startIndex = (currentPage - 1) * PAGE_SIZE;
    const currentItems = menuItems.slice(startIndex, startIndex + PAGE_SIZE);

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
        
       if (sectionTopRef.current) {
            const elementPosition = sectionTopRef.current.getBoundingClientRect().top;
            const offsetPosition = elementPosition + window.pageYOffset - 250;

            window.scrollTo({
                top: offsetPosition,
                behavior: 'smooth'
            });
        }
    };

    return (
        <section className={styles.section} ref={sectionTopRef}>
            <div className={styles.sectionContainer}>
                <div className={styles.container}>
                    <div className={styles.containerItem}
                        style={{
                            backgroundImage: `url(${backImgMenu})`
                        }}>
                        <div className={styles.orangeBlockWrapper}>
                            <CornerAccent className={styles.orangeBlock} />
                        </div>

                        <Flex
                            vertical
                            className={styles.content}
                        >
                            {currentItems.length > 0 ? (
                                <>
                                    {currentItems.map((item) => {
                                        const localizedTitle = getLocalizedTitle(item);
                                        const localizedDesc = getLocalizedDescription(item);
                                        const fullImageUrl = getImageUrl(item);
                                        const priceText = `${item.price} грн`;

                                        return (
                                            <SubmenuCard
                                                key={item.id || localizedTitle}
                                                imageUrl={fullImageUrl}
                                                title={localizedTitle}
                                                description={[
                                                    localizedDesc,
                                                    item.weightOrVolume ? String(item.weightOrVolume) : '',
                                                ].filter(Boolean)}
                                                price={priceText}
                                            />
                                        );
                                    })}

                                    {menuItems.length > PAGE_SIZE && (
                                        <Flex justify="center" style={{ marginTop: '30px', width: '100%' }}>
                                            <Pagination
                                                current={currentPage}
                                                pageSize={PAGE_SIZE}
                                                total={menuItems.length}
                                                onChange={handlePageChange}
                                                showSizeChanger={false}
                                            />
                                        </Flex>
                                    )}
                                </>
                            ) : (
                                <Typography.Title
                                    level={3}
                                    className={styles.emptyMessage}
                                >
                                    This menu is temporarily empty
                                </Typography.Title>
                            )}
                        </Flex>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default SubmenuSection;