import type { CompanySearch } from "../../company";
import "./Card.css";

interface Props {
  id: string;
  searchResults: CompanySearch;
}

const Card: React.FC<Props> = ({ id, searchResults }: Props) => {
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
    </div>
  );
};

export default Card;
