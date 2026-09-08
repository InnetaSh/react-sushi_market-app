export interface IMenuItemProps {
    imageUrl: string;
    count: string;
    title: string;
    onClick: () => void;
}

export interface IProduct {
    id: number | string;
    title?: string;
    name?: string;
    price?: number;
    sortOrder?: number;
    [key: string]: any;
}
