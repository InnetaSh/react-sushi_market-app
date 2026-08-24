import { makeAutoObservable, runInAction } from "mobx";
import CategoryApi from "@api/CategoryApi"; 

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
            runInAction(() => {
                this.categories = data;
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
            runInAction(() => {
                this.currentCategoryProducts = data.products || data;
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
            runInAction(() => {
                this.categoriesWithProducts = data;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error fetching full menu:", error);
            runInAction(() => { this.loading = false; });
        }
    }
}

export default new CategoryStore();