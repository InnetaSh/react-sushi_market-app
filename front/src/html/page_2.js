import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import MenuStore from "../stores/MenuStore";
import TopComponent from "../components/top_panel";
import MenuComponent from "../components/menu_panel";

const { Content } = Layout;

const Page_2 = observer(() => {
  const { category } = useParams();
  const { t } = useTranslation();

  useEffect(() => {
    MenuStore.fetchMenu(category);
  }, [category]);

  return (
    <Layout className="App">
      <TopComponent />
      
      <Content style={{ padding: '50px' }}>
        <div style={{ height: '150px' }}></div>
        
        {MenuStore.loading ? (
          <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <Spin size="large" tip={t("UI_TEXT.LOADING")} />
          </div>
        ) : (
          <MenuComponent 
            imgListData={MenuStore.items} 
            width='250px' 
            height='250px' 
          />
        )}
      </Content>
    </Layout>
  );
});

export default Page_2;