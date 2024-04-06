import {
  Button,
  Col,
  Row
} from 'antd';
import {
  styleContainerHeader,
  styleTitleHeader
} from './style';

import { MenuFoldOutlined, MenuUnfoldOutlined } from '@ant-design/icons';
import { Dispatch, SetStateAction } from 'react';

interface HeaderProps {
  collapsed: boolean
  setCollapsed: Dispatch<SetStateAction<boolean>>
}

const Header = (props: HeaderProps) => {

  const { collapsed, setCollapsed } = props;

  return (
    <Row justify='space-between' align='middle' style={styleContainerHeader}>
      <Col style={styleTitleHeader}>
        <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            style={{
              color: '#fff',
              fontSize: '16px',
              width: 64,
              height: 64,
            }}
          />
        <img style={{width: 150}} src="" alt="logo"/>
      </Col>
    </Row>
  );
};

export default Header;
