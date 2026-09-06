import { PromotionOfferType } from '@models/promotion.types';

export const getLocalizedPromotion = (offer: PromotionOfferType, currentLang: string): PromotionOfferType => {
    const isEn = currentLang?.toLowerCase().startsWith('en');

    return {
        ...offer,
        image: offer.imageUrl,
        date: isEn ? offer.dateKeyEn : offer.dateKeyUa,
        title: isEn ? offer.titleKeyEn : offer.titleKeyUa,
        description: isEn ? offer.descriptionKeyEn : offer.descriptionKeyUa,
    };
};