import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import NewsSection from "@section/NewsSection/NewsSection";

const { Content } = Layout;

const NewsPage = observer(() => {
  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
          <NewsSection/>
      </Content>
    </div>
  );
});

export default NewsPage;