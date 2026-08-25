import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import LoginSection from "@section/LoginSection/LoginSection";

const { Content } = Layout;

const LoginPage = observer(() => {
  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
          <LoginSection/>
      </Content>
    </div>
  );
});

export default LoginPage;