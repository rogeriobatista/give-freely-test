import { Breadcrumb, Col, Row } from "antd";
import Title from "antd/es/typography/Title";

const HomePage = () => {
  return (
    <Row justify="center" align="middle">
      <Col span={24}>
        <Breadcrumb items={[{ title: "Home" }]}></Breadcrumb>
      </Col>
      <Col span={24} flex={1} style={{ marginTop: 50 }}>
        <Row justify="center" align="middle">
          <Col span={24} style={{ textAlign: "center" }}>
            <Title>Welcome</Title>
            <Title level={2}>Employee Management System</Title>
          </Col>
        </Row>
      </Col>
    </Row>
  );
};

export default HomePage;
