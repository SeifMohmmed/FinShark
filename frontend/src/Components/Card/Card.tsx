import type { SyntheticEvent } from "react";
import type { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
import "./Card.css";

interface Props {
  id: string;
  searchResults: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResults,
  onPortfolioCreate,
}: Props) => {
  return (
    <div
      className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
      key={id}
      id={id}
    >
      <h2 className="font-bold text-center text-black md:text-left">
        {searchResults.name} ({searchResults.symbol})
      </h2>
      <p className="text-black">{searchResults.currency}</p>
      <p className="font-bold text-black">
        {searchResults.exchangeShortName} - {searchResults.stockExchange}
      </p>
      <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResults.symbol}
      />
    </div>
  );
};

export default Card;
