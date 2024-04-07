import { Button, Col, Form, Input, Row, message } from "antd";
import { useNavigate } from "react-router-dom";
import { SignInAsync } from "service/auth";
import { useDispatch } from "react-redux";
import { login } from "store/login";
import Title from "antd/es/typography/Title";

type FieldType = {
  email?: string;
  password?: string;
  remember?: string;
};

const SignInPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [messageApi, contextHolder] = message.useMessage();

  const onFinish = (values: any) => {
    SignInAsync(values).then((response) => {
      if (response.data.isLogged) {
        dispatch(login(response.data));
        return navigate("/");
      }

      messageApi.open({
        type: "error",
        content: "Invalid credentials",
      });
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    messageApi.open({
      type: "error",
      content: errorInfo,
    });
  };

  return (
    <>
      {contextHolder}
      <Row justify="center" align="middle" style={{ height: "100vh" }}>
        <Col span={8}>
          <Col span={24} style={{ textAlign: "center" }}>
            <Title>Welcome</Title>
          </Col>
          <Form
            name="basic"
            layout="vertical"
            initialValues={{ remember: false }}
            style={{ padding: 20 }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <Form.Item<FieldType>
              label="Email"
              name="email"
              rules={[{ required: true, message: "Please inform your user!" }]}
            >
              <Input />
            </Form.Item>

            <Form.Item<FieldType>
              label="Password"
              name="password"
              rules={[
                { required: true, message: "Please inform your password!" },
              ]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit" block>
                Sign in
              </Button>
            </Form.Item>
          </Form>
        </Col>
      </Row>
    </>
  );
};

export default SignInPage;
