import { makeAutoObservable, runInAction } from "mobx";
import CategoryApi from "@/api/CategoryApi";

class CategoryStore {
    categories = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchCategories() {
        this.loading = true;
        
        try {
            const data = await CategoryApi.getCategories();

            runInAction(() => {
                this.categories = data.map((item) => ({
                    ...item,
                    imgSrc: item.imgSrc ? `http://localhost:5292/${item.imgSrc}` : null
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

export default new CategoryStore();