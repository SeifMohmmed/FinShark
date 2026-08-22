import axios from "axios";
import type { CompanySearch } from "./company";

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
