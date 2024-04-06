import { Col, Row, Typography } from "antd";
import NotfoundSVG from "assets/images/notfound";

const { Title } = Typography;

const NotFound = () => {
  return (
    <Row justify="center" align="middle">
      <Col>
        <NotfoundSVG />
        <Row justify="center" align="middle">
          <Col>
            <Title level={2}>404</Title>
            <Title level={5}>Sorry, we couldn't find this page</Title>
          </Col>
        </Row>
      </Col>
    </Row>
  );
};

export default NotFound;
