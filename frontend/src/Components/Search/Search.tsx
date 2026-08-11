import { useState, type ChangeEvent, type SyntheticEvent } from "react";

type Props = {};

const Search: React.FC<Props> = (props: Props) => {
  const [search, setSearch] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };

  const onClick = (e: SyntheticEvent) => {
    console.log(e);
  };
  return (
    <div>
      <input
        value={search}
        onChange={(e) => handleChange(e)}
        placeholder="Search for a stock..."
      ></input>
      <button onClick={(e) => onClick(e)}>Search</button>
    </div>
  );
};

export default Search;
