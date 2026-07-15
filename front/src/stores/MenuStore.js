import { makeAutoObservable, runInAction } from "mobx";

class MenuStore {
  items = [];
  loading = false;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchMenu(category = null) {
    this.loading = true;
    const url = category 
      ? `http://localhost:5292/api/Menu/search/category/${category}`
      : `http://localhost:5292/api/Menu`;
      
    try {
      const response = await fetch(url);
      const data = await response.json();
      runInAction(() => {
        this.items = data.map((item) => ({
          ...item,
          imgSrc: `http://localhost:5292/img/menu/${item.imgSrc}`
        }));
        this.loading = false;
      });
    } catch (error) {
      console.error("Exception: ", error);
      runInAction(() => { this.loading = false; });
    }
  }
}

export default new MenuStore();