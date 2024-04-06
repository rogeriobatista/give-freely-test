import { DeleteOutlined, EditOutlined, PlusOutlined } from "@ant-design/icons";
import {
  Breadcrumb,
  Button,
  Col,
  DatePicker,
  Form,
  Input,
  Modal,
  Popconfirm,
  Row,
  Space,
  Table,
  TableProps,
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

type ColumnsType<T extends object> = TableProps<T>["columns"];

const EmployeesPage = () => {
  const dispatch = useAppDispatch();

  const { employees, isLoading } = useAppSelector((state) => state.employee);

  const [form] = Form.useForm();

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState<IEmployee>();

  useEffect(() => {
    if (!isLoading) dispatch(filterEmployees());
  }, []);

  const handleDelete = async (employeeId: number) => {
    dispatch(deleteEmployee(employeeId));
  };

  const showModal = (employee?: IEmployee) => {
    setIsModalOpen(true);
    setSelectedEmployee(employee);
  }

  const handleOnCreate = () => {
    setIsModalOpen(true);
  }

  const handleSave = () => {

    const values = form.getFieldsValue();

    if (selectedEmployee) {
      values.id = selectedEmployee.id;
    }

    dispatch(upsertEmployee(values));

    setIsModalOpen(false);
  }

  const handleCancel = () => {
    setIsModalOpen(false);
    setSelectedEmployee(undefined);
  }

  const onSearch = (predicate: string) => {
    dispatch(filterEmployees(predicate));
  }

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
          <Button
            icon={<EditOutlined />}
            onClick={() => showModal(record)}
          />
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
    <Row justify="center" align="middle">
      <Col span={24}>
        <Breadcrumb>
          <Breadcrumb.Item>Home</Breadcrumb.Item>
          <Breadcrumb.Item>Employees</Breadcrumb.Item>
        </Breadcrumb>
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
        title={selectedEmployee ? `Update ${selectedEmployee.firstName} ${selectedEmployee.lastName}` : "Add new Employee"}
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
        <Form form={form} layout="vertical" style={{ marginTop: 30 }}>
          <Row justify="space-between" align="middle">
            <Form.Item
              name="firstName"
              label="First name"
              required
              tooltip="Required field"
            >
              <Input placeholder="First name" value={selectedEmployee?.firstName} />
            </Form.Item>
            <Form.Item
              name="lastName"
              label="Last name"
              required
              tooltip="Required field"
            >
              <Input placeholder="Last name" />
            </Form.Item>
            <Form.Item
              name="email"
              label="Email"
              required
              tooltip="Required field"
            >
              <Input placeholder="Email" type="email"/>
            </Form.Item>
          </Row>

          <Row justify="start" align="middle">
            <Form.Item
              name="jobTitle"
              label="Job title"
              required
              tooltip="Required field"
            >
              <Input placeholder="Job title" />
            </Form.Item>
            <Form.Item
              name="dateOfJoining"
              className="margin-left-10"
              label="Date of joining"
              required
              tooltip="Required field"
            >
              <DatePicker placeholder="Date of joining" style={{ width: '180px' }} />
            </Form.Item>
          </Row>
        </Form>
      </Modal>
    </Row>
  );
};

export default EmployeesPage;
