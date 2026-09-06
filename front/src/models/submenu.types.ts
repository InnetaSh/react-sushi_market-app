export interface SubmenuItem {
    id: number | string;
    imgSrc?: string;
    ImgSrc?: string;
    imageUrl?: string;
    ImageUrl?: string;
    title?: string;
    titleUa?: string;
    titleEn?: string;
    descriptionUa?: string;
    descriptionEn?: string;
    weightOrVolume?: string | number;
    price: number | string;
}

export interface SubmenuSectionProps {
    menuItems: SubmenuItem[];
    currentPage: number;
    setCurrentPage: (page: number) => void;
}