import React, { useState, useEffect, useRef } from 'react';
import { Typography, Skeleton, Flex } from 'antd';
import { LeftOutlined, RightOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import locationStore from '@stores/locationsStore';
import backImg from '@img/back_small_house.png';

import styles from './RestaurantsCarousel.module.scss';

const { Text } = Typography;

interface AboutSectionProps {
    title: string;
    description: string;
}

const RestaurantsCarousel: React.FC<AboutSectionProps> = observer(({
    title,
    description
}) => {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [withTransition, setWithTransition] = useState(true);
    const viewportRef = useRef<HTMLDivElement>(null);

    const navigate = useNavigate();
    const { t, i18n } = useTranslation();

    const CARD_WIDTH = 380;
    const GAP = 20;

    useEffect(() => {
        locationStore.fetchLocations();
    }, []);

    const currentLang = i18n.language?.toLowerCase().startsWith('en') ? 'En' : 'Ua';
    const locations = locationStore.locations;

    useEffect(() => {
        if (locations.length === 0) return;

        const interval = setInterval(() => {
            setCurrentIndex((prev) => prev + 1);
        }, 5000);

        return () => clearInterval(interval);
    }, [locations.length]);

    useEffect(() => {
        if (locations.length === 0) return;

        if (currentIndex >= locations.length) {
            setTimeout(() => {
                setWithTransition(false);
                setCurrentIndex(0);
            }, 400);
        }

        if (currentIndex < 0) {
            setTimeout(() => {
                setWithTransition(false);
                setCurrentIndex(locations.length - 1);
            }, 400);
        }
    }, [currentIndex, locations.length]);

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
        if (locations.length === 0) return;
        setCurrentIndex((prev) => prev - 1);
    };

    const handleNext = () => {
        if (locations.length === 0) return;
        setCurrentIndex((prev) => prev + 1);
    };

    const handleCardClick = (id: number) => {
        navigate(`/contacts?restaurant=${id}`);
    };

    if (locationStore.loading && locations.length === 0) {
        return (
            <PageSectionLayout backgroundImage={backImg} title={title} description={description}>
                <div className={styles.carouselContainer}>
                    <Flex gap={GAP} style={{ overflow: 'hidden', padding: '20px 0' }}>
                        <Skeleton.Node active style={{ width: CARD_WIDTH, height: 350 }} />
                        <Skeleton.Node active style={{ width: CARD_WIDTH, height: 350 }} />
                        <Skeleton.Node active style={{ width: CARD_WIDTH, height: 350 }} />
                    </Flex>
                </div>
            </PageSectionLayout>
        );
    }

    if (locations.length === 0) {
        return null;
    }

    const extendedList = [
        ...locations.slice(-locations.length),
        ...locations,
        ...locations.slice(0, locations.length),
    ];

    const getOffset = () => {
        const centerIndex = currentIndex + locations.length;
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
                        {extendedList.map((restaurant, i) => {
                            const restaurantTitle = currentLang === 'En' ? restaurant.titleKeyEn : restaurant.titleKeyUa;
                            const address = currentLang === 'En' ? restaurant.addressKeyEn : restaurant.addressKeyUa;

                            return (
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
                                                alt={restaurantTitle}
                                                className={styles.image}
                                            />
                                        </div>
                                        <div className={styles.content}>
                                            <Text className={styles.cardTitle}>{restaurantTitle}</Text>
                                            <Text className={styles.address}>{address}</Text>
                                            <div className={styles.scheduleWrapper}>
                                                <span className={styles.scheduleDot} />
                                                <Text className={styles.schedule}>{restaurant.hours}</Text>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
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
});

export default RestaurantsCarousel;