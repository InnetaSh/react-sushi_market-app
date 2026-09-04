import React from "react";
import { Card, Image, Typography } from "antd";

import styles from "./PromotionDetails.module.scss";
import type { PromotionOffer as PromotionOfferType } from "./types";

const { Title, Paragraph } = Typography;

interface PromotionOfferProps {
    offer: PromotionOfferType;
}

const PromotionOffer: React.FC<PromotionOfferProps> = ({ offer }) => {
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
                    {offer.title}
                </Title>

                <Paragraph className={styles.date}>
                    {offer.date}
                </Paragraph>
                
                <Paragraph className={styles.description}>
                    {offer.description}
                </Paragraph>
            </div>
        </Card>
    );
};

export default PromotionOffer;