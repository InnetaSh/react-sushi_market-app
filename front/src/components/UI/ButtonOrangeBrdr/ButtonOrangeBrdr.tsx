import React from 'react';
import { Button } from 'antd';

import styles from './ButtonOrangeBrdr.module.scss';

interface ButtonOrangeBrdrProps {
    name?: string;
    id?: string;
    text: string;
    width?: string | number;
    className?: string;
    onClick?: () => void;
    loading?: boolean;
}

const ButtonOrangeBrdr: React.FC<ButtonOrangeBrdrProps> = ({
    name,
    id,
    text,
    width,
    className = '',
    onClick,
    loading = false,
}) => {
    return (
        <Button
            type="default" 
            name={name}
            id={id}
            className={`${styles.button} ${className}`}
            style={{ width }}
            onClick={onClick}
            loading={loading}
        >
            <span className={styles.text}>{text}</span>
        </Button>
    );
};

export default ButtonOrangeBrdr;