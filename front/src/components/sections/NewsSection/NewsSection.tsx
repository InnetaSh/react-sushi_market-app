import React, { useEffect } from 'react';
import { Flex, Typography, Spin } from 'antd';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import newsStore from '@stores/newsStore';
import styles from './NewsSection.module.scss';

const NewsSection: React.FC = observer(() => {
    const { t, i18n } = useTranslation();

    useEffect(() => {
        newsStore.fetchNews();
    }, []);

    const currentLang = i18n.language?.toLowerCase().startsWith('en') ? 'En' : 'Ua';

    return (
        <PageSectionLayout
            breadcrumbs={[
                { label: t('BREADCRUMBS.HOME'), path: '/' },
                { label: t('BREADCRUMBS.NEWS') }
            ]}
        >
            <div className={styles.newsLayout}>
                <Flex vertical className={styles.headerInfo}>
                    <Typography.Text className={styles.title}>
                        {t('NEWS_SECTION.TITLE')}
                    </Typography.Text>
                    <Typography.Text className={styles.subtitle}>
                        {t('NEWS_SECTION.DESCRIPTION')}
                    </Typography.Text>
                </Flex>

                {newsStore.loading ? (
                    <Flex justify="center" align="center" style={{ minHeight: '200px' }}>
                        <Spin size="large" />
                    </Flex>
                ) : (
                    <Flex vertical className={styles.newsList} gap={24}>
                        {newsStore.news.map((item) => {
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
                    </Flex>
                )}
            </div>
        </PageSectionLayout>
    );
});

export default NewsSection;