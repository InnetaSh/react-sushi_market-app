import React from 'react';
import { Button, Typography } from 'antd';

import { ButtonGreenProps } from '@models/button.types';
import styles from './ButtonGreen.module.scss';

const ButtonGreen: React.FC<ButtonGreenProps> = ({
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

export default ButtonGreen;