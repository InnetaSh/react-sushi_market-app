export interface PromotionOfferType {
    id: string | number;
    imageUrl: string;
    image?: string;
    date?: string;
    title?: string;
    description?: string;
    dateKeyUa?: string;
    dateKeyEn?: string;
    titleKeyUa?: string;
    titleKeyEn?: string;
    descriptionKeyUa?: string;
    descriptionKeyEn?: string;
}

export interface PromotionsSectionProps {
    imageUrl: string;
    secondaryText: string;
    primaryTextFirst: string;
    primaryTextSecond: string;
    buttonText: string;
}

export interface PromotionOffer {
    id: number;
    image: string;
    title: string;
    description: string;
    date: string;
    dateKeyUa?: string;
    dateKeyEn?: string;
    titleKeyUa?: string;
    titleKeyEn?: string;
    descriptionKeyUa?: string;
    descriptionKeyEn?: string;
}

export interface PromotionDetailsProps {
    offers: PromotionOffer[];
}