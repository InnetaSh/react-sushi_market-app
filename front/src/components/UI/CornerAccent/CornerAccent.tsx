import React from 'react';
import styles from './CornerAccent.module.scss';

interface CornerAccentProps {
    variant?: 'left' | 'right'; 
    className?: string;
}

const CornerAccent: React.FC<CornerAccentProps> = ({ variant = 'left', className }) => {
    const defaultClassName = variant === 'right' ? styles.orangeBlockRight : styles.orangeBlock;
    
    return <div className={`${defaultClassName} ${className || ''}`} />;
};

export default CornerAccent;