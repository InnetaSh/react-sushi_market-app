import kyivImg from '@img/city/Kyiv.avif';
import lvivImg from '@img/city/Lviv.avif';
import bukovelImg from '@img/city/Bukovel.avif';

export interface LocationItem {
    id: number;
    cityKey: string;
    addressKey: string;
    phone: string;
    lat: number;
    lng: number;
    hours: string;
    slug: string;
    title: string;
    imageSrc: string;
}

export const LOCATIONS: LocationItem[] = [
    {
        id: 1,
        slug: 'kyiv',
        title: 'Суши Маркет у Києві',
        imageSrc: kyivImg,
        cityKey: 'CONTACTS.LOCATIONS.KYIV.CITY',
        addressKey: 'CONTACTS.LOCATIONS.KYIV.ADDRESS',
        phone: '+38 (068) 080-00-01',
        lat: 50.4501,
        lng: 30.5234,
        hours: '10:00 - 22:00'
    },
    {
        id: 2,
        slug: 'lviv',
        title: 'Суши Маркет у Львові',
        imageSrc: lvivImg,
        cityKey: 'CONTACTS.LOCATIONS.LVIV.CITY',
        addressKey: 'CONTACTS.LOCATIONS.LVIV.ADDRESS',
        phone: '+38 (068) 080-00-02',
        lat: 49.8397,
        lng: 24.0297,
        hours: '11:00 - 22:00'
    },
    {
        id: 3,
        slug: 'uzhgorod',
        title: 'Суши Маркет в Ужгороді',
        imageSrc: bukovelImg,
        cityKey: 'CONTACTS.LOCATIONS.UZHGOROD.CITY',
        addressKey: 'CONTACTS.LOCATIONS.UZHGOROD.ADDRESS',
        phone: '+38 (068) 080-00-03',
        lat: 48.6208,
        lng: 22.2879,
        hours: '10:00 - 21:00'
    }
];