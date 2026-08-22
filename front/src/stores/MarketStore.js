import { makeAutoObservable, runInAction } from "mobx";
import MarketApi from "@api/MarketApi";

class MarketStore {
    imagesList = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchMarketData() {
        this.loading = true;
        try {
            const data = await MarketApi.getMarketData();

            runInAction(() => {
                this.imagesList = data.map(item => ({
                    ...item,
                    imgSrc: `http://localhost:5292/${item.imgSrc}`
                }));
                this.loading = false;
            });
        } catch (error) {
            console.error("Exception: ", error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }
}

export default new MarketStore();