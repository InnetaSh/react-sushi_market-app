import { makeAutoObservable, runInAction } from "mobx";

class MarketStore {
  imagesList = [];
  loading = false;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchMarketData() {
    this.loading = true;
    try {
      const response = await fetch(`http://localhost:5292/api/Market`);
      const data = await response.json();
      runInAction(() => {
        this.imagesList = data.map(item => ({
          ...item,
          imgSrc: `http://localhost:5292/${item.imgSrc}`
        }));
        this.loading = false;
      });
    } catch (error) {
      console.error("Exception: ", error);
      runInAction(() => this.loading = false);
    }
  }
}

export default new MarketStore();