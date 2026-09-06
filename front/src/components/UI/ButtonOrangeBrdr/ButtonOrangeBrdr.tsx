import React from 'react';
import { Button } from 'antd';

import { ButtonOrangeBrdrProps } from '@models/button.types';
import styles from './ButtonOrangeBrdr.module.scss';

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