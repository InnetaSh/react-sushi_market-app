import React from 'react';
import { Flex, Typography } from 'antd';

import CornerAccent from '../../UI/CornerAccent';
import SubmenuCard from './components/SubmenuCard/SubmenuCard';

import styles from './SubmenuSection.module.scss';
import backImgMenu from '@img/back_menu.jpg';

interface SubmenuItem {
    imgSrc: string;
    title: string;
    about_1: string;
    about_2: string;
    price: string;
}

interface SubmenuSectionProps {
    menuItems: SubmenuItem[];
}

const SubmenuSection: React.FC<SubmenuSectionProps> = ({
    menuItems,
}) => {
    return (
        <section className={styles.section}>
            <div className={styles.sectionContainer}>
                <div className={styles.container}>
                    <div className={styles.containerItem}
                        style={{ backgroundImage: `url(${backImgMenu})` 
                        }}>
                        <div className={styles.orangeBlockWrapper}>
                            <CornerAccent className={styles.orangeBlock} />
                        </div>

                        <Flex
                            vertical
                            className={styles.content}
                        >
                            {menuItems.length > 0 ? (
                                menuItems.map((item) => (
                                    <SubmenuCard
                                        key={item.title}
                                        imageUrl={item.imgSrc}
                                        title={item.title}
                                        description={[
                                            item.about_1,
                                            item.about_2,
                                            item.price,
                                        ]}
                                    />
                                ))
                            ) : (
                                <Typography.Title
                                    level={3}
                                    className={styles.emptyMessage}
                                >
                                    This menu is temporarily empty
                                </Typography.Title>
                            )}
                        </Flex>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default SubmenuSection;