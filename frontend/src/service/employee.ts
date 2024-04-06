import { ICreateEmployee } from "model/employee";
import { EMPLOYEE_URL } from "utils/apiURL";
import api from "utils/httpRequest";

const filterEmployeeAsync = async (predicate?: string) =>
  await api.get(`${EMPLOYEE_URL}?filter=${predicate ?? ""}`);

const getEmployeeAsync = async () => await api.get(EMPLOYEE_URL);

const getEmployeeByIdAsync = async (id: number) =>
  await api.get(`${EMPLOYEE_URL}/${id}`);

const createEmployeeAsync = async (body: ICreateEmployee) =>
  await api.post(EMPLOYEE_URL, body);

const updateEmployeeAsync = async (id: number, body: ICreateEmployee) =>
  await api.put(`${EMPLOYEE_URL}/${id}`, body);

const deleteEmployeeAsync = async (id: number) =>
  await api.delete(`${EMPLOYEE_URL}/${id}`);

export {
  filterEmployeeAsync,
  getEmployeeAsync,
  getEmployeeByIdAsync,
  createEmployeeAsync,
  updateEmployeeAsync,
  deleteEmployeeAsync,
};
