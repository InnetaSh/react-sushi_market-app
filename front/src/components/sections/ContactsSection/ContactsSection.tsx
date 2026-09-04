import React, { useEffect, useRef } from "react";
import { useSearchParams } from "react-router-dom";
import { APIProvider, Map, AdvancedMarker, Pin } from "@vis.gl/react-google-maps";
import { ClockCircleFilled, PhoneFilled } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import { observer } from "mobx-react-lite";
import { Skeleton, Spin, Flex } from "antd";

import PageSectionLayout from "@layout/PageSectionLayout/PageSectionLayout";
import locationStore from "@stores/locationsStore";
import styles from "./ContactsSection.module.scss";

const ContactsSection: React.FC = observer(() => {
    const { t, i18n } = useTranslation();
    const [searchParams, setSearchParams] = useSearchParams();

    const activeCardRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        locationStore.fetchLocations();
    }, []);

    const currentLang = i18n.language?.toLowerCase().startsWith('en') ? 'En' : 'Ua';
    const restaurantIdParam = searchParams.get('restaurant');

    const locations = locationStore.locations;
    const selected = locations.find(loc => loc.id === Number(restaurantIdParam)) || locations[0];

    useEffect(() => {
        if (activeCardRef.current) {
            activeCardRef.current.scrollIntoView({
                behavior: 'smooth',
                block: 'nearest', 
            });
        }
    }, [selected?.id, locationStore.loading]);

    return (
        <PageSectionLayout
            title={t("CONTACTS.TITLE" as any)}
            description={t("CONTACTS.DESCRIPTION" as any)}
            breadcrumbs={[
                { label: t("BREADCRUMBS.HOME" as any), path: '/' },
                { label: t("BREADCRUMBS.CONTACTS" as any) }
            ]}
        >
            <div className={styles.contentGrid}>
                <div className={styles.infoBlock}>
                    <div className={styles.scrollableList}>
                        {locationStore.loading && locations.length === 0 ? (
                            <div style={{ padding: '20px' }}>
                                <Skeleton active paragraph={{ rows: 3 }} style={{ marginBottom: '24px' }} />
                                <Skeleton active paragraph={{ rows: 3 }} style={{ marginBottom: '24px' }} />
                                <Skeleton active paragraph={{ rows: 3 }} />
                            </div>
                        ) : (
                            locations.map((loc) => {
                                const city = currentLang === 'En' ? loc.cityKeyEn : loc.cityKeyUa;
                                const address = currentLang === 'En' ? loc.addressKeyEn : loc.addressKeyUa;
                                const isSelected = selected?.id === loc.id;

                                return (
                                    <div
                                        key={loc.id}
                                        ref={isSelected ? activeCardRef : null} // Привязываем реф только к выбранной карточке
                                        className={`${styles.addressItem} ${isSelected ? styles.active : ''}`}
                                        onClick={() => setSearchParams({ restaurant: String(loc.id) })}
                                    >
                                        <h3 className={styles.city}>{city}</h3>
                                        <p className={styles.address}>{address}</p>

                                        <div className={styles.hours}>
                                            <ClockCircleFilled style={{ marginRight: '8px' }} />
                                            <span>{loc.hours}</span>
                                        </div>
                                    </div>
                                );
                            })
                        )}
                    </div>

                    <div className={styles.hotline}>
                        <PhoneFilled />
                        <span>{selected ? selected.phone : '...'}</span>
                    </div>
                </div>

                <div className={styles.mapBlock}>
                    {locationStore.loading && !selected ? (
                        <Flex justify="center" align="center" style={{ height: '100%', width: '100%', background: '#f5f5f5' }}>
                            <Spin size="large" />
                        </Flex>
                    ) : (
                        selected && (
                            <APIProvider apiKey={process.env.REACT_APP_GOOGLE_MAPS_API_KEY || ''}>
                                <Map
                                    center={{ lat: selected.lat, lng: selected.lng }}
                                    zoom={16}
                                    mapId="OSAMA_MAP"
                                    gestureHandling={'greedy'}
                                >
                                    <AdvancedMarker position={{ lat: selected.lat, lng: selected.lng }}>
                                        <Pin background={'#fe792e'} glyphColor={'#fff'} />
                                    </AdvancedMarker>
                                </Map>
                            </APIProvider>
                        )
                    )}
                </div>
            </div>
        </PageSectionLayout>
    );
});

export default ContactsSection;