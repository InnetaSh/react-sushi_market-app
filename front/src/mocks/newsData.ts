export interface NewsItem {
    id: string;
    date: string;
    titleKey: string;
    descriptionKey: string;
    link: string;
}

export const NEWS_DATA: NewsItem[] = [
    {
        id: '1',
        date: '19.11.2020',
        titleKey: 'NEWS_SECTION.ITEMS.FIRST.TITLE',
        descriptionKey: 'NEWS_SECTION.ITEMS.FIRST.DESCRIPTION',
        link: '/news/1',
    },
    {
        id: '2',
        date: '25.10.2020',
        titleKey: 'NEWS_SECTION.ITEMS.SECOND.TITLE',
        descriptionKey: 'NEWS_SECTION.ITEMS.SECOND.DESCRIPTION',
        link: '/news/2',
    },
];