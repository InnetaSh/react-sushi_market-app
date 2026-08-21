export interface PromotionOffer {
    image: string;
    title: string;
    description: string;
}

export interface PromotionDetailsProps {
    offers: PromotionOffer[];
}