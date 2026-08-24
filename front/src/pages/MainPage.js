import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useNavigate } from 'react-router-dom';
import { useTranslation } from "react-i18next";
import { Spin, Flex } from "antd";
import '../style.css';

import CategoryStore from "../stores/CategoryStore";

import PromotionsSection from '../components/sections/PromotionsSection/PromotionsSection';
import AboutSection from '../components/sections/AboutSection/AboutSection';
import MenuSection from '../components/sections/MenuSection/MenuSection';

import imageUrl1 from "../img/img1.png";

const MainPage = observer(() => {
  const navigate = useNavigate();
  const { t } = useTranslation();

  const sentCategory = (categoryId) => {
    navigate(`/menu/search/category/${categoryId}`);
  };

  useEffect(() => {
    CategoryStore.fetchCategories();
  }, []);

  return (
    <div className="App">
      <Flex vertical align="center">
        <PromotionsSection
          imageUrl={imageUrl1}
          secondaryText={t("PAGE_1_TEXT.TITLE_1")}
          primaryTextFirst={t("PAGE_1_TEXT.TITLE_2")}
          primaryTextSecond={t("PAGE_1_TEXT.TITLE_3")}
          buttonText={t("PAGE_1_TEXT.BTN_ACTIONS")}
        />

        {CategoryStore.loading ? (
          <div style={{ textAlign: 'center', padding: '50px' }}>
            <Spin size="large" />
          </div>
        ) : (
          <MenuSection
            menuItems={CategoryStore.categories}
            onCategoryClick={sentCategory} 
          />
        )}

        <AboutSection
          imageUrl={imageUrl1}
          title={t("PAGE_1_TEXT.RESTAURANTS_TITLE")}
          descriptionFirst={t("PAGE_1_TEXT.RESTAURANTS_DESC_1")}
          descriptionSecond={t("PAGE_1_TEXT.RESTAURANTS_DESC_2")}
          buttonText={t("PAGE_1_TEXT.BTN_READ_MORE")}
        />
      </Flex>
    </div>
  );
});

export default MainPage;