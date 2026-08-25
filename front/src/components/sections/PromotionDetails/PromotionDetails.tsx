import React from "react";
import { Row, Col } from "antd";
import { useTranslation } from "react-i18next";

import PageSectionLayout from "@layout/PageSectionLayout/PageSectionLayout";
import PromotionOffer from "./PromotionOffer";

import styles from "./PromotionDetails.module.scss";
import type { PromotionDetailsProps } from "./types";

const PromotionDetails: React.FC<PromotionDetailsProps> = ({ offers }) => {
    const { t } = useTranslation();
    
    return (
        <PageSectionLayout
            breadcrumbs={[
                { label: t('BREADCRUMBS.HOME'), path: '/' }, 
                { label: t('BREADCRUMBS.PROMOTIONS') }          
            ]}
            title={t("PAGE_3_TEXT.TITLE")}
            description={t("PAGE_3_TEXT.DESCRIPTION")}
        >
            <Row gutter={[24, 24]} className={styles.row}>
                {offers.map((offer) => (
                    <Col key={offer.title} xs={24} lg={12}>
                        <PromotionOffer offer={offer} />
                    </Col>
                ))}
            </Row>
        </PageSectionLayout>
    );
};

export default PromotionDetails;