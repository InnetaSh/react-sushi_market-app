import React, { useEffect, useState } from 'react';
import { Flex, Typography, Skeleton, Pagination } from 'antd';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import newsStore from '@stores/newsStore';
import { NEWS_PAGE_SIZE } from '@constants/pagination';
import { getLocalizedNews } from '@utils/news.utils';
import styles from './NewsSection.module.scss';

const NewsSection: React.FC = observer(() => {
    const { t, i18n } = useTranslation();
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        newsStore.fetchNews();
    }, []);

    const totalNews = newsStore.news.length;
    const startIndex = (currentPage - 1) * NEWS_PAGE_SIZE;
    const currentNews = newsStore.news.slice(startIndex, startIndex + NEWS_PAGE_SIZE);

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
        window.scrollTo({ top: 0, behavior: 'smooth' });
    };

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

                {newsStore.loading && totalNews === 0 ? (
                    <div className={styles.newsGrid} style={{ marginTop: '24px' }}>
                        {Array.from({ length: 4 }).map((_, index) => (
                            <Skeleton key={index} active paragraph={{ rows: 3 }} />
                        ))}
                    </div>
                ) : (
                    <>
                        <div className={styles.newsGrid}>
                            {currentNews.map((item) => {
                                const { title, description } = getLocalizedNews(item, i18n.language);

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

                        {totalNews > NEWS_PAGE_SIZE && (
                            <div className={styles.paginationWrapper}>
                                <Pagination
                                    current={currentPage}
                                    pageSize={NEWS_PAGE_SIZE}
                                    total={totalNews}
                                    onChange={handlePageChange}
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