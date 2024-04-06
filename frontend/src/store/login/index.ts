import { createSlice } from '@reduxjs/toolkit';
import { IUser } from 'model/user';

interface State {
  authToken: string;
  isLoggedIn: boolean;
  user?: IUser
}
export const initialLoginState: State = {
  authToken: "",
  isLoggedIn: true,
};
const loginSlice = createSlice({
  name: 'login',
  initialState: initialLoginState,
  reducers: {
    login: (state, action) => {
      state.authToken = action.payload.authToken;
      state.isLoggedIn = true;
      state.user = action.payload.user;
    },
    logout: (state) => {
      state.isLoggedIn = false;
      state.authToken = "";
      state.user = undefined;
    }
  }
});

export const { login, logout } = loginSlice.actions;

export default loginSlice.reducer;
