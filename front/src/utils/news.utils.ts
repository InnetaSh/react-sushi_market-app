import { NewsItem } from '@models/news.types';

export const getLocalizedNews = (item: NewsItem, currentLang: string) => {
    const isEn = currentLang?.toLowerCase().startsWith('en');
    return {
        title: isEn ? item.titleKeyEn : item.titleKeyUa,
        description: isEn ? item.descriptionKeyEn : item.descriptionKeyUa,
    };
};