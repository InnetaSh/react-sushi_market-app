import React from "react";
import { Card, Image, Typography } from "antd";
import { useTranslation } from "react-i18next";

import styles from "./PromotionDetails.module.scss";
import type { PromotionOffer as PromotionOfferType } from "./types";


const { Title, Paragraph } = Typography;

interface PromotionOfferProps {
    offer: PromotionOfferType;
}

const PromotionOffer: React.FC<PromotionOfferProps> = ({ offer }) => {
        const { t } = useTranslation();
        
        return (
        <Card
            className={styles.offerCard}
            bordered={false}
        >
            
            <div className={styles.imageWrapper}>
                <Image
                    src={offer.image}
                    alt={offer.title}
                    preview={false}
                    className={styles.image}
                />
            </div>

            <div className={styles.content}>
                <Title
                    level={4}
                    className={styles.title}
                >
                    {t(offer.titleKey)}
                </Title>

                <Paragraph className={styles.date}>
                    {t(offer.dateKey) }
                </Paragraph>
                <Paragraph className={styles.description}>
                    {t(offer.descriptionKey )}
                </Paragraph>
            </div>
        </Card>
    );
};

export default PromotionOffer;