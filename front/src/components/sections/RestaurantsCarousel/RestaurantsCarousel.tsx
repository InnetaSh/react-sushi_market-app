import React, { useState, useEffect, useRef } from 'react';
import { Typography } from 'antd';
import { LeftOutlined, RightOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { LOCATIONS } from '@mocks/contactsData';
import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import backImg from '@img/back_small_house.png';

import styles from './RestaurantsCarousel.module.scss';

const { Text } = Typography;

interface AboutSectionProps {
    title: string;
    description: string;
}

const RestaurantsCarousel: React.FC<AboutSectionProps> = ({
    title,
    description
}) => {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [withTransition, setWithTransition] = useState(true);
    const viewportRef = useRef<HTMLDivElement>(null);

    const navigate = useNavigate();
    const { t } = useTranslation();

    const CARD_WIDTH = 380;
    const GAP = 20;


    useEffect(() => {
        const interval = setInterval(() => {
            setCurrentIndex((prev) => prev + 1);
        }, 5000);

        return () => clearInterval(interval);
    }, []);


    useEffect(() => {
        if (currentIndex >= LOCATIONS.length) {
            setTimeout(() => {
                setWithTransition(false);
                setCurrentIndex(0);
            }, 400);
        }

        if (currentIndex < 0) {
            setTimeout(() => {
                setWithTransition(false);
                setCurrentIndex(LOCATIONS.length - 1);
            }, 400);
        }
    }, [currentIndex]);


    useEffect(() => {
        if (!withTransition) {
            requestAnimationFrame(() => {
                requestAnimationFrame(() => {
                    setWithTransition(true);
                });
            });
        }
    }, [withTransition]);

    const handlePrev = () => {
        setCurrentIndex((prev) => prev - 1);
    };

    const handleNext = () => {
        setCurrentIndex((prev) => prev + 1);
    };

    const handleCardClick = (id: number) => {
        navigate(`/contacts?restaurant=${id}`);
    };

    const extendedList = [
        ...LOCATIONS.slice(-LOCATIONS.length),
        ...LOCATIONS,
        ...LOCATIONS.slice(0, LOCATIONS.length),
    ];

    const getOffset = () => {
        const centerIndex = currentIndex + LOCATIONS.length;
        return centerIndex * (CARD_WIDTH + GAP);
    };

    return (
        <PageSectionLayout backgroundImage={backImg}
            title={title}
            description={description}
        >
            <div className={styles.carouselContainer}>
                <div className={styles.viewport} ref={viewportRef}>
                    <div
                        className={styles.track}
                        style={{
                            transform: `translateX(-${getOffset()}px)`,
                            transition: withTransition ? 'transform 0.4s ease' : 'none',
                            gap: `${GAP}px`,
                        }}
                    >
                        {extendedList.map((restaurant, i) => (
                            <div
                                key={`${restaurant.id}-${i}`}
                                className={styles.cardWrapper}
                                style={{ width: CARD_WIDTH, flexShrink: 0 }}
                            >
                                <div
                                    className={styles.restaurantCard}
                                    onClick={() => handleCardClick(restaurant.id)}
                                    style={{ cursor: 'pointer' }}
                                >
                                    <div className={styles.imageContainer}>
                                        <img
                                            src={restaurant.imageSrc}
                                            alt={restaurant.title}
                                            className={styles.image}
                                        />
                                    </div>
                                    <div className={styles.content}>
                                        <Text className={styles.cardTitle}>{restaurant.title}</Text>
                                        <Text className={styles.address}>{t(restaurant.addressKey)}</Text>
                                        <div className={styles.scheduleWrapper}>
                                            <span className={styles.scheduleDot} />
                                            <Text className={styles.schedule}>{restaurant.hours}</Text>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
                <div className={styles.headerRow}>
                    <div className={styles.arrowsContainer}>
                        <button className={styles.arrowButton} onClick={handlePrev} aria-label="Previous">
                            <LeftOutlined />
                        </button>
                        <button className={styles.arrowButton} onClick={handleNext} aria-label="Next">
                            <RightOutlined />
                        </button>
                    </div>
                </div>
            </div>
        </PageSectionLayout>
    );
};

export default RestaurantsCarousel;