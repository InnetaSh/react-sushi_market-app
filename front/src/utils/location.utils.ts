import { LocationItem } from '@models/location.types';

export const getLocalizedLocation = (restaurant: LocationItem, currentLang: string) => {
    const isEn = currentLang?.toLowerCase().startsWith('en');

    return {
        title: isEn ? restaurant.titleKeyEn : restaurant.titleKeyUa,
        address: isEn ? restaurant.addressKeyEn : restaurant.addressKeyUa,
    };
};