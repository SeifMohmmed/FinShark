import axios from "axios";
import type { UserProfileToken } from "../Models/User";
import { handleError } from "../Helpers/ErrorHandler";

// Base URL of the backend API.
const baseUrl = "http://localhost:5000/api/";

// Sends the user's login credentials to the backend.
export const loginAPI = async (username: string, password: string) => {
  try {
    // Send a POST request to the login endpoint.
    //
    // The generic <UserProfileToken> tells TypeScript
    // what type of response we expect from the API.
    const data = await axios.post<UserProfileToken>(baseUrl + "account/login", {
      username,
      password,
    });

    // Return the response to the component/context
    // that called loginAPI().
    return data;
  } catch (error) {
    // If the API request fails, pass the error
    // to our centralized error handler.
    handleError(error);
  }
};

// Sends registration information to the backend.
export const registerAPI = async (
  username: string,
  email: string,
  password: string,
) => {
  try {
    // Send a POST request to the register endpoint.
    const data = await axios.post<UserProfileToken>(
      baseUrl + "account/register",
      {
        username,
        email,
        password,
      },
    );

    // Return the API response.
    return data;
  } catch (error) {
    // Handle the error using the centralized error handler.
    handleError(error);
  }
};
