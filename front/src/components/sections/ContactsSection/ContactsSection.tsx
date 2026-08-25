import React, { useState } from 'react';
import { APIProvider, Map, AdvancedMarker, Pin } from '@vis.gl/react-google-maps';
import { ClockCircleFilled, PhoneFilled } from '@ant-design/icons';
import { useTranslation } from 'react-i18next';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import { LOCATIONS } from '@mocks/contactsData';
import styles from './ContactsSection.module.scss';

const ContactsSection: React.FC = () => {
    const { t } = useTranslation();
    const [selected, setSelected] = useState(LOCATIONS[0]);

    return (
        <PageSectionLayout
            title={t("CONTACTS.TITLE")}
            description={t("CONTACTS.DESCRIPTION")}
            breadcrumbs={[
                { label: t("BREADCRUMBS.HOME"), path: '/' },
                { label: t("BREADCRUMBS.CONTACTS") }
            ]}
        >
            <div className={styles.contentGrid}>
                <div className={styles.infoBlock}>
                    <div className={styles.scrollableList}>
                        {LOCATIONS.map((loc) => (
                            <div
                                key={loc.id}
                                className={`${styles.addressItem} ${selected.id === loc.id ? styles.active : ''}`}
                                onClick={() => setSelected(loc)}
                            >
                                <h3 className={styles.city}>{t(loc.cityKey)}</h3>
                                <p className={styles.address}>{t(loc.addressKey)}</p>

                                <div className={styles.hours}>
                                    <ClockCircleFilled style={{ marginRight: '8px' }} />
                                    <span>{loc.hours}</span>
                                </div>
                            </div>
                        ))}
                    </div>

                    <div className={styles.hotline}>
                        <PhoneFilled />
                        <span>{selected.phone}</span>
                    </div>
                </div>

                <div className={styles.mapBlock}>
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
                </div>
            </div>
        </PageSectionLayout>
    );
};

export default ContactsSection;