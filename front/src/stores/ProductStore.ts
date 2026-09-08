import { makeAutoObservable, runInAction } from "mobx";
import ProductApi from "@/api/productApi";
import { IProduct } from "@models/product.types";

class ProductStore {
    products: IProduct[] = [];
    currentProduct: IProduct | null = null;
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchProducts(categoryId: number | string): Promise<void> {
        this.loading = true;
        try {
            const data = (await ProductApi.fetchProducts(categoryId)) as IProduct[];
        
            const sortedProducts = (data || []).sort((a, b) => (a.sortOrder ?? 0) - (b.sortOrder ?? 0));
            runInAction(() => {
                this.products = sortedProducts;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching products:", error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    async fetchProductById(id: number | string): Promise<void> {
        this.loading = true;
        try {
            const data = (await ProductApi.getProductById(id)) as IProduct;
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