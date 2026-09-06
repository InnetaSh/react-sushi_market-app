export interface EntityModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSave: (values: any) => Promise<void> | void;
    editingItem: any;
    type?: "category" | "product";
    currentLang?: string;
    categories?: any[];
}