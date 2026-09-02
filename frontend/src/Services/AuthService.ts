import axios from "axios";
import type { UserProfileToken } from "../Models/User";
import { handleError } from "../Helpers/ErrorHandler";

const baseUrl = "http://localhost:5000/api";

export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(baseUrl + "account/login", {
      username,
      password,
    });
    return data.data;
  } catch (error) {
    handleError(error);
  }
};

export const registerAPI = async (
  username: string,
  email: string,
  password: string,
) => {
  try {
    const data = await axios.post<UserProfileToken>(
      baseUrl + "account/register",
      {
        username,
        email,
        password,
      },
    );
    return data.data;
  } catch (error) {
    handleError(error);
  }
};
