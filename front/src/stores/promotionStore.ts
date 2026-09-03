import { makeAutoObservable, runInAction } from 'mobx';
import promotionApi from '@api/promotionApi';

export interface PromotionItem {
    id: number;
    imageUrl: string;
    dateKeyUa: string;
    dateKeyEn: string;
    titleKeyUa: string;
    titleKeyEn: string;
    descriptionKeyUa: string;
    descriptionKeyEn: string;
    link: string;
}

class PromotionStore {
    promotions: PromotionItem[] = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    fetchPromotions = async () => {
        this.loading = true;
        try {
            const data = await promotionApi.getPromotions();
            runInAction(() => {
                this.promotions = data;
            });
        } catch (error) {
            console.error('Failed to load promotions:', error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    };
}

export default new PromotionStore();