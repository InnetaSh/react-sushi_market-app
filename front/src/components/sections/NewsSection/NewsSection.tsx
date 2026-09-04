import React, { useEffect, useState } from 'react';
import { Flex, Typography, Skeleton, Pagination } from 'antd';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import newsStore from '@stores/newsStore';
import styles from './NewsSection.module.scss';

const NewsSection: React.FC = observer(() => {
    const { t, i18n } = useTranslation();
    const [currentPage, setCurrentPage] = useState(1);
    const pageSize = 6;

    useEffect(() => {
        newsStore.fetchNews();
    }, []);

    const currentLang = i18n.language?.toLowerCase().startsWith('en') ? 'En' : 'Ua';

    const totalNews = newsStore.news.length;
    const startIndex = (currentPage - 1) * pageSize;
    const currentNews = newsStore.news.slice(startIndex, startIndex + pageSize);

    return (
        <PageSectionLayout
            breadcrumbs={[
                { label: t('BREADCRUMBS.HOME' as any), path: '/' },
                { label: t('BREADCRUMBS.NEWS' as any) }
            ]}
        >
            <div className={styles.newsLayout}>
                <Flex vertical className={styles.headerInfo}>
                    <Typography.Text className={styles.title}>
                        {t('NEWS_SECTION.TITLE' as any)}
                    </Typography.Text>
                    <Typography.Text className={styles.subtitle}>
                        {t('NEWS_SECTION.DESCRIPTION' as any)}
                    </Typography.Text>
                </Flex>

                {newsStore.loading && newsStore.news.length === 0 ? (
                    <div className={styles.newsGrid} style={{ marginTop: '24px' }}>
                        <Skeleton active paragraph={{ rows: 3 }} />
                        <Skeleton active paragraph={{ rows: 3 }} />
                        <Skeleton active paragraph={{ rows: 3 }} />
                        <Skeleton active paragraph={{ rows: 3 }} />
                    </div>
                ) : (
                    <>
                        <div className={styles.newsGrid}>
                            {currentNews.map((item) => {
                                const title = currentLang === 'En' ? item.titleKeyEn : item.titleKeyUa;
                                const description = currentLang === 'En' ? item.descriptionKeyEn : item.descriptionKeyUa;

                                return (
                                    <div key={item.id} className={styles.newsCard}>
                                        <Typography.Text className={styles.newsDate}>
                                            {item.date}
                                        </Typography.Text>
                                        <Link to={item.link} className={styles.newsTitle}>
                                            {title}
                                        </Link>
                                        <Typography.Paragraph className={styles.newsDescription}>
                                            {description}
                                        </Typography.Paragraph>
                                    </div>
                                );
                            })}
                        </div>

                        {totalNews > pageSize && (
                            <div className={styles.paginationWrapper}>
                                <Pagination
                                    current={currentPage}
                                    pageSize={pageSize}
                                    total={totalNews}
                                    onChange={(page) => {
                                        setCurrentPage(page);
                                        window.scrollTo({ top: 0, behavior: 'smooth' });
                                    }}
                                    showSizeChanger={false}
                                />
                            </div>
                        )}
                    </>
                )}
            </div>
        </PageSectionLayout>
    );
});

export default NewsSection;