import { useState } from "react";

type Props = {};

const Search: React.FC<Props> = (props: Props) => {
  const [search, setSearch] = useState<string>("");

  const onClick = (e: any) => {
    setSearch(e.target.value);
    console.log(e);
  };

  return (
    <div>
      <input
        value={search}
        onChange={onClick}
        placeholder="Search for a stock..."
      />
    </div>
  );
};

export default Search;
