import React from 'react';
import { Button, Typography } from 'antd';

import styles from './ButtonOrangeBrdr.module.scss';

interface ButtonOrangeBrdrProps {
    name?: string;
    id?: string;
    text: string;
    width?: string | number;
    onClick?: () => void;
}

const ButtonOrangeBrdr: React.FC<ButtonOrangeBrdrProps> = ({
    name,
    id,
    text,
    width,
    onClick,
}) => {
    return (
        <Button
            type="text"
            name={name}
            id={id}
            className={styles.button}
            style={{ width: width }}
            onClick={onClick}
        >
            <Typography.Text className={styles.text}>
                {text}
            </Typography.Text>
        </Button>
    );
};

export default ButtonOrangeBrdr;