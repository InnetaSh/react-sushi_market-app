import { makeAutoObservable, runInAction } from "mobx";
import ProductApi from "@/api/ProductApi";

class ProductStore {
    products = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchProducts(categoryId = null) {
        this.loading = true;
        try {
            const data = await ProductApi.getProducts(categoryId);

            runInAction(() => {
                this.products = data.map(item => ({
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

export default new ProductStore();