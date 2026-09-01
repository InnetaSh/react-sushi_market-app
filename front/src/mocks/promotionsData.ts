import hours from '@img/promotion/hours.png';
import birthday from '@img/promotion/birthday.png';

export interface PromotionItem {
    id: string;
    image: string;
    dateKey: string;
    titleKey: string;
    descriptionKey: string;
    link: string;
}

export const PROMOTION_DATA: PromotionItem[] = [
    {
        id: '1',
        image: hours,
        dateKey: "PAGE_3_TEXT.SALE_1_DATE",
        titleKey: "PAGE_3_TEXT.SALE_1_NAME",
        descriptionKey: "PAGE_3_TEXT.SALE_1_DESC",
        link: "/sale/happy-hours"
    },
    {
        id: '2',
        image: birthday,
        dateKey: "PAGE_3_TEXT.SALE_2_DATE",
        titleKey: "PAGE_3_TEXT.SALE_2_NAME",
        descriptionKey: "PAGE_3_TEXT.SALE_2_DESC",
        link: "/sale/birthday"
    },
];