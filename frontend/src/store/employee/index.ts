import { createSlice } from '@reduxjs/toolkit';
import { ICreateEmployee, IEmployee } from 'model/employee';
import { getEmployeeAsync, createEmployeeAsync, updateEmployeeAsync, deleteEmployeeAsync, filterEmployeeAsync } from 'service/employee';
import { AppDispatch } from 'store';

interface State {
  employees: IEmployee[];
  isLoading: boolean;
}

export const initialEmployeeState: State = {
  employees: [],
  isLoading: false
};

export const filterEmployees = (predicate?: string) => async (dispatch: AppDispatch) => {
  dispatch(setLoading(true));
  const response = await filterEmployeeAsync(predicate);
  dispatch(setEmployees(response.data));
  dispatch(setLoading(false));
}

export const fetchEmployees = () => async (dispatch: any) => {
  dispatch(setLoading(true));
  const response = await getEmployeeAsync();
  dispatch(setEmployees(response.data));
  dispatch(setLoading(false));
}

export const upsertEmployee = (employee: ICreateEmployee, id?: number) => async(dispatch: any) => {
  dispatch(setLoading(true));

  if (id) {
    await updateEmployeeAsync(id, employee);
    dispatch(updateEmployee(employee));
  } else {
    await createEmployeeAsync(employee);
    dispatch(addEmployee(employee));
  }

  dispatch(setLoading(false));
}

export const deleteEmployee = (userId: number) => async(dispatch: any) => {
  dispatch(setLoading(true));
  await deleteEmployeeAsync(userId);
  dispatch(removeEmployee(userId));
  dispatch(setLoading(false));
}

const employeeSlice = createSlice({
  name: 'employee',
  initialState: initialEmployeeState,
  reducers: {
    setEmployees: (state, action) => {
      state.employees = action.payload;
    },
    addEmployee: (state, action) => {
      state.employees = [...state.employees, action.payload];
    },
    updateEmployee: (state, action) => {
      const userIndex = state.employees.findIndex(x => x.id == action.payload.id);
      state.employees[userIndex] = action.payload;
    },
    removeEmployee: (state, action) => {
      state.employees = state.employees.filter(x => x.id !== action.payload)
    },
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
  }
});

export const { setEmployees, addEmployee, updateEmployee, removeEmployee, setLoading } = employeeSlice.actions;

export default employeeSlice.reducer;
