import React from 'react';
import { Button, Typography } from 'antd';

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
    className,
    onClick,
    loading = false,
}) => {
    return (
        <Button
            type="text"
            name={name}
            id={id}
            className={`${styles.orangeBtn} ${className}`}
            style={{ width: width }}
            onClick={onClick}
            loading={loading}
        >
            <Typography.Text className={styles.text}>
                {text}
            </Typography.Text>
        </Button>
    );
};

export default ButtonOrangeBrdr;