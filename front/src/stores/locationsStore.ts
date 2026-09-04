import { makeAutoObservable, runInAction } from 'mobx';
import locationApi from '@/api/locationApi';

export interface LocationItem {
    id: number;
    slug: string;
    titleKeyUa: string;
    titleKeyEn: string;
    cityKeyUa: string;
    cityKeyEn: string;
    addressKeyUa: string;
    addressKeyEn: string;
    phone: string;
    lat: number;
    lng: number;
    hours: string;
    imageSrc: string;
}

class LocationStore {
    locations: LocationItem[] = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    fetchLocations = async () => {
        this.loading = true;
        try {
            const data = await locationApi.getLocations();
            runInAction(() => {
                this.locations = data;
            });
        } catch (error) {
            console.error('Failed to load locations:', error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    };
}

export default new LocationStore();