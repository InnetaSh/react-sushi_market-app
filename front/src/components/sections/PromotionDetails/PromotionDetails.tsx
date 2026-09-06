import React, { useEffect } from "react";
import { Row, Col, Skeleton } from "antd";
import { useTranslation } from "react-i18next";
import { observer } from "mobx-react-lite";

import PageSectionLayout from "@layout/PageSectionLayout/PageSectionLayout";
import PromotionOffer from "./PromotionOffer";
import promotionStore from "@stores/promotionStore";
import { getLocalizedPromotion } from "@utils/promotion.utils";

import styles from "./PromotionDetails.module.scss";

const PromotionDetails: React.FC = observer(() => {
    const { t, i18n } = useTranslation();

    useEffect(() => {
        promotionStore.fetchPromotions();
    }, []);

    return (
        <PageSectionLayout
            breadcrumbs={[
                { label: t('BREADCRUMBS.HOME' as any), path: '/' }, 
                { label: t('BREADCRUMBS.PROMOTIONS' as any) }         
            ]}
            title={t("PAGE_3_TEXT.TITLE" as any)}
            description={t("PAGE_3_TEXT.DESCRIPTION" as any)}
        >
            {promotionStore.loading && promotionStore.promotions.length === 0 ? (
                <Row gutter={[24, 24]} className={styles.row}>
                    {Array.from({ length: 2 }).map((_, index) => (
                        <Col key={index} xs={24} lg={12}>
                            <div style={{ padding: '24px', background: '#fff', borderRadius: '8px' }}>
                                <Skeleton active paragraph={{ rows: 4 }} />
                            </div>
                        </Col>
                    ))}
                </Row>
            ) : (
                <Row gutter={[24, 24]} className={styles.row}>
                    {promotionStore.promotions.map((offer) => {
                        const localizedOffer = getLocalizedPromotion(offer, i18n.language);

                        return (
                            <Col key={offer.id} xs={24} lg={12}>
                                <PromotionOffer offer={localizedOffer} />
                            </Col>
                        );
                    })}
                </Row>
            )}
        </PageSectionLayout>
    );
});

export default PromotionDetails;