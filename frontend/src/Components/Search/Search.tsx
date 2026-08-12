import { type ChangeEvent, type SyntheticEvent } from "react";

interface Props {
  search: string;
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onClick: (e: SyntheticEvent) => void;
}

const Search: React.FC<Props> = ({ search, handleChange, onClick }: Props) => {
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
