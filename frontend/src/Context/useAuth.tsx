import { createContext, useContext, useEffect, useState } from "react";
import type { UserProfile } from "../Models/User";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { loginAPI, registerAPI } from "../Services/AuthService";
import { toast } from "react-toastify";

// Defines the shape of the authentication context.
// Any component using useAuth() will have access to these values/functions.
type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, username: string, password: string) => void;
  loginUser: (username: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

// Props received by the UserProvider component.
type Props = {
  children: React.ReactNode;
};

// Creates the authentication context.
// The actual values will be provided by UserProvider.
const UserContext = createContext<UserContextType>({} as UserContextType);

// UserProvider is responsible for managing the user's authentication state.
export const UserProvider = ({ children }: Props) => {
  // Used to navigate the user to different routes.
  const navigate = useNavigate();

  // Stores the JWT token.
  // null means that there is currently no token.
  const [token, setToken] = useState<string | null>(null);

  // Stores the currently logged-in user's information.
  // null means that no user is currently logged in.
  const [user, setUser] = useState<UserProfile | null>(null);

  // Used to prevent the application from rendering before
  // we finish checking localStorage for an existing login session.
  const [isReady, setIsReady] = useState(false);

  // Runs once when the UserProvider is mounted.
  useEffect(() => {
    // Try to retrieve the saved user and token from localStorage.
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");

    // If both user and token exist, restore the previous login session.
    if (user && token) {
      // Convert the stored JSON string back into a JavaScript object.
      setUser(JSON.parse(user));

      // Restore the JWT token into React state.
      setToken(token);

      // Add the token to Axios default headers.
      // This means future Axios requests will automatically
      // send the JWT token with the Authorization header.
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }

    // Authentication initialization is finished.
    setIsReady(true);
  }, []);

  // Handles registering a new user.
  const registerUser = async (
    username: string,
    email: string,
    password: string,
  ) => {
    // Call the register API with the user's information.
    await registerAPI(email, username, password)
      .then((res) => {
        // Make sure we received a response.
        if (res) {
          // Save the JWT token in localStorage
          // so the login session survives a page refresh.
          localStorage.setItem("token", res?.data.token);

          // Create an object containing the user's information.
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };

          // Save the user object in localStorage.
          // JSON.stringify converts the object into a string.
          localStorage.setItem("user", JSON.stringify(userObj));

          // Update the token in React state.
          setToken(res?.data.token!);

          // Update the current user in React state.
          setUser(userObj!);

          // Display a success notification.
          toast.success("Login Success!");

          // Navigate the user to the search page.
          navigate("/search");
        }
      })
      .catch((e) => {
        // Display a warning if the registration request fails.
        toast.warning("Server error occured");
      });
  };

  // Handles logging in an existing user.
  const loginUser = async (username: string, password: string) => {
    // Send the username and password to the login API.
    await loginAPI(username, password)
      .then((res) => {
        // Make sure we received a response.
        if (res) {
          // Save the JWT token in localStorage.
          localStorage.setItem("token", res?.data.token);

          // Create an object containing the user's information.
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };

          // Save the user object in localStorage.
          localStorage.setItem("user", JSON.stringify(userObj));

          // Update the token in React state.
          setToken(res?.data.token!);

          // Update the current user in React state.
          setUser(userObj!);

          // Display a success notification.
          toast.success("Login Success!");

          // Redirect the user to the search page.
          navigate("/search");
        }
      })
      .catch((e) => {
        // Display a warning if the login request fails.
        toast.warning("Server error occured");
      });
  };

  // Checks whether a user is currently logged in.
  // !!user converts the user value into a boolean:
  // user exists -> true
  // user is null -> false
  const isLoggedIn = () => {
    return !!user;
  };

  // Logs the current user out.
  const logout = () => {
    // Remove the saved authentication data from localStorage.
    localStorage.removeItem("token");
    localStorage.removeItem("user");

    // Clear the user from React state.
    setUser(null);

    // Clear the token from React state.
    setToken("");

    // Redirect the user to the home page.
    navigate("/");
  };

  return (
    // Make authentication data available to all child components.
    <UserContext.Provider
      value={{
        loginUser,
        user,
        token,
        logout,
        isLoggedIn,
        registerUser,
      }}
    >
      {/* 
        Only render the application after checking localStorage.
        This prevents components from rendering before the
        authentication state has been restored.
      */}
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

// Custom hook used by components to access the authentication context.
export const useAuth = () => useContext(UserContext);
