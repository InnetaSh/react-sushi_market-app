import React from 'react';
import { Button } from 'antd';
import styles from './ButtonOrange.module.scss';

interface ButtonOrangeProps {
  text: string;
  onClick: () => void;
  width?: string | number;
  icon?: React.ReactNode;
  id?: string;
  name?: string;
  className?: string;
  loading?: boolean;
}

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