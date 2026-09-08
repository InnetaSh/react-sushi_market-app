import { IProduct } from "./product.types";

export interface ICategory {
    id: number | string;
    name?: string;
    title?: string;
    sortOrder?: number;
    products?: IProduct[];
    [key: string]: any;
}