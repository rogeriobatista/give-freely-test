import { DeleteOutlined, EditOutlined, PlusOutlined } from "@ant-design/icons";
import {
  Breadcrumb,
  Button,
  Col,
  DatePicker,
  Form,
  FormProps,
  Input,
  Modal,
  Popconfirm,
  Row,
  Space,
  Table,
  TableProps,
  message,
} from "antd";
import { IEmployee } from "model/employee";
import { useEffect, useState } from "react";

import { useAppDispatch, useAppSelector } from "store/hooks";
import {
  filterEmployees,
  upsertEmployee,
  deleteEmployee,
} from "store/employee";
import Search from "antd/es/input/Search";
import dayjs from "dayjs";

const breadcrumbItems = [
  {
    title: "Home",
  },
  {
    title: "Employees",
  },
];

type FieldType = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  jobTitle: string;
  dateOfJoining: Date;
  totalOfYearsInTheCompany: number;
};

type ColumnsType<T extends object> = TableProps<T>["columns"];

const EmployeesPage = () => {
  const dispatch = useAppDispatch();
  const [messageApi, contextHolder] = message.useMessage();

  const { employees, isLoading } = useAppSelector((state) => state.employee);

  const [form] = Form.useForm();

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState<IEmployee>();

  useEffect(() => {
    if (!isLoading) dispatch(filterEmployees());
  }, []);

  const handleDelete = async (employeeId: number) => {
    dispatch(deleteEmployee(employeeId));
    messageApi.open({
      type: 'success',
      content: 'Employee deleted',
    });
  };

  const showModal = (employee: IEmployee) => {
    setIsModalOpen(true);

    if (employee) {
      setSelectedEmployee(employee);

      form.setFieldValue("firstName", employee.firstName);
      form.setFieldValue("lastName", employee.lastName);
      form.setFieldValue("email", employee.email);
      form.setFieldValue("jobTitle", employee.jobTitle);
      form.setFieldValue("dateOfJoining", dayjs(employee?.dateOfJoining));
    }
  };

  const handleOnCreate = () => {
    setIsModalOpen(true);
  };

  const handleSave = async () => {
    form.submit();
  };

  const onFormSubmit: FormProps<FieldType>["onFinish"] = (values) => {
    if (selectedEmployee) {
      values.id = selectedEmployee.id;
      values.totalOfYearsInTheCompany =
        selectedEmployee.totalOfYearsInTheCompany;
    }

    dispatch(upsertEmployee(values, selectedEmployee?.id));

    form.resetFields();

    setIsModalOpen(false);

    messageApi.open({
      type: 'success',
      content: 'Employee saved',
    });
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    setSelectedEmployee(undefined);
    form.resetFields();
  };

  const onSearch = (predicate: string) => {
    dispatch(filterEmployees(predicate));
  };

  const columns: ColumnsType<IEmployee> = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
      align: "center",
    },
    {
      title: "Name",
      key: "name",
      align: "center",
      render: (row: IEmployee) => {
        return (
          <>
            {row.firstName} {row.lastName}
          </>
        );
      },
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
      align: "center",
    },
    {
      title: "Job title",
      dataIndex: "jobTitle",
      key: "jobTitle",
      align: "center",
    },
    {
      title: "Date of Joining",
      dataIndex: "dateOfJoining",
      key: "dateOfJoining",
      align: "center",
      render: (dateOfJoining: Date) => {
        return new Date(dateOfJoining)?.toLocaleDateString("en-US");
      },
    },
    {
      title: "Years of service",
      dataIndex: "totalOfYearsInTheCompany",
      key: "totalOfYearsInTheCompany",
      align: "center",
    },
    {
      title: "Actions",
      key: "action",
      align: "center",
      render: (_, record) => (
        <Space size="middle">
          <Button icon={<EditOutlined />} onClick={() => showModal(record)} />
          <Popconfirm
            title="Delete employee"
            description="Are you sure you want to delete this employee?"
            onConfirm={() => handleDelete(record.id!)}
            okText="Yes"
            cancelText="No"
          >
            <Button
              icon={<DeleteOutlined />}
              style={{ background: "#BE041C", color: "#fff" }}
            />
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <>
      {contextHolder}
      <Row justify="center" align="middle">
        <Col span={24}>
          <Breadcrumb items={breadcrumbItems}></Breadcrumb>
        </Col>
        <Col span={24} flex={1} style={{ marginTop: 50 }}>
          <Row justify="end" align="middle" style={{ marginBottom: 10 }}>
            <Col>
              <Button
                icon={<PlusOutlined />}
                onClick={handleOnCreate}
                style={{ marginRight: 5, backgroundColor: "#6EF6AA" }}
              />
              <Search
                placeholder="Search..."
                allowClear
                onSearch={onSearch}
                style={{ width: 200 }}
              />
            </Col>
          </Row>
          <Table
            rowKey="id"
            columns={columns}
            dataSource={employees}
            loading={isLoading}
          />
        </Col>

        <Modal
          title={
            selectedEmployee
              ? `Update ${selectedEmployee.firstName} ${selectedEmployee.lastName}`
              : "Add new Employee"
          }
          open={isModalOpen}
          onOk={handleSave}
          width={620}
          zIndex={1002}
          okText="Save"
          onCancel={handleCancel}
          cancelText="Cancel"
          closable={false}
          footer={(_, { OkBtn, CancelBtn }) => (
            <Row justify="end" align="middle">
              <Col>
                <Space size="small">
                  <CancelBtn />
                  <OkBtn />
                </Space>
              </Col>
            </Row>
          )}
        >
          <Form
            form={form}
            layout="vertical"
            style={{ marginTop: 30 }}
            disabled={isLoading}
            onFinish={onFormSubmit}
          >
            <Row justify="space-between" align="middle">
              <Form.Item<FieldType>
                name="firstName"
                label="First name"
                required
                tooltip="Required field"
                rules={[
                  { required: true, message: "Please input your first name!" },
                ]}
              >
                <Input placeholder="First name" />
              </Form.Item>
              <Form.Item<FieldType>
                name="lastName"
                label="Last name"
                required
                tooltip="Required field"
                rules={[
                  { required: true, message: "Please input your last name!" },
                ]}
              >
                <Input placeholder="Last name" />
              </Form.Item>
              <Form.Item<FieldType>
                name="email"
                label="Email"
                required
                tooltip="Required field"
                rules={[
                  { required: true, message: "Please input your email!" },
                ]}
              >
                <Input placeholder="Email" type="email" />
              </Form.Item>
            </Row>

            <Row justify="start" align="middle">
              <Form.Item<FieldType>
                name="jobTitle"
                label="Job title"
                required
                tooltip="Required field"
                rules={[
                  { required: true, message: "Please input your job title!" },
                ]}
              >
                <Input placeholder="Job title" />
              </Form.Item>
              <Form.Item<FieldType>
                name="dateOfJoining"
                className="margin-left-10"
                label="Date of joining"
                required
                tooltip="Required field"
                rules={[
                  {
                    required: true,
                    message: "Please input your date of joining!",
                  },
                ]}
              >
                <DatePicker
                  placeholder="Date of joining"
                  style={{ width: "180px" }}
                />
              </Form.Item>
            </Row>
          </Form>
        </Modal>
      </Row>
    </>
  );
};

export default EmployeesPage;
