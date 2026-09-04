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