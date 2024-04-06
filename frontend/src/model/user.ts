export interface IUser {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface ICreateUser extends Omit<IUser, "id"> {}

export interface ISignInUser {
    email: string;
    password: string;
}

export interface IAuthUser extends IUser {
    authToken: string;
}