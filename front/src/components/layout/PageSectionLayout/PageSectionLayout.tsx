import React, { ReactNode } from "react";
import { Typography } from "antd";
import CornerAccent from "@UI/CornerAccent";
import Breadcrumbs, { BreadcrumbItem } from "@UI/Breadcrumbs/Breadcrumbs";

import styles from "./PageSectionLayout.module.scss";

const { Title, Paragraph } = Typography;

interface PageSectionLayoutProps {
    title?: string;
    description?: string;
    backgroundImage?: string; 
    breadcrumbs?: BreadcrumbItem[];
    children: ReactNode;
}

const PageSectionLayout: React.FC<PageSectionLayoutProps> = ({
    title,
    description,
    backgroundImage,
    breadcrumbs,
    children,
}) => {
    return (
        <section className={styles.section}>
            <div className={styles.sectionContainer}>
                <div className={styles.container}>
                    <div className={styles.containerItem}>
                        <div className={styles.orangeBlockWrapper}>
                            <CornerAccent className={styles.orangeBlock} />
                        </div>

                        <div 
                            className={styles.contentWrapper}
                            style={
                                backgroundImage 
                                    ? { 
                                        backgroundImage: `url(${backgroundImage})`,
                                        backgroundSize: '100% 640px',
                                        backgroundPosition: 'bottom',
                                        backgroundRepeat: 'no-repeat',
                                      } 
                                    : undefined
                            }
                        >
                            {breadcrumbs && breadcrumbs.length > 0 && (
                                <div className={styles.breadcrumbsWrapper}>
                                    <Breadcrumbs items={breadcrumbs} />
                                </div>
                            )}
                            
                            {(title || description) && (
                                <div className={styles.sectionText}>
                                    {title && (
                                        <Title level={4} className={styles.titlePromotion}>
                                            {title}
                                        </Title>
                                    )}
                                    {description && (
                                        <Paragraph className={styles.descriptionPromotion}>
                                            {description}
                                        </Paragraph>
                                    )}
                                </div>
                            )}

                            <div className={styles.contentBody}>
                                {children}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default PageSectionLayout;