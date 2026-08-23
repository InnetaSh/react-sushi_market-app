import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import MenuStore from "../stores/CategoryStore";
import NewsSection from "@section/NewsSection/NewsSection";

const { Content } = Layout;

const NewsPage = observer(() => {
  const { category } = useParams();
  const { t } = useTranslation();

  useEffect(() => {
    MenuStore.fetchMenu(category);
  }, [category]);

  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
          <NewsSection/>
      </Content>
    </div>
  );
});

export default NewsPage;