import React, { useState } from 'react';
import { APIProvider, Map, AdvancedMarker, Pin } from '@vis.gl/react-google-maps';
import { ClockCircleFilled, PhoneFilled } from '@ant-design/icons';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import styles from './ContactsSection.module.scss';

const LOCATIONS = [
    {
        id: 1,
        city: 'Київ',
        address: 'вул. Нежинська, 5',
        phone: '+38 (068) 080-00-01',
        lat: 50.4501,
        lng: 30.5234,
        hours: '10:00 - 22:00'
    },
    {
        id: 2,
        city: 'Львів',
        address: 'вул. Новознесенська, 4',
        phone: '+38 (068) 080-00-02',
        lat: 49.8397,
        lng: 24.0297,
        hours: '11:00 - 22:00'
    },
    {
        id: 3,
        city: 'Ужгород',
        address: 'вул. Миколи Баб’яка, 8',
        phone: '+38 (068) 080-00-03',
        lat: 48.6208,
        lng: 22.2879,
        hours: '10:00 - 21:00'
    }
];

const ContactsSection: React.FC = () => {
    const [selected, setSelected] = useState(LOCATIONS[0]);

    return (
        <PageSectionLayout
            title="Контакты"
            description="Традиции Японии, вкус твоего города!"
            breadcrumbs={[
                { label: 'Главная', path: '/' },
                { label: 'Контакты' }
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
                                <h3 className={styles.city}>{loc.city}</h3>
                                <p className={styles.address}>{loc.address}</p>

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