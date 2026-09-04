import { makeAutoObservable, runInAction } from "mobx";
import CategoryApi from "@/api/categoryApi"; 

class CategoryStore {
    categories = [];
    currentCategoryProducts = [];
    categoriesWithProducts = []; 
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchCategories() {
        this.loading = true;
        try {
            const data = await CategoryApi.getCategories();
            
            const sortedCategories = (data || []).sort((a, b) => (a.sortOrder ?? 0) - (b.sortOrder ?? 0));
            runInAction(() => {
                this.categories = sortedCategories;
                this.loading = false;
            });
        } catch (error) {
            runInAction(() => { this.loading = false; });
        }
    }

    async fetchCategoryWithProducts(id) {
        this.loading = true;
        try {
            const data = await CategoryApi.getCategoryWithProducts(id);
            const products = data.products || data || [];
            
            const sortedProducts = products.sort((a, b) => (a.sortOrder ?? 0) - (b.sortOrder ?? 0));
            
            runInAction(() => {
                this.currentCategoryProducts = sortedProducts;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching category products:", error);
            runInAction(() => { this.loading = false; });
        }
    }

    async fetchCategoriesWithProducts() {
        this.loading = true;
        try {
            const data = await CategoryApi.getCategoriesWithProducts();
            
            const sortedData = (data || [])
                .sort((a, b) => (a.sortOrder ?? 0) - (b.sortOrder ?? 0))
                .map(cat => ({
                    ...cat,
                    products: (cat.products || []).sort((a, b) => (a.sortOrder ?? 0) - (b.sortOrder ?? 0))
                }));

            runInAction(() => {
                this.categoriesWithProducts = sortedData;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching full menu:", error);
            runInAction(() => { this.loading = false; });
        }
    }
}

export default new CategoryStore();