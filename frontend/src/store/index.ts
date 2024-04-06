import  LoginReducer  from 'store/login';
import EmployeeReducer from 'store/employee';
import { configureStore } from "@reduxjs/toolkit";
import type { Store } from "@reduxjs/toolkit";

export const store: Store = configureStore({
	reducer: {
        login: LoginReducer,
		employee: EmployeeReducer
	},
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;

