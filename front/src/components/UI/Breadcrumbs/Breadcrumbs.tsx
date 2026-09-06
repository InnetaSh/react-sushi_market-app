import React from 'react';
import { Link } from 'react-router-dom';

import { BreadcrumbItem, BreadcrumbsProps } from '@models/breadcrumbs.types';
import styles from './Breadcrumbs.module.scss';

const Breadcrumbs: React.FC<BreadcrumbsProps> = ({ items }) => {
    return (
        <div className={styles.breadcrumbs}>
            {items.map((item, index) => {
                const isLast = index === items.length - 1;

                return (
                    <React.Fragment key={index}>
                        {index > 0 && <span className={styles.divider}> / </span>}
                        {item.path && !isLast ? (
                            <Link to={item.path} className={styles.link}>
                                {item.label}
                            </Link>
                        ) : (
                            <span className={styles.current}>{item.label}</span>
                        )}
                    </React.Fragment>
                );
            })}
        </div>
    );
};

export default Breadcrumbs;