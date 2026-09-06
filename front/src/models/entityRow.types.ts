export interface EntityRowProps {
    sortOrder?: number;
    imgSrc?: string;
    title: string;
    subtitle?: string;
    categoryName?: string;
    price?: string | number;
    weight?: string;
    isSelected?: boolean;
    onClick?: () => void;
    onEdit: () => void;
    onDelete: () => void;
}