import type { SyntheticEvent } from "react";
import type { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
import "./Card.css";

interface Props {
  id: string;
  searchResults: CompanySearch;
  onPrtofolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResults,
  onPrtofolioCreate,
}: Props) => {
  return (
    <div key={id} className="card">
      <div className="details">
        <h2>
          {searchResults.name} ({searchResults.symbol})
        </h2>
        <p>${searchResults.currency}</p>
      </div>
      <p className="info">
        {searchResults.exchangeShortName} - {searchResults.stockExchange}
      </p>
      <AddPortfolio
        onPrtofolioCreate={onPrtofolioCreate}
        symbol={searchResults.symbol}
      />
    </div>
  );
};

export default Card;
