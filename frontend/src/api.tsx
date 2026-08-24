import axios from "axios";
import type {
  CompanyBalanceSheet,
  CompanyCashFlow,
  CompanyCompData,
  CompanyIncomeStatement,
  CompanyKeyMetrics,
  CompanySearch,
  CompanyTenK,
} from "./company";

export interface SearchResponse {
  data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/stable/search-symbol?query=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );

    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.message);
      return error.message;
    } else {
      console.log("unexpected error: ", error);
      return "An expected error has occurred.";
    }
  }
};

export const getCompanyProfile = async (query: string) => {
  try {
    const data = await axios.get(
      `https://financialmodelingprep.com/stable/profile?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return data;
  } catch (error: any) {
    console.log("error message: ", error.message);
  }
};

export const getKeyMetrics = async (query: string) => {
  try {
    const data = await axios.get<CompanyKeyMetrics[]>(
      `https://financialmodelingprep.com/stable/key-metrics-ttm?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return data;
  } catch (error: any) {
    console.log("error message: ", error.message);
  }
};

export const getIncomeStatement = async (query: string) => {
  try {
    const response = await axios.get<CompanyIncomeStatement[]>(
      `https://financialmodelingprep.com/stable/income-statement?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return response.data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    throw error;
  }
};

export const getBalanceSheet = async (query: string) => {
  try {
    const response = await axios.get<CompanyBalanceSheet[]>(
      `https://financialmodelingprep.com/stable/balance-sheet-statement?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return response.data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    throw error;
  }
};

export const getCashFlowStatement = async (query: string) => {
  try {
    const response = await axios.get<CompanyCashFlow[]>(
      `https://financialmodelingprep.com/stable/cash-flow-statement?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return response.data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    throw error;
  }
};

export const getStockPeers = async (query: string) => {
  try {
    const response = await axios.get<CompanyCompData[]>(
      `https://financialmodelingprep.com/stable/stock-peers?symbol=${query}&apikey=${import.meta.env.VITE_API_KEY}`,
    );
    return response.data;
  } catch (error: any) {
    console.log("error message: ", error.message);
    throw error;
  }
};

export const getTenKFilings = async (query: string) => {
  try {
    const response = await axios.get<CompanyTenK[]>(
      `https://financialmodelingprep.com/stable/sec-filings-search/symbol?symbol=${query}&from=2020-01-01&to=2026-08-24&page=0&limit=100&apikey=${import.meta.env.VITE_API_KEY}`,
    );

    return response.data.filter((filing) => filing.formType === "10-K");
  } catch (error: any) {
    console.log("error message: ", error.message);
    throw error;
  }
};
