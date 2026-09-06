import { ContactLocation } from '@models/contact.types';

export const getLocalizedContactInfo = (loc: ContactLocation, currentLang: string) => {
    const isEn = currentLang?.toLowerCase().startsWith('en');

    return {
        city: isEn ? loc.cityKeyEn : loc.cityKeyUa,
        address: isEn ? loc.addressKeyEn : loc.addressKeyUa,
    };
};