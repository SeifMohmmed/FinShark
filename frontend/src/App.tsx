import { useState, type ChangeEvent, type SyntheticEvent } from "react";
import "./App.css";
import { CardList } from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";

function App() {
  const [search, setSearch] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };

  const onClick = (e: SyntheticEvent) => {
    console.log(e);
  };
  return (
    <>
      <Search search={search} handleChange={handleChange} onClick={onClick} />
      <CardList />
    </>
  );
}

export default App;
