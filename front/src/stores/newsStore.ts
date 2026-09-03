import { makeAutoObservable, runInAction } from 'mobx';
import newsApi from '@api/newsApi';

export interface NewsItem {
    id: number;
    date: string;
    titleKeyUa: string;
    titleKeyEn: string;
    descriptionKeyUa: string;
    descriptionKeyEn: string;
    link: string;
}

class NewsStore {
    news: NewsItem[] = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    fetchNews = async () => {
        this.loading = true;
        try {
            const data = await newsApi.getNews();
            runInAction(() => {
                this.news = data;
            });
        } catch (error) {
            console.error('Failed to load news:', error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    };
}

export default new NewsStore();