export interface ButtonGreenProps {
    name?: string;
    id?: string;
    text: string;
    width?: string | number;
    onClick?: () => void;
}

export interface ButtonGreenProps {
    name?: string;
    id?: string;
    text: string;
    width?: string | number;
    onClick?: () => void;
}

export interface ButtonOrangeProps {
    text: string;
    onClick: () => void;
    width?: string | number;
    icon?: React.ReactNode;
    id?: string;
    name?: string;
    className?: string;
    loading?: boolean;
}

export interface ButtonOrangeBrdrProps {
    name?: string;
    id?: string;
    text: string;
    width?: string | number;
    className?: string;
    onClick?: () => void;
    loading?: boolean;
}