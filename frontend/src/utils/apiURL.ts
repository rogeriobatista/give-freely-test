export const {
  VITE_APP_API
} =
  import.meta.env;
const API_URL = VITE_APP_API

const SIGN_IN_URL = "/user/sign-in"
const EMPLOYEE_URL = "/employees"

export {
  API_URL,
  EMPLOYEE_URL,
  SIGN_IN_URL
}
