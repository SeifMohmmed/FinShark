import { type SyntheticEvent } from "react";

interface Props {
  onPrtofolioCreate: (e: SyntheticEvent) => void;
  symbol: string;
}

const AddPortfolio = ({ onPrtofolioCreate, symbol }: Props) => {
  return (
    <form onSubmit={onPrtofolioCreate}>
      <input type="hidden" name="symbol" value={symbol} />
      <button type="submit">Add </button>
    </form>
  );
};

export default AddPortfolio;
