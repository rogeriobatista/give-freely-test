import { ISignInUser } from "model/user"
import { SIGN_IN_URL } from "utils/apiURL"
import api from "utils/httpRequest"

export const SignInAsync = async (payload: ISignInUser) => {
  return api.post(SIGN_IN_URL, payload)
}