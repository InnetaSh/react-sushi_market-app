import { makeAutoObservable, runInAction } from "mobx";
import ProductApi from "@api/ProductApi";

class ProductStore {
    products = [];
    currentProduct = null;
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchProducts(categoryId) {
        this.loading = true;
        try {
            const data = await ProductApi.fetchProducts(categoryId);
            runInAction(() => {
                this.products = data;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching products:", error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    async fetchProductById(id) {
        this.loading = true;
        try {
            const data = await ProductApi.getProductById(id);
            runInAction(() => {
                this.currentProduct = data;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching product by id:", error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }
}

export default new ProductStore();