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

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onClick = async (e: SyntheticEvent) => {
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };
  return (
    <>
      <Search search={search} handleChange={handleChange} onClick={onClick} />
      <CardList />
      {serverError && <div>Unable to connect to API</div>}
    </>
  );
}

export default App;
