import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import CompanyProfile from "../Components/CompanyProfile/CompanyProfile";
import IncomeStatements from "../Components/IncomeStatements/IncomeStatements";
import DesignGuide from "../Pages/DesignPage/DesignPage";
import BalanceSheet from "../Components/BalanceSheet/BalanceSheet";
import CashFlowStatement from "../Components/CashFlowStatement/CashFlowStatement";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "search", element: <SearchPage /> },
      { path: "design-guide", element: <DesignGuide /> },
      {
        path: "company/:ticker",
        element: <CompanyPage />,
        children: [
          { path: "company-profile", element: <CompanyProfile /> },
          { path: "income-statement", element: <IncomeStatements /> },
          { path: "balance-sheet", element: <BalanceSheet /> },
          { path: "cash-flow-statement", element: <CashFlowStatement /> },
        ],
      },
    ],
  },
]);
