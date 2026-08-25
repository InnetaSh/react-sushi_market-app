import React from 'react';
import { Flex, Typography } from 'antd';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import { NEWS_DATA } from '@mocks/newsData';
import styles from './NewsSection.module.scss';

const NewsSection: React.FC = () => {
    const { t } = useTranslation(); 

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

                <Flex vertical className={styles.newsList} gap={24}>
                    {NEWS_DATA.map((item) => (
                        <div key={item.id} className={styles.newsCard}>
                            <Typography.Text className={styles.newsDate}>
                                {item.date}
                            </Typography.Text>
                            <Link to={item.link} className={styles.newsTitle}>
                                {t(item.titleKey)}
                            </Link>
                            <Typography.Paragraph className={styles.newsDescription}>
                                {t(item.descriptionKey)}
                            </Typography.Paragraph>
                        </div>
                    ))}
                </Flex>
            </div>
        </PageSectionLayout>
    );
};

export default NewsSection;