import { useState, type ChangeEvent, type SyntheticEvent } from "react";
import "./App.css";
import { CardList } from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";
import type { CompanySearch } from "./company";
import { searchCompanies } from "./api";

function App() {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };

  const onPrtofolioCreate = (e: SyntheticEvent) => {
    e.preventDefault();
    console.log(e);
  };

  return (
    <>
      <Search
        search={search}
        handleSearchChange={handleSearchChange}
        onSearchSubmit={onSearchSubmit}
      />
      <CardList
        searchResults={searchResult}
        onPrtofolioCreate={onPrtofolioCreate}
      />
      {serverError && <div>Unable to connect to API</div>}
    </>
  );
}

export default App;
