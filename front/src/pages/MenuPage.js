import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import MenuStore from "../stores/CategoryStore";
import SubmenuSection from "../components/sections/SubmenuSection/SubmenuSection";

const { Content } = Layout;

const MenuPage = observer(() => {
  const { category } = useParams();
  const { t } = useTranslation();

  useEffect(() => {
    MenuStore.fetchMenu(category);
  }, [category]);

  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>

        {MenuStore.loading ? (
          <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <Spin size="large" tip={t("UI_TEXT.LOADING")} />
          </div>
        ) : (
          <SubmenuSection
            menuItems={MenuStore.items}
            />
        )}
      </Content>
    </div>
  );
});

export default MenuPage;