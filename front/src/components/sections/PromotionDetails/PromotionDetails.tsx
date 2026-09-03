import React, { useEffect } from "react";
import { Row, Col, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import { observer } from "mobx-react-lite";

import PageSectionLayout from "@layout/PageSectionLayout/PageSectionLayout";
import PromotionOffer from "./PromotionOffer";
import promotionStore from "@stores/promotionStore";

import styles from "./PromotionDetails.module.scss";

const PromotionDetails: React.FC = observer(() => {
    const { t, i18n } = useTranslation();

    useEffect(() => {
        promotionStore.fetchPromotions();
    }, []);

    const currentLang = i18n.language?.toLowerCase().startsWith('en') ? 'En' : 'Ua';

    if (promotionStore.loading) {
        return (
            <PageSectionLayout
                breadcrumbs={[
                    { label: t('BREADCRUMBS.HOME'), path: '/' }, 
                    { label: t('BREADCRUMBS.PROMOTIONS') }          
                ]}
                title={t("PAGE_3_TEXT.TITLE")}
                description={t("PAGE_3_TEXT.DESCRIPTION")}
            >
                <Flex justify="center" align="center" style={{ minHeight: '300px', width: '100%' }}>
                    <Spin size="large" />
                </Flex>
            </PageSectionLayout>
        );
    }

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
                {promotionStore.promotions.map((offer) => {
                    const localizedOffer = {
                        ...offer,
                        date: currentLang === 'En' ? offer.dateKeyEn : offer.dateKeyUa,
                        title: currentLang === 'En' ? offer.titleKeyEn : offer.titleKeyUa,
                        description: currentLang === 'En' ? offer.descriptionKeyEn : offer.descriptionKeyUa,
                        dateKeyUa: offer.dateKeyUa,
                        dateKeyEn: offer.dateKeyEn,
                        titleKeyUa: offer.titleKeyUa,
                        titleKeyEn: offer.titleKeyEn,
                        descriptionKeyUa: offer.descriptionKeyUa,
                        descriptionKeyEn: offer.descriptionKeyEn,
                    };

                    return (
                        <Col key={offer.id} xs={24} lg={12}>
                            <PromotionOffer offer={localizedOffer} />
                        </Col>
                    );
                })}
            </Row>
        </PageSectionLayout>
    );
});

export default PromotionDetails;