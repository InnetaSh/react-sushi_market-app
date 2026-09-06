export interface LocationItem {
    id: number;
    imageSrc: string;
    hours: string;
    titleKeyEn?: string;
    titleKeyUa?: string;
    addressKeyEn?: string;
    addressKeyUa?: string;
}

export interface RestaurantsCarouselProps {
    title: string;
    description: string;
}