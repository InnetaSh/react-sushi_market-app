export interface LocationItem {
    id: number;
    cityKey: string;     // Ключ для перевода города
    addressKey: string;  // Ключ для перевода адреса
    phone: string;
    lat: number;
    lng: number;
    hours: string;
}

export const LOCATIONS: LocationItem[] = [
    {
        id: 1,
        cityKey: 'CONTACTS.LOCATIONS.KYIV.CITY',
        addressKey: 'CONTACTS.LOCATIONS.KYIV.ADDRESS',
        phone: '+38 (068) 080-00-01',
        lat: 50.4501,
        lng: 30.5234,
        hours: '10:00 - 22:00'
    },
    {
        id: 2,
        cityKey: 'CONTACTS.LOCATIONS.LVIV.CITY',
        addressKey: 'CONTACTS.LOCATIONS.LVIV.ADDRESS',
        phone: '+38 (068) 080-00-02',
        lat: 49.8397,
        lng: 24.0297,
        hours: '11:00 - 22:00'
    },
    {
        id: 3,
        cityKey: 'CONTACTS.LOCATIONS.UZHGOROD.CITY',
        addressKey: 'CONTACTS.LOCATIONS.UZHGOROD.ADDRESS',
        phone: '+38 (068) 080-00-03',
        lat: 48.6208,
        lng: 22.2879,
        hours: '10:00 - 21:00'
    }
];