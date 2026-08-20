import type { SyntheticEvent } from "react";
import type { CompanySearch } from "../../company";
import Card from "../Card/Card";
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: CompanySearch[];
  onPrtofolioCreate: (e: SyntheticEvent) => void;
}

export const CardList: React.FC<Props> = ({
  searchResults,
  onPrtofolioCreate,
}: Props) => {
  return (
    <>
      {searchResults.length > 0 ? (
        searchResults.map((result) => {
          return (
            <Card
              id={result.symbol}
              key={uuidv4()}
              searchResults={result}
              onPrtofolioCreate={onPrtofolioCreate}
            />
          );
        })
      ) : (
        <h1>No results!</h1>
      )}
    </>
  );
};
