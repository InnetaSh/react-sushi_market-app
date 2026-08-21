import React from 'react';
import { Flex, Typography } from 'antd';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import styles from './NewsSection.module.scss';

interface NewsItem {
    id: string;
    date: string;
    title: string;
    description: string;
    link: string;
}

const newsData: NewsItem[] = [
    {
        id: '1',
        date: '19.11.2020',
        title: 'Нам 1 год! Акции! Скидки!',
        description: 'При создании новости, помимо заголовка и содержимого, Вы можете задать еще ряд параметров. Тут Вы видите пример заполнения анонса новости.',
        link: '/news/1',
    },
    {
        id: '2',
        date: '25.10.2020',
        title: 'Новогодние каникулы',
        description: 'При создании новости, помимо заголовка и содержимого, Вы можете задать еще ряд параметров. Тут Вы видите пример заполнения анонса новости.',
        link: '/news/2',
    },
];

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
                    {newsData.map((item) => (
                        <div key={item.id} className={styles.newsCard}>
                            <Typography.Text className={styles.newsDate}>
                                {item.date}
                            </Typography.Text>
                            <Link to={item.link} className={styles.newsTitle}>
                                {item.title}
                            </Link>
                            <Typography.Paragraph className={styles.newsDescription}>
                                {item.description}
                            </Typography.Paragraph>
                        </div>
                    ))}
                </Flex>
            </div>
        </PageSectionLayout>
    );
};

export default NewsSection;