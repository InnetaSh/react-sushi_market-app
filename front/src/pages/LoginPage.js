import { observer } from "mobx-react-lite";
import { Layout} from "antd";

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