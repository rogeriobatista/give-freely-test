import Icon from '@mdi/react';
import { mdiLoading } from '@mdi/js';
import { Col, Layout, Row, Spin } from 'antd';

const antIcon = <Icon  color="#024183" path={mdiLoading} style={{ fontSize: 50}} spin />;

const Loading = () => {
  return (
    <Layout  style={{ 
        flex:1,
        justifyContent:'center',
        alignItems:'center',
        background: '#FFF',
        height:'50vw'
    }}>
      <Row justify='center' align='middle'>
        <Col style={{marginInline:12}}>
          <Spin indicator={antIcon} />
        </Col>
      </Row>
    </Layout>
  );
};

export default Loading;
