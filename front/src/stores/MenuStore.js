import { makeAutoObservable, runInAction } from "mobx";
import MenuApi from "@api/MenuApi"; 

class MenuStore {
    items = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    async fetchMenu(category = null) {
        this.loading = true;
        
        try {
            const data = await MenuApi.getMenu(category);

            runInAction(() => {
                this.items = data.map((item) => ({
                    ...item,
                    imgSrc: `http://localhost:5292/img/menu/${item.imgSrc}`
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

export default new MenuStore();