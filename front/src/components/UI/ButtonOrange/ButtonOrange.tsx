import React from 'react';
import { Button } from 'antd';

import { ButtonOrangeProps } from '@models/button.types';
import styles from './ButtonOrange.module.scss';

export const ButtonOrange: React.FC<ButtonOrangeProps> = ({
    text,
    onClick,
    width,
    icon,
    id,
    name,
    className = '',
    loading = false,
}) => {
    return (
        <Button
            type="primary"
            id={id}
            name={name}
            onClick={onClick}
            className={`${styles.orangeBtn} ${className}`}
            style={{ width: width }}
            icon={icon}
            loading={loading}
        >
            {text}
        </Button>
    );
};